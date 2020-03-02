using SonicBloom.Koreo;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {

    public enum Position
    {
        Left,
        Center,
        Right
    }
    [SerializeField]
    private ShootInfo _shootInfo;
    private Position _currentPosition;
    private Queue<NoteInfo> _notesInRange;

	// Use this for initialization
	void Start () {
        SetCurrentPosition(Position.Center);
        _notesInRange = new Queue<NoteInfo>();
        #if UNITY_ANDROID || UNITY_IOS
        Koreographer.Instance.RegisterForEvents("test", AutoShoot);
        #endif
        EventManager.StartListening("PressedShoot", Shoot);
        EventManager.StartListening("NoteInRange", AddNote);
        EventManager.StartListening("NoteOutOfRange", RemoveNote);
        EventManager.StartListening("PressedRight", () => { SetCurrentPosition(Position.Right); });
        EventManager.StartListening("PressedLeft", () => { SetCurrentPosition(Position.Left); });
        EventManager.StartListening("PressedCenter", () => { SetCurrentPosition(Position.Center); });
    }

    private void AutoShoot(KoreographyEvent koreoEvent)
    {
        Shoot();
    }

    private void Shoot()
    {
        //Check if you hit a note
        if (_notesInRange.Count == 0)
        {
            EventManager.TriggerEvent("OutOfRhythmShot");
            return;
        }
        else
        {
            NoteInfo currentNote = _notesInRange.Dequeue();
            if(currentNote.position != _currentPosition)
            {
                #if UNITY_STANDALONE
                EventManager.TriggerEvent("OutOfRhythmShot");
                #endif
                return;
            }
            EventManager.TriggerEvent("NoteShot", JsonUtility.ToJson(currentNote));
        }

        //Shoot logic
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.tag == "Shootable")
            {
                _shootInfo.pointOfHit = hit.point;
                hit.transform.GetComponent<Shootable>().Shot(JsonUtility.ToJson(_shootInfo));
            }
            else
            {
                EventManager.TriggerEvent("HitAWall", hit.point.x + "," + hit.point.y + "," + hit.point.z + "," + hit.normal.x + "," + hit.normal.y + "," + hit.normal.z);
            }
        }
        EventManager.TriggerEvent("Shot");
    }

    private void AddNote(string json)
    {
        _notesInRange.Enqueue(JsonUtility.FromJson<NoteInfo>(json));
    }

    private void RemoveNote(string json)
    {
        if(_notesInRange.Count > 0)
        {
            _notesInRange.Dequeue();
        }
    }

    private void SetCurrentPosition(Position position)
    {
        _currentPosition = position;
    }
}

[Serializable]
public class ShootInfo
{
    public int damage;
    public Vector3 pointOfHit;
}
