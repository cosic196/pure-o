using System.Collections.Generic;
using UnityEngine;

public class EnableComponentOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _triggerName;
    [SerializeField]
    private List<MonoBehaviour> _behavioursToEnable;
    [SerializeField]
    private float _delay = 0f;
    [SerializeField]
    private bool _useUnscaledTime = true;
    private float _timer;

    void Start()
    {
        _timer = _delay;
        EventManager.StartListening(_triggerName, StartEnable);
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
                foreach (var obj in _behavioursToEnable)
                {
                    obj.enabled = true;
                }
            }
        }
    }

    private void StartEnable()
    {
        if (_delay <= 0)
        {
            foreach (var obj in _behavioursToEnable)
            {
                obj.enabled = true;
            }
        }
        else
        {
            _timer = 0f;
        }
    }
}
