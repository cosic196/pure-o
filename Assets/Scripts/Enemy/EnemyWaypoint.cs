using UnityEngine;

public class EnemyWaypoint : MonoBehaviour {

    [SerializeField]
    private Transform _enemyTransform;
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _speed;
    private Transform _transform;
    private Quaternion _startRotation;
    private Vector3 _startPosition;
    private float _timer = 0f;
    private bool _started = false;

	void Start () {
        _transform = GetComponent<Transform>();
        _startRotation = _enemyTransform.rotation;
        _startPosition = _enemyTransform.position;
	}
	
	void Update () {
        // TODO : Remove this after testing
        if(Input.GetKeyDown(KeyCode.X))
        {
            StartMoving();
            Debug.Log("Test");
        }

		if(_started)
        {
            if(_timer < 1f)
            {
                _enemyTransform.position = Vector3.Lerp(_startPosition, _transform.position, _animationCurve.Evaluate(_timer));
                _enemyTransform.rotation = Quaternion.Lerp(_startRotation, _transform.rotation, _animationCurve.Evaluate(_timer));
                _timer += _speed * CustomTime.GetDeltaTime();
            }
            else if(_timer > 1f)
            {
                _timer = 1f;
                _enemyTransform.position = _transform.position;
                _enemyTransform.rotation = _transform.rotation;
            }
        }
	}

    public void StartMoving()
    {
        _started = true;
    }
}
