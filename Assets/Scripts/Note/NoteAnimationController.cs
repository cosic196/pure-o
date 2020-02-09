using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(GameObjectEventManager))]
public class NoteAnimationController : MonoBehaviour {

    private Image _image;
    private Transform _transform;
    private Color _goalColor, _startColor;
    private Vector3 _goalScale, _startScale;
    private float _appearTimer = 0f, _disappearTimer = 1f;
    [SerializeField]
    private float _appearTime;
    [SerializeField]
    private float _disappearTime;
    private GameObjectEventManager _gameObjectEventManager;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _transform = GetComponent<Transform>();
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _goalColor = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a);
        _startColor = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
        _goalScale = new Vector3(0f, _transform.localScale.y, _transform.localScale.z);
        _startScale = new Vector3(_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
    }

    void Start () {
        _gameObjectEventManager.StartListening("NoteOutOfRange", StartDisappearing);
	}

    private void OnEnable()
    {
        _image.color = new Color(0f, 0f, 0f, 0f);
        _transform.localScale = _startScale;
        _appearTimer = 0f;
        _disappearTimer = 1f;
    }

    // Update is called once per frame
    void Update () {
        Appear();
        Disappear();
	}

    private void Disappear()
    {
        if(_disappearTimer < 1f)
        {
            _disappearTimer += 1f / _disappearTime * CustomTime.GetDeltaTime();
            _transform.localScale = Vector3.Lerp(_startScale, _goalScale, _disappearTimer);
        }
        else if(_disappearTimer > 1f)
        {
            _transform.localScale = _goalScale;
            _disappearTimer = 1f;
            gameObject.SetActive(false);
        }
    }

    private void StartDisappearing()
    {
        _disappearTimer = 0f;
    }

    private void Appear()
    {
        if (_appearTimer < 1f)
        {
            _appearTimer += 1f / _appearTime * CustomTime.GetDeltaTime();
            _image.color = Color.Lerp(_startColor, _goalColor, _appearTimer);
        }
        else if (_appearTimer > 1f)
        {
            _image.color = _goalColor;
            _appearTimer = 1f;
        }
    }
}
