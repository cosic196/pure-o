using System.Collections.Generic;
using UnityEngine;

public class DisableComponentOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _triggerName;
    [SerializeField]
    private List<MonoBehaviour> _behavioursToDisable;
    [SerializeField]
    private float _delay = 0f;
    [SerializeField]
    private bool _useUnscaledTime = true;
    private float _timer;

    void Start()
    {
        _timer = _delay;
        EventManager.StartListening(_triggerName, StartDisable);
    }

    void Update()
    {
        if (_timer < _delay)
        {
            if (_useUnscaledTime)
            {
                _timer += Time.unscaledDeltaTime;
            }
            else
            {
                _timer += CustomTime.GetDeltaTime();
            }
            if (_timer >= _delay)
            {
                foreach (var obj in _behavioursToDisable)
                {
                    obj.enabled = false;
                }
            }
        }
    }

    private void StartDisable()
    {
        if (_delay <= 0)
        {
            foreach (var obj in _behavioursToDisable)
            {
                obj.enabled = false;
            }
        }
        else
        {
            _timer = 0f;
        }
    }
}
