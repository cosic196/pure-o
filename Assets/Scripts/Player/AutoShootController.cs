using System.Collections.Generic;
using UnityEngine;

public class AutoShootController : MonoBehaviour
{

    [SerializeField]
    private ShootInfo _shootInfo;
    private ShootController.Position _currentPosition = ShootController.Position.Center;
    private Queue<NoteInfo> _notesInRange;

    // Use this for initialization
    void Start()
    {
        _notesInRange = new Queue<NoteInfo>();
        EventManager.StartListening("PressedShoot", Shoot);
        EventManager.StartListening("NoteInRange", AddNote);
        EventManager.StartListening("NoteOutOfRange", RemoveNote);
        EventManager.StartListening("PressedRight", () => { SetCurrentPosition(ShootController.Position.Right); });
        EventManager.StartListening("PressedLeft", () => { SetCurrentPosition(ShootController.Position.Left); });
        EventManager.StartListening("PressedCenter", () => { SetCurrentPosition(ShootController.Position.Center); });
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
            NoteInfo currentNote = _notesInRange.Peek();
            if (currentNote.position != _currentPosition)
            {
                // TODO - Implement missing notes
                _notesInRange.Dequeue();
                return;
            }
        }

        //Shoot logic
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.tag == "Shootable")
            {
                // TODO - Implement ShootInfo
                hit.transform.GetComponent<Shootable>().Shot(JsonUtility.ToJson(_shootInfo));
            }
        }
        _notesInRange.Dequeue();
    }

    private void AddNote(string json)
    {
        _notesInRange.Enqueue(JsonUtility.FromJson<NoteInfo>(json));
        Shoot();
    }

    private void RemoveNote(string json)
    {
        if (_notesInRange.Count > 0)
        {
            _notesInRange.Dequeue();
        }
    }

    private void SetCurrentPosition(ShootController.Position position)
    {
        _currentPosition = position;
        Shoot();
    }
}
