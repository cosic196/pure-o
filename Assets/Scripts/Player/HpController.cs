using UnityEngine;

public class HpController : MonoBehaviour {

    [SerializeField]
    private float _maxHp;
    private float _currentHp;
    [SerializeField]
    private float _regenerationSpeed;
    [SerializeField]
    private float _missedNoteDamage;

    public float CurrentHp
    {
        get
        {
            return _currentHp;
        }
        set
        {
            float returnValue;
            if (value < 0)
                returnValue = 0;
            else if (value > _maxHp)
                returnValue = _maxHp;
            else
                returnValue = value;
            _currentHp = returnValue;
            EventManager.TriggerEvent("HpChanged", returnValue.ToString() + "/" + _maxHp.ToString());
        }
    }
    
	void Start () {
        EventManager.StartListening("NoteOutOfRange", Damaged);
        _currentHp = _maxHp;
	}
	
    private void Damaged(string noteInfo)
    {
        CurrentHp -= _missedNoteDamage;
        EventManager.TriggerEvent("PlayerDamaged");
        if(CurrentHp <= 0f)
        {
            EventManager.TriggerEvent("PlayerDied");
        }
    }
    
	void Update () {
		if(CurrentHp < _maxHp)
        {
            CurrentHp += _regenerationSpeed * CustomTime.GetDeltaTime();
        }
        if(CurrentHp > _maxHp)
        {
            CurrentHp = _maxHp;
        }
	}
}
