using UnityEngine;

[RequireComponent(typeof(GameObjectEventManager))]
public class MoveOnLocalTrigger : MonoBehaviour {

    [SerializeField]
    private string _trigger;
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private Vector3 _goalPosition;
    [SerializeField]
    private bool _useSetPositionAsStartPosition = true;
    [SerializeField]
    private Vector3 _startPosition;

    private float _timer;
    private Transform _transform;
    private GameObjectEventManager _gameObjectEventManager;

    void Start()
    {
        _transform = GetComponent<Transform>();
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _timer = 1f;
        if (_useSetPositionAsStartPosition)
        {
            _startPosition = _transform.localPosition;
        }
        _gameObjectEventManager.StartListening(_trigger, () => { _timer = 0f; });
    }

    void Update()
    {
        if (_timer < 1f)
        {
            _timer += _animationSpeed * CustomTime.GetDeltaTime();
            _transform.localPosition = Vector3.Lerp(_startPosition, _goalPosition, _animationCurve.Evaluate(_timer));
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
            _transform.localPosition = Vector3.Lerp(_startPosition, _goalPosition, _animationCurve.Evaluate(1f));
        }
    }
}
