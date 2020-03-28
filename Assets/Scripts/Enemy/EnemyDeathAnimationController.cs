using UnityEngine;

[RequireComponent(typeof(GameObjectEventManager))]
public class EnemyDeathAnimationController : MonoBehaviour {

    [SerializeField]
    private ParticleSystem _particleSystem;
    private GameObjectEventManager _gameObjectEventManager;

	void Start () {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _gameObjectEventManager.StartListening("Died", StartAnimation);
        _gameObjectEventManager.StartListening("Died", TurnOffOtherParticles);
	}

    private void TurnOffOtherParticles()
    {
        var particles = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Stop();
        }
    }

    private void StartAnimation()
    {
        _particleSystem.Play();
    }
}