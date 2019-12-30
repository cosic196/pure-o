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
    private Position _currentPosition = Position.Center;
    private Queue<NoteInfo> _notesInRange;

	// Use this for initialization
	void Start () {
        _notesInRange = new Queue<NoteInfo>();
        EventManager.StartListening("PressedShoot", Shoot);
        EventManager.StartListening("NoteInRange", AddNote);
        EventManager.StartListening("NoteOutOfRange", RemoveNote);
        EventManager.StartListening("PressedRight", () => { SetCurrentPosition(Position.Right); });
        EventManager.StartListening("PressedLeft", () => { SetCurrentPosition(Position.Left); });
        EventManager.StartListening("PressedCenter", () => { SetCurrentPosition(Position.Center); });
    }

    private void Shoot()
    {
        //Check if you hit a note
        if (_notesInRange.Count == 0)
        {
            return;
        }
        else
        {
            NoteInfo currentNote = _notesInRange.Dequeue();
            if(currentNote.position != _currentPosition)
            {
                // TODO - Implement missing notes
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
