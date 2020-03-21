using UnityEngine;

[RequireComponent(typeof(GameObjectEventManager))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyShotForceController : MonoBehaviour {

    [SerializeField]
    private float _force = 2f;
    private GameObjectEventManager _gameObjectEventManager;
    private Rigidbody _rigidbody;

	void Start () {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _rigidbody = GetComponent<Rigidbody>();
        _force *= 10;
        _gameObjectEventManager.StartListening("Shot", AddForce);
	}

    private void AddForce(string shootInfoJson)
    {
        ShootInfo shootInfo = JsonUtility.FromJson<ShootInfo>(shootInfoJson);
        _rigidbody.AddForceAtPosition(_force * shootInfo.direction, shootInfo.pointOfHit, ForceMode.Impulse);
    }
}
