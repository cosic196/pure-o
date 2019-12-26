using UnityEngine;
using UnityEngine.UI;

public class XAnimationController : MonoBehaviour {

    private Image _image;
    private Transform _transform;
    private Color _goalColor, _startColor;
    [SerializeField]
    ShootController.Position position;
    [SerializeField]
    private float _animationDuration;
    [SerializeField]
    private AnimationCurve _animationCurve;
    private float _timer = 1f;

    void Start () {
        _image = GetComponent<Image>();
        _goalColor = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a);
        _startColor = new Color(_image.color.r, _image.color.g, _image.color.b, 0);

        _image.color = new Color(0f, 0f, 0f, 0f);
        EventManager.StartListening("NoteOutOfRange", StartAnimation);
    }

    void Update()
    {
        Appear();
    }

    private void StartAnimation(string noteInfoJson)
    {
        NoteInfo noteInfo = JsonUtility.FromJson<NoteInfo>(noteInfoJson);
        if(noteInfo.position == position)
        {
            _timer = 0f;
        }
    }

    private void Appear()
    {
        if (_timer < 1f)
        {
            _image.color = Color.Lerp(_startColor, _goalColor, _animationCurve.Evaluate(_timer));
            _timer += 1f / _animationDuration * CustomTime.GetDeltaTime();
        }
        else if (_timer > 1f)
        {
            _image.color = _startColor;
            _timer = 1f;
        }
    }
}
