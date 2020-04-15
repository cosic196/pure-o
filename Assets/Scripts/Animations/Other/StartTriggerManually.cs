using System.Collections.Generic;
using UnityEngine;

public class StartTriggerManually : MonoBehaviour {

    [SerializeField]
    private bool _onEnable = true;
    [SerializeField]
    private bool _onDisable = true;
    [SerializeField]
    private string _singleTrigger;
    [SerializeField]
    private List<string> _triggerSeries;
    private int _counter = 0;

    private void OnEnable()
    {
        if(!_onEnable)
        {
            return;
        }
        TriggerSingle();
        TriggerSeries();
    }

    private void TriggerSeries()
    {
        if(_triggerSeries.Count > 0)
        {
            if(_counter < _triggerSeries.Count)
            {
                if(!string.IsNullOrEmpty(_triggerSeries[_counter]))
                    EventManager.TriggerEvent(_triggerSeries[_counter]);
                _counter++;
            }
        }
    }

    private void OnDisable()
    {
        if (!_onDisable)
        {
            return;
        }
        TriggerSingle();
        TriggerSeries();
    }

    private void TriggerSingle()
    {
        if (!string.IsNullOrEmpty(_singleTrigger))
        {
            EventManager.TriggerEvent(_singleTrigger);
        }
    }
}
