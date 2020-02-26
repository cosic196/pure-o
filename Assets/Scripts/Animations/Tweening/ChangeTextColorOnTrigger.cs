using TMPro;
using UnityEngine;

public class ChangeTextColorOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _trigger;
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private Color _goalColor;
    [SerializeField]
    private bool _useSetColorAsStartColor = true;
    [SerializeField]
    private Color _startColor;
    [SerializeField]
    private bool _unscaledTime = false;

    private float _timer;
    private TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _timer = 1f;
        if (_useSetColorAsStartColor)
        {
            _startColor = _text.color;
        }
        if (string.IsNullOrEmpty(_trigger))
        {
            _timer = 0f;
        }
        else
        {
            EventManager.StartListening(_trigger, () => { _timer = 0f; });
        }
    }

    void Update()
    {
        if (_timer < 1f)
        {
            if (_unscaledTime)
            {
                _timer += _animationSpeed * Time.unscaledDeltaTime;
            }
            else
            {
                _timer += _animationSpeed * CustomTime.GetDeltaTime();
            }
            _text.color = Color.Lerp(_startColor, _goalColor, _animationCurve.Evaluate(_timer));
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
            _text.color = Color.Lerp(_startColor, _goalColor, _animationCurve.Evaluate(1f));
        }
    }
}
