using UnityEngine;

public class HpController : MonoBehaviour {

    [SerializeField]
    private float _hp;
    private float _currentHp;
    [SerializeField]
    private float _regenerationSpeed;
    [SerializeField]
    private float _missedNoteDamage;
    
	void Start () {
        EventManager.StartListening("NoteOutOfRange", Damaged);
        _currentHp = _hp;
	}
	
    private void Damaged(string noteInfo)
    {
        _currentHp -= _missedNoteDamage;
        if(_currentHp <= 0f)
        {
            EventManager.TriggerEvent("PlayerDied");
        }
    }
    
	void Update () {
		if(_currentHp < _hp)
        {
            _currentHp += _regenerationSpeed * CustomTime.GetDeltaTime();
        }
        if(_currentHp > _hp)
        {
            _currentHp = _hp;
        }
	}
}
