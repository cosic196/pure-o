using SonicBloom.Koreo;
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
        _currentPosition = ShootController.Position.Center;
        _notesInRange = new Queue<NoteInfo>();
        Koreographer.Instance.RegisterForEvents("test", Shoot);
        EventManager.StartListening("NoteInRange", AddNote);
        EventManager.StartListening("NoteOutOfRange", RemoveNote);
        EventManager.StartListening("PressedRight", () => { SetCurrentPosition(ShootController.Position.Right); });
        EventManager.StartListening("PressedLeft", () => { SetCurrentPosition(ShootController.Position.Left); });
        EventManager.StartListening("PressedCenter", () => { SetCurrentPosition(ShootController.Position.Center); });
    }

    private void Shoot(KoreographyEvent koreoEvent)
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
            if (currentNote.position != _currentPosition)
            {
                //EventManager.TriggerEvent("OutOfRhythmShot");
                return;
            }
            EventManager.TriggerEvent("NoteShot", JsonUtility.ToJson(currentNote));
        }

        //Shoot logic
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
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
        if (_notesInRange.Count > 0)
        {
            _notesInRange.Dequeue();
        }
    }

    private void SetCurrentPosition(ShootController.Position position)
    {
        _currentPosition = position;
    }
}
