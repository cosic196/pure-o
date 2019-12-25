using UnityEngine;

[RequireComponent(typeof(GameObjectEventManager))]
[RequireComponent(typeof(Collider))]
public class EnemyHealthController : MonoBehaviour {

    private Collider _collider;
    private GameObjectEventManager _gameObjectEventManager;
    [SerializeField]
    private int _hp;
    
    void Start () {
        _collider = GetComponent<Collider>();
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _gameObjectEventManager.StartListening("Shot", Damaged);
	}

    private void Damaged(string shootInfo)
    {
        _hp -= JsonUtility.FromJson<ShootInfo>(shootInfo).damage;
        if(_hp <= 0)
        {
            _collider.enabled = false;
            _gameObjectEventManager.TriggerEvent("Died");
        }
    }
}
