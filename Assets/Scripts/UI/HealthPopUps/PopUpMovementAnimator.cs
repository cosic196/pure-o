using UnityEngine;

public class PopUpMovementAnimator : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _acceleration;
    [SerializeField]
    private float _accelerationDuration;
    [SerializeField]
    private float _maxSpeed;

    private RectTransform _rectTransform;
    private float _timer = 0f;
    private Vector2 _direction;

    void Start()
    {
        var rand = Random.insideUnitCircle.normalized;
        _direction = new Vector2(rand.x, Mathf.Abs(rand.y));
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if(_timer < 1f)
        {
            _timer += CustomTime.GetDeltaTime() / _accelerationDuration;
        }
        _rectTransform.Translate(_direction * _acceleration.Evaluate(_timer) * _maxSpeed);
    }
}
