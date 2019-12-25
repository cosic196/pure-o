using UnityEngine;

public class MovementController : MonoBehaviour {

    private Transform _transform;
    private Vector3 _startPos, _leftPos, _rightPos, _goalPos, _lastPos;
    [SerializeField]
    private float _moveDistance = 1.5f;
    private Vector3 _startRotation, _leftRotation, _rightRotation, _currentRotation, _goalRotation, _rotationCalculator;
    [SerializeField]
    private float _rotation = 11f;
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _animationSpeed;
    private float _timer = 1f;

    // Use this for initialization
    void Awake () {
        _transform = GetComponent<Transform>();
        _startPos = _transform.position;
        _leftPos = new Vector3(_startPos.x - _moveDistance, _startPos.y, _startPos.z);
        _rightPos = new Vector3(_startPos.x + _moveDistance, _startPos.y, _startPos.z);
        _goalPos = _startPos;
        _lastPos = _startPos;

        _startRotation = _transform.eulerAngles;
        _currentRotation = _startRotation;
        _goalRotation = _startRotation;
        _rotationCalculator = _goalRotation;
        _leftRotation = new Vector3(0f, _startRotation.y + _rotation, 0f);
        _rightRotation = new Vector3(0f, _startRotation.y - _rotation, 0f);

        EventManager.StartListening("PressedRight", MoveRight);
        EventManager.StartListening("PressedLeft", MoveLeft);
        EventManager.StartListening("PressedCenter", MoveCenter);
    }
	
	// Update is called once per frame
	void Update () {
        _transform.position = Vector3.Lerp(_lastPos, _goalPos, _animationCurve.Evaluate(_timer));
        _rotationCalculator.y = Mathf.LerpAngle(_currentRotation.y, _goalRotation.y, _animationCurve.Evaluate(_timer));
        _transform.eulerAngles = _rotationCalculator;

        if (_timer < 1f)
        {
            _timer += CustomTime.GetDeltaTime() * _animationSpeed;
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
        }
    }

    private void MoveLeft()
    {
        _lastPos = _transform.position;
        _goalPos = _leftPos;

        _currentRotation = _transform.eulerAngles;
        _goalRotation = _leftRotation;
        _timer = 0f;
    }

    private void MoveRight()
    {
        _lastPos = _transform.position;
        _goalPos = _rightPos;

        _currentRotation = _transform.eulerAngles;
        _goalRotation = _rightRotation;
        _timer = 0f;
    }

    private void MoveCenter()
    {
        _lastPos = _transform.position;
        _goalPos = _startPos;

        _currentRotation = _transform.eulerAngles;
        _goalRotation = _startRotation;
        _timer = 0f;
    }
}
