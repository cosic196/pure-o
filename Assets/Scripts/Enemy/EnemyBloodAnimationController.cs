using UnityEngine;

public class EnemyBloodAnimationController : MonoBehaviour {

    [SerializeField]
    private GameObject _shotParticlePrefab, _shotParticlePrefab2 ,_diedParticlePrefab;
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
        ShootInfo shootInfo = JsonUtility.FromJson<ShootInfo>(vectorJson);
        Instantiate(_shotParticlePrefab, shootInfo.pointOfHit, Quaternion.LookRotation(shootInfo.normal), _transform);
        Instantiate(_shotParticlePrefab2, shootInfo.pointOfHit, Quaternion.LookRotation(shootInfo.normal));
    }
}
