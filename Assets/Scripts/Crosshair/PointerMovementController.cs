using UnityEngine;

public class PointerMovementController : MonoBehaviour {

    private Transform _transform;
    private Vector3 _startPosition, _currentPosition, _goalPosition;
    [SerializeField]
    private Vector3 _leftPosition;
    [SerializeField]
    private Vector3 _rightPosition;
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _animationSpeed;
    private float _timer = 1f;

    void Start () {
        _transform = GetComponent<Transform>();

        _startPosition = _transform.localPosition;
        _currentPosition = _startPosition;
        _goalPosition = _startPosition;

        EventManager.StartListening("PressedRight", MoveRight);
        EventManager.StartListening("PressedLeft", MoveLeft);
        EventManager.StartListening("PressedCenter", MoveCenter);
    }
	
	void Update () {
        _transform.localPosition = Vector3.Lerp(_currentPosition, _goalPosition, _animationCurve.Evaluate(_timer));

        if (_timer < 1f)
        {
            _timer += Time.deltaTime * Time.timeScale * _animationSpeed;
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
        }
    }

    private void MoveLeft()
    {
        _currentPosition = _transform.localPosition;
        _goalPosition = _leftPosition;
        _timer = 0f;
    }

    private void MoveRight()
    {
        _currentPosition = _transform.localPosition;
        _goalPosition = _rightPosition;
        _timer = 0f;
    }

    private void MoveCenter()
    {
        _currentPosition = _transform.localPosition;
        _goalPosition = _startPosition;
        _timer = 0f;
    }
}
