using UnityEngine;

public class BeatLineSpawner : MonoBehaviour {

    [SerializeField]
    private int _numberOfBeatsInAdvance;
    [SerializeField]
    private string _beatLineTag;
    [SerializeField]
    private Transform _parentTransform;

    void Start () {
        EventManager.StartListening("SpawnBeatLine", Spawn);
    }

    private void Spawn()
    {
        GameObject _beatLine = ObjectPooler.Instance.SpawnFromPool(_beatLineTag);
        _beatLine.GetComponent<Transform>().localPosition = new Vector3(0f, GetDistance(LevelStats.Reference.NoteSpeed));

        GameObject _beatLineMirrored = ObjectPooler.Instance.SpawnFromPool(_beatLineTag);
        _beatLineMirrored.GetComponent<Transform>().localPosition = new Vector3(0f, -GetDistance(LevelStats.Reference.NoteSpeed));

        _beatLine.SetActive(true);
        _beatLineMirrored.SetActive(true);
    }

    private float GetDistance(float noteSpeed)
    {
        float bps = LevelStats.Reference.Bpm / 60f;
        float time = _numberOfBeatsInAdvance / bps;
        return noteSpeed * time;
    }
}
