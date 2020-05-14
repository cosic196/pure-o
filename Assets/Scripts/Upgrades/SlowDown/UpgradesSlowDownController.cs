using UnityEngine;

public class UpgradesSlowDownController : MonoBehaviour
{
    [Header("Properties")]
    
    [SerializeField]
    private float _duration;
    [SerializeField]
    [Range(0f, 0.9f)]
    private float _slowDown;
    [SerializeField]
    [Range(0f, 100f)]
    private double _chanceOnNoteSpawn = 0f;
    [SerializeField]
    private int _numberOfNotesToSkip;

    [Space]

    [Header("Look & Feel")]
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _transitionSpeed;

    public static double ChanceOnNoteSpawn { get; private set; }
    public static int NumberOfNotesToSkip { get; private set; }

    private float _timer;
    private float _transitionTimer = 1f;
    private float _timeScaleBeforeSlowDown = 1f;
    private bool _slowDownEntered = false;
    private GameObjectEventManager _gameObjectEventManager;

    void Start()
    {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _timer = _duration;
        ChanceOnNoteSpawn = _chanceOnNoteSpawn;
        NumberOfNotesToSkip = _numberOfNotesToSkip * 2;
        EventManager.StartListening("UpgradesStartSlowDown", StartSlowDown);
        EventManager.StartListening("UpgradesStopSlowDown", StopSlowDown);
    }

    private void StopSlowDown()
    {
        _transitionTimer = 0f;
    }

    private void StartSlowDown()
    {
        if(!_slowDownEntered)
        {
            _timeScaleBeforeSlowDown = Time.timeScale;
            _transitionTimer = 0f;
        }
    }

    private void Update()
    {
        Transition();
        if(_timer < _duration)
        {
            _timer += CustomTime.slowDownUpgradeTimeScale * Time.unscaledDeltaTime;
        }
        else if(_timer > _duration)
        {
            _timer = _duration;
            StopSlowDown();
        }
    }

    private void Transition()
    {
        if(_transitionTimer < 1f)
        {
            _transitionTimer += CustomTime.slowDownUpgradeTimeScale * Time.unscaledDeltaTime;
            if(_slowDownEntered)
            {
                Time.timeScale = Mathf.Lerp(_timeScaleBeforeSlowDown - _slowDown, _timeScaleBeforeSlowDown, _animationCurve.Evaluate(_transitionTimer));
            }
            else
            {
                Time.timeScale = Mathf.Lerp(_timeScaleBeforeSlowDown, _timeScaleBeforeSlowDown - _slowDown, _animationCurve.Evaluate(_transitionTimer));
            }
            _gameObjectEventManager.TriggerEvent("Animate", _animationCurve.Evaluate(_transitionTimer) + "," + _slowDownEntered);
        }
        else if(_transitionTimer > 1f)
        {
            _transitionTimer = 1f;
            if(!_slowDownEntered)
            {
                Time.timeScale = _timeScaleBeforeSlowDown - _slowDown;
                _timer = 0f;
                _slowDownEntered = true;
            }
            else
            {
                Time.timeScale = _timeScaleBeforeSlowDown;
                _slowDownEntered = false;
            }
            _gameObjectEventManager.TriggerEvent("Animate", _animationCurve.Evaluate(_transitionTimer) + "," + !_slowDownEntered);
        }
        else
        {
            if(_slowDownEntered && Time.timeScale != _timeScaleBeforeSlowDown - _slowDown)
            {
                Time.timeScale = _timeScaleBeforeSlowDown - _slowDown;
                _gameObjectEventManager.TriggerEvent("Animate", _animationCurve.Evaluate(_transitionTimer) + "," + !_slowDownEntered);
            }
            else if(Time.timeScale != _timeScaleBeforeSlowDown)
            {
                Time.timeScale = _timeScaleBeforeSlowDown;
                _gameObjectEventManager.TriggerEvent("Animate", _animationCurve.Evaluate(_transitionTimer) + "," + !_slowDownEntered);
            }
        }
    }
}
