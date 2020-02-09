using UnityEngine;

public class NoteSpawner : MonoBehaviour {

    [SerializeField]
    private int _numberOfBeatsInAdvance;
    [SerializeField]
    private string _noteTag;
    [SerializeField]
    private Transform _parentTransform;
    [SerializeField]
    private float _sideNoteXOffset;
    private static int noteId;

	void Start () {
        noteId = 0;
        EventManager.StartListening("SpawnNoteCenter", SpawnNoteCenter);
        EventManager.StartListening("SpawnNoteRight", SpawnNoteRight);
        EventManager.StartListening("SpawnNoteLeft", SpawnNoteLeft);
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
        GameObject spawnedNote = ObjectPooler.Instance.SpawnFromPool(_noteTag);
        //Set transform position
        Transform spawnedNoteTransform = spawnedNote.GetComponent<Transform>();
        NoteCollisionController spawnedNoteCollisionController = spawnedNote.GetComponent<NoteCollisionController>();
        spawnedNoteCollisionController.Init(new NoteInfo {id = noteId++, position = notePosition });
        spawnedNoteTransform.localPosition = GetLocalPosition(LevelStats.Reference.NoteSpeed, xOffset);

        //Do the same for the double with inversed transform
        GameObject spawnedNoteDouble = ObjectPooler.Instance.SpawnFromPool(_noteTag);
        Transform spawnedNoteDoubleTransform = spawnedNoteDouble.GetComponent<Transform>();
        spawnedNoteDoubleTransform.localPosition = Vector3.Scale(spawnedNoteTransform.localPosition, new Vector3(1,-1,1));
        spawnedNoteDouble.GetComponent<NoteCollisionController>().Init(spawnedNoteCollisionController.NoteInfo, true);

        //Activate notes
        spawnedNote.SetActive(true);
        spawnedNoteDouble.SetActive(true);
    }

    private float GetDistance(float noteSpeed)
    {
        float bps = LevelStats.Reference.Bpm / 60f;
        float time = _numberOfBeatsInAdvance / bps;
        return noteSpeed * time;
    }

    private Vector3 GetLocalPosition(float speed, float xOffset)
    {
        float distance = GetDistance(speed);
        return new Vector3(xOffset, distance);
    }
}
