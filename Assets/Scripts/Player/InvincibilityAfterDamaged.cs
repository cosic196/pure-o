using UnityEngine;

[RequireComponent(typeof(HpController))]
public class InvincibilityAfterDamaged : MonoBehaviour {

    [SerializeField]
    private float _invincibilityDuration;
    private HpController _hpController;
    private float _timer;
    
	void Start () {
        _timer = _invincibilityDuration;
        _hpController = GetComponent<HpController>();
        EventManager.StartListening("PlayerDamaged", StartInvincibility);
	}

    private void StartInvincibility()
    {
        _timer = 0f;
        _hpController.enabled = false;
    }

    void Update () {
		if(_timer < _invincibilityDuration)
        {
            _timer += CustomTime.GetDeltaTime();
        }
        else if(_timer > _invincibilityDuration)
        {
            _hpController.enabled = true;
            _timer = _invincibilityDuration;
        }
	}
}
