using UnityEngine;

public class AssignTransformOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _trigger;
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private Transform _goalTransform;
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private bool _playOnAwake = false;

    private Transform _transform, _currentTransform;
    private float _timer = 1f;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _currentTransform = _transform;
    }

    void Start () {
        if(_playOnAwake)
        {
            StartAnimation();
        }
        if(!string.IsNullOrEmpty(_trigger))
        {
            EventManager.StartListening(_trigger, StartAnimation);
        }
	}

    private void StartAnimation()
    {
        _timer = 0f;
        _currentTransform = _transform;
    }
    
    void Update () {
        if (_timer < 1f)
        {
            _transform.position = Vector3.Lerp(_currentTransform.position, _goalTransform.position, _animationCurve.Evaluate(_timer));
            _transform.rotation = Quaternion.Lerp(_currentTransform.rotation, _goalTransform.rotation, _animationCurve.Evaluate(_timer));
            _timer += _speed * CustomTime.GetDeltaTime();
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
            _transform.position = _goalTransform.position;
            _transform.rotation = _goalTransform.rotation;
        }
    }
}
