using UnityEngine;
using UnityEngine.UI;

public class ChangeImageColorOnTrigger : MonoBehaviour {

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
    private string _triggerNext = "";

    private float _timer;
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
        _timer = 1f;
        if (_useSetColorAsStartColor)
        {
            _startColor = _image.color;
        }
        if(string.IsNullOrEmpty(_trigger))
        {
            _timer = 0f;
            return;
        }
        EventManager.StartListening(_trigger, () => { _timer = 0f; _triggerNext = ""; });
        EventManager.StartListening(_trigger, StartAndTrigger);
    }

    void StartAndTrigger(string trigger)
    {
        _timer = 0f;
        _triggerNext = trigger;
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
            _image.color = Color.Lerp(_startColor, _goalColor, _animationCurve.Evaluate(_timer));
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
            _image.color = Color.Lerp(_startColor, _goalColor, _animationCurve.Evaluate(1f));
            if(!string.IsNullOrEmpty(_triggerNext))
            {
                EventManager.TriggerEvent(_triggerNext);
            }
        }
    }
}
