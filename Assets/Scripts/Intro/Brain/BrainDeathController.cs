using System;
using SonicBloom.Koreo.Players;
using UnityEngine;

public class BrainDeathController : MonoBehaviour {

    [SerializeField]
    private Renderer _renderer;
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private SimpleMusicPlayer _simpleMusicPlayer;
    private bool _died = false;

	void Start () {
        EventManager.StartListening("BrainDied", Die);
        EventManager.StartListening("StopMusicAfterDied", StopMusicAfterDied);
	}

    private void StopMusicAfterDied()
    {
        if (!_died)
            return;
        _simpleMusicPlayer.Stop();
    }

    private void Die()
    {
        _renderer.enabled = false;
        _collider.enabled = false;
        _died = true;
    }
}
