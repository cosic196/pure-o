using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBloodAnimationController : MonoBehaviour {

    [SerializeField]
    private GameObject _particlePrefab;
    private GameObjectEventManager _gameObjectEventManager;
    private Transform _transform;
    
	void Start () {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _gameObjectEventManager.StartListening("Shot", StartAnimation);
        _transform = GetComponent<Transform>();
	}
	
    void StartAnimation(string vectorJson)
    {
        Vector3 position = JsonUtility.FromJson<ShootInfo>(vectorJson).pointOfHit;
        Instantiate(_particlePrefab, position, Quaternion.identity, _transform);
        //_particlePrefab.GetComponent<ParticleSystem>().Play();
    }
}
