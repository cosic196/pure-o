using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour {

    [SerializeField]
    private List<Transform> _movePointsList;
    private Queue<Transform> _movePoints;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private AnimationCurve _animationCurve;
    private float _timer = 1f;
    private Transform _transform;
    private Transform _goalTransform, _currentTransform;

	void Start () {
        _movePoints = new Queue<Transform>();
        foreach(var point in _movePointsList)
        {
            _movePoints.Enqueue(point);
        }
        _transform = GetComponent<Transform>();
        _goalTransform = _transform;
        _currentTransform = _transform;
        EventManager.StartListening("MoveToNextPoint", MoveToNextPoint);
	}

    private void MoveToNextPoint()
    {
        if(_movePoints.Count > 0)
        {
            _timer = 0f;
            _currentTransform = _goalTransform;
            _goalTransform = _movePoints.Dequeue();
        }
    }

    void Update () {
        //TODO remove after testing
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.TriggerEvent("MoveToNextPoint");
        }

        _transform.position = Vector3.Lerp(_currentTransform.position, _goalTransform.position, _animationCurve.Evaluate(_timer));
        _transform.rotation = Quaternion.Lerp(_currentTransform.rotation, _goalTransform.rotation, _animationCurve.Evaluate(_timer));
        if(_timer < 1f)
        {
            _timer += _speed * CustomTime.GetDeltaTime();
        }
        else if(_timer > 1f)
        {
            _timer = 1f;
            _transform.position = _goalTransform.position;
            _transform.rotation = _goalTransform.rotation;
        }
	}
}
