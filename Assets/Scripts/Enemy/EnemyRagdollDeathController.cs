using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObjectEventManager))]
public class EnemyRagdollDeathController : MonoBehaviour {

    [SerializeField]
    private List<Rigidbody> _disableGravityRigidbodies;
    [SerializeField]
    private Rigidbody _hanger;
    private GameObjectEventManager _gameObjectEventManager;

	void Start () {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _gameObjectEventManager.StartListening("Killed", Killed);
	}

    private void Killed()
    {
        foreach (var rb in _disableGravityRigidbodies)
        {
            rb.useGravity = false;
        }
        _hanger.isKinematic = false;
    }
}
