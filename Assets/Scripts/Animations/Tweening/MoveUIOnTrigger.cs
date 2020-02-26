using UnityEngine;

public class MoveUIOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _trigger;
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private Vector3 _goalPosition;
    [SerializeField]
    private bool _useSetPositionAsStartPosition = true;
    [SerializeField]
    private Vector3 _startPosition;
    [SerializeField]
    private bool _unscaledTime = false;

    private float _timer;
    private RectTransform _transform;

    void Start()
    {
        _transform = GetComponent<RectTransform>();
        _timer = 1f;
        if (_useSetPositionAsStartPosition)
        {
            _startPosition = _transform.anchoredPosition;
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
            _transform.anchoredPosition = Vector3.Lerp(_startPosition, _goalPosition, _animationCurve.Evaluate(_timer));
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
            _transform.anchoredPosition = Vector3.Lerp(_startPosition, _goalPosition, _animationCurve.Evaluate(1f));
        }
    }
}
