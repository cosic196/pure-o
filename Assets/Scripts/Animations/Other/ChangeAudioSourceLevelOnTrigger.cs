using UnityEngine;

public class ChangeAudioSourceLevelOnTrigger : MonoBehaviour {

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private string _trigger;
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _goalVolume;
    [SerializeField]
    private bool _useSetVolumeAsStartVolume = true;
    [SerializeField]
    private float _startVolume;
    [SerializeField]
    private bool _unscaledTime = false;
    [SerializeField]
    private bool _playOnAwake = false;

    private float _timer;

    private void Awake()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        if (_useSetVolumeAsStartVolume)
        {
            _startVolume = _audioSource.volume;
        }
        _timer = _playOnAwake ? 0f : 1f;
    }

    void Start ()
    {
        if(string.IsNullOrEmpty(_trigger))
        {
            return;
        }
        EventManager.StartListening(_trigger, () => { _timer = 0f; });
    }

    void Update()
    {
        if (_timer < 1f)
        {
            if (_unscaledTime)
            {
                _timer += _speed * Time.unscaledDeltaTime;
            }
            else
            {
                _timer += _speed * CustomTime.GetDeltaTime();
            }
            _audioSource.volume = Mathf.Lerp(_startVolume, _goalVolume, _animationCurve.Evaluate(_timer));
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
            _audioSource.volume = Mathf.Lerp(_startVolume, _goalVolume, _animationCurve.Evaluate(_timer));
        }
    }
}
