using UnityEngine;
using UnityEngine.UI;

public class BeatLineAnimationController : MonoBehaviour {

    private Image _image;
    private Color _goalColor, _startColor;
    [SerializeField]
    private float _appearTime;
    private float _timer = 0f;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _goalColor = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a);
        _startColor = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
    }

    void OnEnable () {
        _image.color = new Color(0f, 0f, 0f, 0f);
        _timer = 0f;
    }
	
	void Update () {
        Appear();
	}

    private void Appear()
    {
        if (_timer < 1f)
        {
            _timer += 1f / _appearTime * CustomTime.GetDeltaTime();
            _image.color = Color.Lerp(_startColor, _goalColor, _timer);
        }
        else if (_timer > 1f)
        {
            _image.color = _goalColor;
            _timer = 1f;
        }
    }
}
