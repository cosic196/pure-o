using UnityEngine;

public class RotateOnTrigger : MonoBehaviour {

    [SerializeField]
    private float _threshold;
    [SerializeField]
    private string _trigger;
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private Quaternion _goalRotation;
    [SerializeField]
    private bool _useSetPositionAsStartPosition = true;
    [SerializeField]
    private Quaternion _startRotation;
    [SerializeField]
    private bool _unscaledTime = false;
    private FpsCameraMovementController _cameraController;

    private float _timer;
    private Transform _transform;

    void Start()
    {
        _cameraController = GetComponent<FpsCameraMovementController>();
        _transform = GetComponent<Transform>();
        _timer = 1f;
        if (_useSetPositionAsStartPosition)
        {
            _startRotation = _transform.localRotation;
        }
        EventManager.StartListening(_trigger, Rotate);
    }

    private void Rotate(string checkIfPositive)
    {
        if(float.Parse(checkIfPositive)>0)
        {
            if(_transform.localEulerAngles.x > _threshold || _transform.localEulerAngles.y > _threshold)
            {
                if(_transform.localEulerAngles.x < 360f - _threshold || _transform.localEulerAngles.y < 360f - _threshold)
                {
                    _cameraController.enabled = false;
                    _timer = 0f;
                    _startRotation = _transform.localRotation;
                }
            }
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
            _transform.localRotation = Quaternion.Slerp(_startRotation, _goalRotation, _animationCurve.Evaluate(_timer));
            if(_timer == 1f)
            {
                _cameraController.enabled = true;
            }
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
            _transform.localRotation = Quaternion.Slerp(_startRotation, _goalRotation, _animationCurve.Evaluate(1f));
            _cameraController.enabled = true;
        }
    }
}
