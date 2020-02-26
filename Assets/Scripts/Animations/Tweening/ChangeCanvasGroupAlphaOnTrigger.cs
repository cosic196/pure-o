using UnityEngine;

public class ChangeCanvasGroupAlphaOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _trigger;
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private float _goalAlpha;
    [SerializeField]
    private bool _useSetAlphaAsStartAlpha = true;
    [SerializeField]
    private float _startAlpha;
    [SerializeField]
    private bool _unscaledTime = false;

    private float _timer;
    private CanvasGroup _canvasGroup;

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _timer = 1f;
        if (_useSetAlphaAsStartAlpha)
        {
            _startAlpha = _canvasGroup.alpha;
        }
        EventManager.StartListening(_trigger, () => { _timer = 0f; });
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
            _canvasGroup.alpha = Mathf.Lerp(_startAlpha, _goalAlpha, _animationCurve.Evaluate(_timer));
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
            _canvasGroup.alpha = Mathf.Lerp(_startAlpha, _goalAlpha, _animationCurve.Evaluate(1f));
        }
    }
}
