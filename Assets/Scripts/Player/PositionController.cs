using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour {

    [SerializeField]
    private Transform _movePointsParent;
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
        for(int i = 0; i < _movePointsParent.childCount; i++)
        {
            _movePoints.Enqueue(_movePointsParent.GetChild(i));
        }
        _transform = GetComponent<Transform>();
        _goalTransform = _transform;
        _currentTransform = _transform;
        EventManager.StartListening("MoveToNextPoint", MoveToNextPoint);
        EventManager.StartListening("MoveToNextPointSpan", MoveToNextPointSpan);
        EventManager.StartListening("MoveToNextPointSpanEnd", MoveToNextPointSpanEnd);
        EventManager.StartListening("MoveToNextPointSpanStart", MoveToNextPointSpanStart);
    }

    private void MoveToNextPoint(string speed)
    {
        if(_movePoints.Count > 0)
        {
            _speed = Mathf.Abs(float.Parse(speed));
            _timer = 0f;
            _currentTransform = _goalTransform;
            _goalTransform = _movePoints.Dequeue();
        }
    }
    
    private void MoveToNextPointSpanStart(string curvePoint)
    {
        float t = float.Parse(curvePoint);
        _currentTransform = _goalTransform;
        _goalTransform = _movePoints.Dequeue();
        _transform.position = Vector3.Lerp(_currentTransform.position, _goalTransform.position, t);
        _transform.rotation = Quaternion.Lerp(_currentTransform.rotation, _goalTransform.rotation, t);
    }

    private void MoveToNextPointSpan(string curvePoint)
    {
        float t = float.Parse(curvePoint);
        _transform.position = Vector3.Lerp(_currentTransform.position, _goalTransform.position, t);
        _transform.rotation = Quaternion.Lerp(_currentTransform.rotation, _goalTransform.rotation, t);
    }
    
    private void MoveToNextPointSpanEnd()
    {
        _transform.position = _goalTransform.position;
        _transform.rotation = _goalTransform.rotation;
    }

    void Update () {
        if(_timer == 1f)
        {
            return;
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
