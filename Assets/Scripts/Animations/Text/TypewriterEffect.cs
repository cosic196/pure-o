using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TypewriterEffect : MonoBehaviour {

    [SerializeField]
    private string _eventName;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private bool _useUnscaledTime = true;
    [SerializeField]
    private bool _playOnAwake = false;
    private TextMeshProUGUI _tmPro;
    private float _timer = -1f;

    private void Awake()
    {
        _tmPro = GetComponent<TextMeshProUGUI>();
        _tmPro.maxVisibleCharacters = 0;
    }

    void Start () {
        if (string.IsNullOrEmpty(_eventName))
        {
            _timer = 0f;
        }
        else
        {
            if (_playOnAwake)
            {
                _timer = 0f;
            }
            EventManager.StartListening(_eventName, Play);
        }
	}

    public void Play()
    {
        _timer = 0f;
        _tmPro.maxVisibleCharacters = 0;
    }
	
	void Update () {
		if(_tmPro.maxVisibleCharacters < _tmPro.text.Length)
        {
            if(_timer >= 0f)
            {
                if(_useUnscaledTime)
                {
                    _timer += _speed * Time.unscaledDeltaTime;
                }
                else
                {
                    _timer += _speed * CustomTime.GetDeltaTime();
                }
                if(_timer >= 1f)
                {
                    _timer = 0f;
                    _tmPro.maxVisibleCharacters++;
                }
            }
        }
	}
}
