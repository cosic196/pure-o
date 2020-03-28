using UnityEngine;

public class ParticleSystemReverseSimulationSuperSimple : MonoBehaviour
{
    ParticleSystem[] particleSystems;

    float[] simulationTimes;

    public float startTime = 2.0f;
    public float simulationSpeedScale = 1.0f;

    void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>(false);
        simulationTimes = new float[particleSystems.Length];
    }

    void OnEnable()
    {
        for (int i = 0; i < simulationTimes.Length; i++) { simulationTimes[i] = 0.0f; }
        particleSystems[0].Simulate(startTime, true, false, true);
    }
    void Update()
    {
        particleSystems[0].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        for (int i = particleSystems.Length - 1; i >= 0; i--)
        {
            particleSystems[i].Play(false);

            simulationTimes[i] -= (Time.deltaTime * particleSystems[i].main.simulationSpeed) * simulationSpeedScale;

            float currentSimulationTime = startTime + simulationTimes[i];
            particleSystems[i].Simulate(currentSimulationTime, false, false, true);

            if (currentSimulationTime < 0.0f)
            {
                particleSystems[i].Play(false);
                particleSystems[i].Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }
}