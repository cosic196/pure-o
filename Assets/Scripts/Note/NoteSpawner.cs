using SonicBloom.Koreo;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour {
    
    [SerializeField]
    private string _poolTag;
    [SerializeField]
    private string _koreoTag;
    [SerializeField]
    private Transform _parentTransform;
    [SerializeField]
    private float _sideNoteXOffset;
    [SerializeField]
    private int _notesToSpawn;
    private List<KoreographyEvent> _events;
    private static int _eventNumber;

    void Start () {
        _eventNumber = 0;
        _notesToSpawn *= 2;
        _events = Koreographer.Instance.GetKoreographyAtIndex(0).GetTrackByID(_koreoTag).GetAllEvents();
        Koreographer.Instance.RegisterForEvents(_koreoTag, NoteTrigger);
        EventManager.StartListening("KoreographerStarted", KoreographerStarted);
    }

    private void SpawnNoteCenter()
    {
        SpawnNote(0f, ShootController.Position.Center);
    }

    private void SpawnNoteLeft()
    {
        SpawnNote(-_sideNoteXOffset, ShootController.Position.Left);
    }

    private void SpawnNoteRight()
    {
        SpawnNote(_sideNoteXOffset, ShootController.Position.Right);
    }

    private void SpawnNote(float xOffset, ShootController.Position notePosition)
    {
        //Instantiate note
        GameObject spawnedNote = ObjectPooler.Instance.SpawnFromPool(_poolTag);
        //Set transform position
        Transform spawnedNoteTransform = spawnedNote.GetComponent<Transform>();
        NoteCollisionController spawnedNoteCollisionController = spawnedNote.GetComponent<NoteCollisionController>();
        spawnedNoteCollisionController.Init(new NoteInfo {id = _eventNumber, position = notePosition });
        spawnedNoteTransform.localPosition = GetLocalPosition(xOffset);
        //Set eventId
        spawnedNote.GetComponent<NoteMovementController>().Init(_events[_eventNumber]);

        //Do the same for the double with inversed transform
        GameObject spawnedNoteDouble = ObjectPooler.Instance.SpawnFromPool(_poolTag);
        Transform spawnedNoteDoubleTransform = spawnedNoteDouble.GetComponent<Transform>();
        spawnedNoteDoubleTransform.localPosition = Vector3.Scale(spawnedNoteTransform.localPosition, new Vector3(1,-1,1));
        spawnedNoteDouble.GetComponent<NoteCollisionController>().Init(spawnedNoteCollisionController.NoteInfo, true);
        spawnedNoteDouble.GetComponent<NoteMovementController>().Init(_events[_eventNumber], -1f);

        _eventNumber++;

        //Activate notes
        spawnedNote.SetActive(true);
        spawnedNoteDouble.SetActive(true);
    }
    
    private void NoteTrigger(KoreographyEvent ev)
    {
        if(_events.Count <= _eventNumber)
        {
            return;
        }
        if (!_events[_eventNumber].HasIntPayload())
        {
            Debug.LogError("Invalid event payload on a note!");
            return;
        }
        if (_events[_eventNumber].GetIntValue() == -1)
        {
            SpawnNoteLeft();
        }
        else if (_events[_eventNumber].GetIntValue() == 0)
        {
            SpawnNoteCenter();
        }
        else if (_events[_eventNumber].GetIntValue() == 1)
        {
            SpawnNoteRight();
        }
    }

    private void KoreographerStarted()
    {
        for (int i = 0; i < _notesToSpawn && i < _events.Count; i++)
        {
            if (!_events[i].HasIntPayload())
            {
                Debug.LogError("Invalid event payload on a note!");
                return;
            }
            if (_events[i].GetIntValue() == -1)
            {
                SpawnNoteLeft();
            }
            else if (_events[i].GetIntValue() == 0)
            {
                SpawnNoteCenter();
            }
            else if (_events[i].GetIntValue() == 1)
            {
                SpawnNoteRight();
            }
        }
    }

    private Vector3 GetLocalPosition(float xOffset)
    {
        return new Vector3(xOffset, 0);
    }
}
