using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectEventManager : MonoBehaviour {

    private Dictionary<string, UnityEvent> _eventDictionary;
    private Dictionary<string, ParameterEvent> _parameterEventDictionary;

    private void Awake()
    {
        _eventDictionary = new Dictionary<string, UnityEvent>();
        _parameterEventDictionary = new Dictionary<string, ParameterEvent>();
    }

    public void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (_eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            _eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StartListening(string eventName, UnityAction<string> listener)
    {
        ParameterEvent thisEvent = null;
        if (_parameterEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new ParameterEvent();
            thisEvent.AddListener(listener);
            _parameterEventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StopListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (_eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public void StopListening(string eventName, UnityAction<string> listener)
    {
        ParameterEvent thisEvent = null;
        if (_parameterEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (_eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
        else
        {
            Debug.LogError(gameObject.name + " - Event manager - No listeners on event: " + eventName);
        }
    }

    public void TriggerEvent(string eventName, string eventInfo)
    {
        ParameterEvent thisEvent = null;
        if (_parameterEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventInfo);
        }
        else
        {
            Debug.LogError(gameObject.name + " - EventManager - No listeners on event: " + eventName);
        }
    }
}
