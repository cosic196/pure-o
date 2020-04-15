using SonicBloom.Koreo;
using System.Collections.Generic;
using UnityEngine;

public class IntroNoteSpawner : MonoBehaviour
{

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
    public ShootController.Position _spawnPosition = ShootController.Position.Center;

    void Start()
    {
        _eventNumber = 8;
        _notesToSpawn *= 2;
        _events = Koreographer.Instance.GetKoreographyAtIndex(0).GetTrackByID(_koreoTag).GetAllEvents();
        Koreographer.Instance.RegisterForEvents(_koreoTag, NoteTrigger);
        EventManager.StartListening("KoreographerStarted", KoreographerStarted);
        EventManager.StartListening("StartSpawningRight", () => { _spawnPosition = ShootController.Position.Right; });
        EventManager.StartListening("StartSpawningLeft", () => { _spawnPosition = ShootController.Position.Left; });
    }

    private void SpawnNoteCenter()
    {
        SpawnNote(0f);
        EventManager.TriggerEvent("SpawnedNoteCenter");
    }

    private void SpawnNoteLeft()
    {
        SpawnNote(-_sideNoteXOffset);
        EventManager.TriggerEvent("SpawnedNoteLeft");
    }

    private void SpawnNoteRight()
    {
        SpawnNote(_sideNoteXOffset);
        EventManager.TriggerEvent("SpawnedNoteRight");
    }

    private void SpawnNote(float xOffset)
    {
        //Instantiate note
        GameObject spawnedNote = ObjectPooler.Instance.SpawnFromPool(_poolTag);
        //Set transform position
        Transform spawnedNoteTransform = spawnedNote.GetComponent<Transform>();
        IntroNoteCollisionController spawnedNoteCollisionController = spawnedNote.GetComponent<IntroNoteCollisionController>();
        spawnedNoteCollisionController.Init(new NoteInfo { id = _eventNumber, position = _spawnPosition });
        spawnedNoteTransform.localPosition = GetLocalPosition(xOffset);
        //Set eventId
        spawnedNote.GetComponent<IntroNoteMovementController>().Init(_events[_eventNumber], xOffset, _sideNoteXOffset);

        //Do the same for the double with inversed transform
        GameObject spawnedNoteDouble = ObjectPooler.Instance.SpawnFromPool(_poolTag);
        Transform spawnedNoteDoubleTransform = spawnedNoteDouble.GetComponent<Transform>();
        spawnedNoteDoubleTransform.localPosition = Vector3.Scale(spawnedNoteTransform.localPosition, new Vector3(1, -1, 1));
        spawnedNoteDouble.GetComponent<IntroNoteCollisionController>().Init(spawnedNoteCollisionController.NoteInfo, true);
        spawnedNoteDouble.GetComponent<IntroNoteMovementController>().Init(_events[_eventNumber], xOffset, _sideNoteXOffset, -1f);

        _eventNumber++;

        //Activate notes
        spawnedNote.SetActive(true);
        spawnedNoteDouble.SetActive(true);
    }

    private void NoteTrigger(KoreographyEvent ev)
    {
        if (_events.Count <= _eventNumber)
        {
            _eventNumber = 0;
        }
        if(_spawnPosition == ShootController.Position.Center)
        {
            SpawnNoteCenter();
        }
        else if(_spawnPosition == ShootController.Position.Right)
        {
            SpawnNoteRight();
        }
        else
        {
            SpawnNoteLeft();
        }
    }

    private void KoreographerStarted()
    {
        for (int i = 0; i < _notesToSpawn && i < _events.Count - 1; i++)
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
