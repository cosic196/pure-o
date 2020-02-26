using SonicBloom.Koreo;
using System.Collections.Generic;
using UnityEngine;

public class BeatLineSpawner : MonoBehaviour {

    [SerializeField]
    private string _poolTag;
    [SerializeField]
    private string _koreoTag;
    [SerializeField]
    private Transform _parentTransform;
    [SerializeField]
    private int _linesToSpawn;
    private List<KoreographyEvent> _events;
    private static int _eventNumber;

    void Start () {
        _eventNumber = 0;
        _linesToSpawn *= 2;
        _events = Koreographer.Instance.GetKoreographyAtIndex(0).GetTrackByID(_koreoTag).GetAllEvents();
        Koreographer.Instance.RegisterForEvents(_koreoTag, LineTrigger);
        EventManager.StartListening("KoreographerStarted", KoreographerStarted);
    }

    private void LineTrigger(KoreographyEvent koreoEvent)
    {
        if(_eventNumber >= _events.Count)
        {
            return;
        }
        Spawn();
    }

    private void KoreographerStarted()
    {
        for (int i = 0; i < _linesToSpawn && i < _events.Count; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject _beatLine = ObjectPooler.Instance.SpawnFromPool(_poolTag);
        _beatLine.GetComponent<BeatLineMovementController>().Init(_events[_eventNumber]);
        GameObject _beatLineMirrored = ObjectPooler.Instance.SpawnFromPool(_poolTag);
        _beatLineMirrored.GetComponent<BeatLineMovementController>().Init(_events[_eventNumber], -1f);

        _beatLine.SetActive(true);
        _beatLineMirrored.SetActive(true);

        _eventNumber++;
    }
}
