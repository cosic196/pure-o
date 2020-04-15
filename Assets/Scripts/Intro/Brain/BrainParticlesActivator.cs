using System.Collections.Generic;
using UnityEngine;

public class BrainParticlesActivator : MonoBehaviour {

    [SerializeField]
    List<ParticleSystem> _first;
    [SerializeField]
    List<ParticleSystem> _second;
    [SerializeField]
    List<ParticleSystem> _third;

    void Start () {
        EventManager.StartListening("BrainLostLife", TryActivateParticles);
        EventManager.StartListening("BrainDied", StopAllPlayThird);
	}

    private void StopAllPlayThird()
    {
        foreach (var ps in _third)
        {
            ps.Play();
        }
        foreach (var ps in _first)
        {
            ps.Stop();
        }
        foreach (var ps in _second)
        {
            ps.Stop();
        }
    }

    private void TryActivateParticles(string lives)
    {
        if(lives == "2")
        {
            foreach (var ps in _first)
            {
                ps.Play();
            }
        }
        else if(lives == "1")
        {
            foreach (var ps in _second)
            {
                ps.Play();
            }
        }
    }
}
