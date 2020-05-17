using TMPro;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _numberTMPro;

    private RectTransform _transform;
    private Vector3 _spawnPosition;
    private CanvasGroup _canvasGroup;

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnGUI()
    {
        _transform.position = Camera.main.WorldToScreenPoint(_spawnPosition);
        if(_transform.position.z < 0f)
        {
            _canvasGroup.alpha = 0f;
        }
        else
        {
            _canvasGroup.alpha = 1f;
        }
    }

    public void Init(string popUpText, Vector3 spawnPosition)
    {
        _transform = GetComponent<RectTransform>();
        _numberTMPro.text = popUpText;
        _spawnPosition = spawnPosition;
    }
}
