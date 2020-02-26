using UnityEngine;

public class ChangeCameraViewportOnTrigger : MonoBehaviour {

	[SerializeField]
    private string _trigger;
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private float _goalX;
    [SerializeField]
    private bool _useSetXAsStartX = true;
    [SerializeField]
    private float _startX;

    private float _timer;
    private Camera _camera;
    private Rect _startRect;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _startRect = _camera.rect;
        _timer = 1f;
        if (_useSetXAsStartX)
        {
            _startX = _camera.rect.x;
        }
        EventManager.StartListening(_trigger, () => { _timer = 0f; });
    }

    void Update()
    {
        if (_timer < 1f)
        {
            _timer += _animationSpeed * CustomTime.GetDeltaTime();
            _camera.rect = new Rect(Mathf.Lerp(_startX, _goalX, _animationCurve.Evaluate(_timer)), _startRect.y, _startRect.width, _startRect.height);
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
            _camera.rect = new Rect(Mathf.Lerp(_startX, _goalX, _animationCurve.Evaluate(1f)), _startRect.y, _startRect.width, _startRect.height);
        }
    }
}
