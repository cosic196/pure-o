using UnityEngine;

[RequireComponent(typeof(GameObjectEventManager))]
[RequireComponent(typeof(Renderer))]
public class EnemyAnimationController : MonoBehaviour {

    private GameObjectEventManager _gameObjectEventManager;
    private Renderer _renderer;

	void Start () {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _renderer = GetComponent<Renderer>();

        _gameObjectEventManager.StartListening("Died", AnimateDeath);
	}

    private void AnimateDeath()
    {
        _renderer.enabled = false;
    }
	
}
