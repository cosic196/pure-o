using UnityEngine;
using UnityEngine.UI;

public class HpBarAnimation : MonoBehaviour {

    private Slider _slider;

    [SerializeField]
    private Image _fill;
    [SerializeField]
    private Color _flashColor;
    [SerializeField]
    private AnimationCurve _flashCurve;
    [SerializeField]
    private float _flashSpeed;
    private Color _startColor;
    private float _timer;

	void Start () {
        _slider = GetComponent<Slider>();
        _startColor = _fill.color;
        _timer = 1f;
        EventManager.StartListening("HpChanged", ChangeFill);
        EventManager.StartListening("PlayerDamaged", () => { _timer = 0f; });
	}

    private void ChangeFill(string hpSlashMaxHp)
    {
        float currentHp = float.Parse(hpSlashMaxHp.Split('/')[0]);
        float maxHp = float.Parse(hpSlashMaxHp.Split('/')[1]);
        _slider.normalizedValue = currentHp / maxHp;
    }

    private void Update()
    {
        if(_timer < 1f)
        {
            _timer += _flashSpeed * CustomTime.GetDeltaTime();
            _fill.color = Color.Lerp(_flashColor, _startColor, _flashCurve.Evaluate(_timer));
        }
        else if(_timer > 1f)
        {
            _timer = 1f;
            _fill.color = _startColor;
        }
    }
}
