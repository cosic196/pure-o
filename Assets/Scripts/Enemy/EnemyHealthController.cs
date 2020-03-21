using UnityEngine;

[RequireComponent(typeof(GameObjectEventManager))]
public class EnemyHealthController : MonoBehaviour {

    private Collider[] _colliders;
    private GameObjectEventManager _gameObjectEventManager;
    [SerializeField]
    private int _hp;
    private bool _dead = false;
    
    void Start () {
        _colliders = GetComponentsInChildren<Collider>();
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _gameObjectEventManager.StartListening("Shot", Damaged);
        EventManager.StartListening("EnemiesDie", Died);
	}

    private void Damaged(string shootInfo)
    {
        if (_dead)
        {
            return;
        }
        _hp -= JsonUtility.FromJson<ShootInfo>(shootInfo).damage;
        if(_hp <= 0)
        {
            _dead = true;
            EventManager.TriggerEvent("AnEnemyDied");
            _gameObjectEventManager.TriggerEvent("Killed");
            _hp = 0;
        }
        _gameObjectEventManager.TriggerEvent("Damaged", _hp.ToString());
    }

    private void Died()
    {
        if(_dead)
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].enabled = false;
            }
            _gameObjectEventManager.TriggerEvent("Died");
            _dead = false;
        }
    }
}
