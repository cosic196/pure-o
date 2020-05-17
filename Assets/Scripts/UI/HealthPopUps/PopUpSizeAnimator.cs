using UnityEngine;

public class PopUpSizeAnimator : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _animationSpeed;

    private RectTransform _rectTransform;
    private float _timer = 0f;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        _timer += CustomTime.GetDeltaTime() * _animationSpeed;
        _rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, _animationCurve.Evaluate(_timer));
        if(_timer >= 1f)
        {
            Destroy(_rectTransform.parent.gameObject);
        }
    }
}
