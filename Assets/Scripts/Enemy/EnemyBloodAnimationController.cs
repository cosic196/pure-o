using UnityEngine;

public class EnemyBloodAnimationController : MonoBehaviour {

    [SerializeField]
    private GameObject _shotParticlePrefab, _shotParticlePrefab2;
    private GameObjectEventManager _gameObjectEventManager;
    private Transform _transform;
    
	void Start () {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _gameObjectEventManager.StartListening("Shot", StartShotAnimation);
        _transform = GetComponent<Transform>();
	}

    void StartShotAnimation(string vectorJson)
    {
        ShootInfo shootInfo = JsonUtility.FromJson<ShootInfo>(vectorJson);
        Instantiate(_shotParticlePrefab, shootInfo.pointOfHit, Quaternion.LookRotation(shootInfo.normal), _transform);
        Instantiate(_shotParticlePrefab2, shootInfo.pointOfHit, Quaternion.LookRotation(shootInfo.normal));
    }
}
