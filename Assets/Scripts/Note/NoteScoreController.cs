using UnityEngine;

public class NoteScoreController : MonoBehaviour {

    [SerializeField]
    private int _noteScore;
    GameObjectEventManager _gameObjectEventManager;

	void Start () {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _gameObjectEventManager.StartListening("NoteShot", GiveScore);
	}

    private void GiveScore()
    {
        EventManager.TriggerEvent("GiveScore", _noteScore.ToString());
    }
}
