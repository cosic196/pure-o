using UnityEngine;

public class CrosshairAnimationController : MonoBehaviour {

    private Transform _transform;
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _rotateDegrees;
    [SerializeField]
    private float _speed;
    private float _timer = 1f, _currentRotation, _goalRotation;

	void Start () {
        _transform = GetComponent<Transform>();
        _currentRotation = _transform.localRotation.z;
        _goalRotation = _currentRotation;
        EventManager.StartListening("Shot", Rotate);
	}
	
	void Update () {
        if(_timer < 1f)
        {
            _timer += _speed * CustomTime.GetDeltaTime();
            _transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(_currentRotation, _goalRotation, _timer));
        }
        else if(_timer > 1f)
        {
            _timer = 1f;
            _transform.localRotation = Quaternion.Euler(0f, 0f, _goalRotation);
        }
	}

    private void Rotate()
    {
        _goalRotation = _currentRotation + _rotateDegrees;
        _timer = 0f;
    }
}
