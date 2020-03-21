using System.Collections.Generic;
using UnityEngine;

public class GameObjectEventTransmitter : MonoBehaviour {

    [SerializeField]
    private GameObjectEventManager _listenFrom;
    [SerializeField]
    private GameObjectEventManager _transmitTo;
    [SerializeField]
    private List<string> _events;
    [SerializeField]
    private List<string> _eventsWithParameters;

    void Start () {
        if(_listenFrom == null)
        {
            _listenFrom = GetComponent<GameObjectEventManager>();
            if(_listenFrom == null)
            {
                return;
            }
        }
        foreach (var _event in _events)
        {
            _listenFrom.StartListening(_event, () => { _transmitTo.TriggerEvent(_event); });
        }

        foreach (var _event in _eventsWithParameters)
        {
            _listenFrom.StartListening(_event, (string x) => { _transmitTo.TriggerEvent(_event, x); });
        }
	}
}
