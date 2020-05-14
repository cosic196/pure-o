using UnityEngine;

public class UpgradesSlowDownActivateController : MonoBehaviour
{
    [Header("Properties")]

    [SerializeField]
    [Range(0f, 0.9f)]
    private float _slowDown;
    [SerializeField]
    [Range(0f, 100f)]
    private double _hpCost;

    [Space]

    [Header("Look & Feel")]
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _transitionSpeed;

    private float _transitionTimer = 1f;
    private float _timeScaleBeforeSlowDown = 1f;
    private GameObjectEventManager _gameObjectEventManager;
    private bool _slowDownActivated = false;
    private int _timerAdditionMultiplier = 0;

    void Start()
    {
        _timeScaleBeforeSlowDown = Time.timeScale;
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        EventManager.StartListening("PressedUpgrade", ConfigureSlowDown);
        EventManager.StartListening("UpgradesStopSlowDown", StopSlowDown);
    }

    private void StopSlowDown()
    {
        _timerAdditionMultiplier = 1;
        _slowDownActivated = false;
    }

    private void StartSlowDown()
    {
        _timeScaleBeforeSlowDown = Time.timeScale;
        _slowDownActivated = true;
        _timerAdditionMultiplier = -1;
    }

    private void ConfigureSlowDown()
    {
        if (!_slowDownActivated)
        {
            StartSlowDown();
        }
        else
        {
            StopSlowDown();
        }
    }

    private void Update()
    {
        Transition();
        DepleteHp();
    }

    private void DepleteHp()
    {
        if(_slowDownActivated)
        {
            EventManager.TriggerEvent("DamagePlayerByInput", _hpCost.ToString());
        }
    }

    private void Transition()
    {
        if(_transitionTimer < 0f)
        {
            _transitionTimer = 0f;
            _timerAdditionMultiplier = 0;
            Time.timeScale = Mathf.Lerp(_timeScaleBeforeSlowDown - _slowDown, _timeScaleBeforeSlowDown, _animationCurve.Evaluate(_transitionTimer));
            _gameObjectEventManager.TriggerEvent("Animate", _animationCurve.Evaluate(_transitionTimer).ToString());
        }
        else if(_transitionTimer > 1f)
        {
            _transitionTimer = 1f;
            _timerAdditionMultiplier = 0;
            Time.timeScale = Mathf.Lerp(_timeScaleBeforeSlowDown - _slowDown, _timeScaleBeforeSlowDown, _animationCurve.Evaluate(_transitionTimer));
            _gameObjectEventManager.TriggerEvent("Animate", _animationCurve.Evaluate(_transitionTimer).ToString());
        }
        else if(_transitionTimer != 1f && _transitionTimer != 0f)
        {
            Time.timeScale = Mathf.Lerp(_timeScaleBeforeSlowDown - _slowDown, _timeScaleBeforeSlowDown, _animationCurve.Evaluate(_transitionTimer));
            _gameObjectEventManager.TriggerEvent("Animate", _animationCurve.Evaluate(_transitionTimer).ToString());
        }
        _transitionTimer += CustomTime.slowDownUpgradeTimeScale * Time.unscaledDeltaTime * _timerAdditionMultiplier;
    }
}
