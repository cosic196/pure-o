using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class ParameterEvent : UnityEvent<string> { }

public class EventManager : MonoBehaviour
{

    private Dictionary<string, UnityEvent> _eventDictionary;
    private Dictionary<string, ParameterEvent> _parameterEventDictionary;

    private static EventManager _eventManager;

    public static EventManager Instance
    {
        get
        {
            if (!_eventManager)
            {
                _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!_eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    _eventManager.Init();
                }
            }

            return _eventManager;
        }
    }

    void Init()
    {
        if (_eventDictionary == null)
        {
            _eventDictionary = new Dictionary<string, UnityEvent>();
        }

        if (_parameterEventDictionary == null)
        {
            _parameterEventDictionary = new Dictionary<string, ParameterEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance._eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StartListening(string eventName, UnityAction<string> listener)
    {
        ParameterEvent thisEvent = null;
        if (Instance._parameterEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new ParameterEvent();
            thisEvent.AddListener(listener);
            Instance._parameterEventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (_eventManager == null) return;
        UnityEvent thisEvent = null;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void StopListening(string eventName, UnityAction<string> listener)
    {
        if (_eventManager == null) return;
        ParameterEvent thisEvent = null;
        if (Instance._parameterEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
        else
        {
            Debug.LogError("EventManager - No listeners on event: " + eventName);
        }
    }

    public static void TriggerEvent(string eventName, string json)
    {
        ParameterEvent thisEvent = null;
        if (Instance._parameterEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(json);
        }
        else
        {
            Debug.LogError("EventManager - No listeners on event: " + eventName);
        }
    }
}