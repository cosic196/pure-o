using UnityEngine;

public class ResizeOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _trigger;
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private Vector3 _goalSize;
    [SerializeField]
    private bool _useSetSizeAsStartSize = true;
    [SerializeField]
    private Vector3 _startSize;
    [SerializeField]
    private bool _unscaledTime = false;

    private float _timer;
    private Transform _transform;

	void Start () {
        _transform = GetComponent<Transform>();
        _timer = 1f;
        if(_useSetSizeAsStartSize)
        {
            _startSize = _transform.localScale;
        }
        if(string.IsNullOrEmpty(_trigger))
        {
            _timer = 0f;
        }
        else
        {
            EventManager.StartListening(_trigger, () => { _timer = 0f; });
        }
	}
	
	void Update () {
		if(_timer < 1f)
        {
            if(_unscaledTime)
            {
                _timer += _animationSpeed * Time.unscaledDeltaTime;
            }
            else
            {
                _timer += _animationSpeed * CustomTime.GetDeltaTime();
            }
            _transform.localScale = Vector3.Lerp(_startSize, _goalSize, _animationCurve.Evaluate(_timer));
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
            _transform.localScale = Vector3.Lerp(_startSize, _goalSize, _animationCurve.Evaluate(1f));
        }
	}
}
