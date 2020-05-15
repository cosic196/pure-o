using UnityEngine;

public class EnemyScoreController : MonoBehaviour {

    [SerializeField]
    private int _shotScore;
    [SerializeField]
    private int _diedScore;
    [SerializeField]
    private int _headshotScore;
    GameObjectEventManager _gameObjectEventManager;

	void Start () {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _gameObjectEventManager.StartListening("Died", GiveScoreOnDeath);
        _gameObjectEventManager.StartListening("Shot", GiveScoreOnShot);
        _gameObjectEventManager.StartListening("Headshot", GiveScoreOnHeadshot);
	}

    private void GiveScoreOnHeadshot(string shootInfo)
    {
        EventManager.TriggerEvent("GiveScore", _headshotScore.ToString());
    }

    private void GiveScoreOnDeath()
    {
        EventManager.TriggerEvent("GiveScore", _diedScore.ToString());
    }

    private void GiveScoreOnShot(string shootInfo)
    {
        EventManager.TriggerEvent("GiveScore", _shotScore.ToString());
    }
}
