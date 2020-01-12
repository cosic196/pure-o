using UnityEngine;

public class EnemyBloodAnimationController : MonoBehaviour {

    [SerializeField]
    private GameObject _shotParticlePrefab, _diedParticlePrefab;
    private GameObjectEventManager _gameObjectEventManager;
    private Transform _transform;
    
	void Start () {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _gameObjectEventManager.StartListening("Shot", StartShotAnimation);
        _gameObjectEventManager.StartListening("Died", StartDiedAnimation);
        _transform = GetComponent<Transform>();
	}

    private void StartDiedAnimation()
    {
        var particleSystem = Instantiate(_diedParticlePrefab, _transform.position, Quaternion.identity);
        particleSystem.GetComponent<Transform>().rotation = _transform.rotation;
    }

    void StartShotAnimation(string vectorJson)
    {
        Vector3 position = JsonUtility.FromJson<ShootInfo>(vectorJson).pointOfHit;
        var particleSystem = Instantiate(_shotParticlePrefab, position, Quaternion.identity, _transform);
        particleSystem.GetComponent<Transform>().localRotation = new Quaternion(0, 0, 0, 0);
    }
}
