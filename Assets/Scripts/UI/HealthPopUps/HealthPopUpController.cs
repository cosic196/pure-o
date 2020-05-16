using TMPro;
using UnityEngine;

public class HealthPopUpController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _numberTMPro;

    private RectTransform _transform;
    private Transform _transformToFollow;
    private CanvasGroup _canvasGroup;

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnGUI()
    {
        _transform.position = Camera.main.WorldToScreenPoint(_transformToFollow.position);
        if(_transform.position.z < 0f)
        {
            _canvasGroup.alpha = 0f;
        }
        else
        {
            _canvasGroup.alpha = 1f;
        }
    }

    public void Init(string amount, Transform transformToFollow)
    {
        _transform = GetComponent<RectTransform>();
        if (float.Parse(amount) < 10f)
        {
            _transform.localScale = Vector3.one / 1.25f;
        }
        _numberTMPro.text = Mathf.RoundToInt(float.Parse(amount)).ToString();
        _transformToFollow = transformToFollow;
    }
}
