using UnityEngine;

[RequireComponent(typeof(GameObjectEventManager))]
[RequireComponent(typeof(Collider))]
public class EnemyHealthController : MonoBehaviour {

    private Collider _collider;
    private GameObjectEventManager _gameObjectEventManager;
    [SerializeField]
    private int _hp;
    private bool _dead = false;
    
    void Start () {
        _collider = GetComponent<Collider>();
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _gameObjectEventManager.StartListening("Shot", Damaged);
        EventManager.StartListening("EnemiesDie", Died);
	}

    private void Damaged(string shootInfo)
    {
        _hp -= JsonUtility.FromJson<ShootInfo>(shootInfo).damage;
        if(_hp <= 0)
        {
            _dead = true;
        }
    }

    private void Died()
    {
        if(_dead)
        {
            _collider.enabled = false;
            _gameObjectEventManager.TriggerEvent("Died");
            _dead = false;
        }
    }
}
