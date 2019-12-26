using UnityEngine;

public class LevelStats : MonoBehaviour {

    public static LevelStats Reference { get; private set; }
    [SerializeField]
    private int _bpm;
    [SerializeField]
    private float _noteSpeed;

    public int Bpm { get { return _bpm; } }
    public float NoteSpeed { get { return _noteSpeed; } }

    void Start () {
		if(Reference == null)
        {
            Reference = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
