using UnityEngine;

public class NoteSpawner : MonoBehaviour {

    [SerializeField]
    private int _bpm;
    [SerializeField]
    private int _numberOfBeatsInAdvance;
    [SerializeField]
    private GameObject _centerNotePrefab;
    [SerializeField]
    private GameObject _centerNoteDoublePrefab;
    [SerializeField]
    private GameObject _rightNotePrefab;
    [SerializeField]
    private GameObject _rightNoteDoublePrefab;
    [SerializeField]
    private GameObject _leftNotePrefab;
    [SerializeField]
    private GameObject _leftNoteDoublePrefab;
    [SerializeField]
    private Transform _parentTransform;

	void Start () {
        EventManager.StartListening("SpawnNoteCenter", SpawnNoteCenter);
        EventManager.StartListening("SpawnNoteRight", SpawnNoteRight);
        EventManager.StartListening("SpawnNoteLeft", SpawnNoteLeft);
    }

    private void SpawnNoteCenter()
    {
        SpawnNote(_centerNotePrefab, _centerNoteDoublePrefab);
    }

    private void SpawnNoteLeft()
    {
        SpawnNote(_leftNotePrefab, _leftNoteDoublePrefab);
    }

    private void SpawnNoteRight()
    {
        SpawnNote(_rightNotePrefab, _rightNoteDoublePrefab);
    }

    private void SpawnNote(GameObject notePrefab, GameObject noteDoublePrefab)
    {
        //Instantiate note
        GameObject spawnedNote = Instantiate(notePrefab, _parentTransform);
        //Set transform position
        Transform spawnedNoteTransform = spawnedNote.GetComponent<Transform>();
        NoteCollisionController spawnedNoteCollisionController = spawnedNote.GetComponent<NoteCollisionController>();
        spawnedNoteCollisionController.Init(spawnedNote.GetInstanceID());
        spawnedNoteTransform.localPosition = GetLocalPosition(spawnedNote.GetComponent<NoteMovementController>());

        //Do the same for the double with inversed transform
        GameObject spawnedNoteDouble = Instantiate(noteDoublePrefab, _parentTransform);
        Transform spawnedNoteDoubleTransform = spawnedNoteDouble.GetComponent<Transform>();
        spawnedNoteDoubleTransform.localPosition = Vector3.Scale(spawnedNoteTransform.localPosition, new Vector3(1,-1,1));
        spawnedNoteDouble.GetComponent<NoteDoubleCollisionController>().Init(spawnedNoteCollisionController.NoteInfo.id);

        //Activate notes
        spawnedNote.SetActive(true);
        spawnedNoteDouble.SetActive(true);
    }

    private float GetDistance(float noteSpeed)
    {
        float bps = _bpm / 60f;
        float time = _numberOfBeatsInAdvance / bps;
        return noteSpeed * time;
    }

    private Vector3 GetLocalPosition(NoteMovementController noteMovementController)
    {
        float distance = GetDistance(noteMovementController.Speed);
        return new Vector3(noteMovementController.Goal.x, distance + noteMovementController.Goal.y);
    }
}
