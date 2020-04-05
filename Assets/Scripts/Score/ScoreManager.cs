using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private int _score;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            int returnValue;
            if (value < 0)
                returnValue = 0;
            else
                returnValue = value;

            _score = returnValue;
            EventManager.TriggerEvent("ScoreChanged", Score.ToString());
        }
    }

	void Start () {
        _score = 0;
        EventManager.StartListening("GiveScore", GiveScore);
	}

    private void GiveScore(string score)
    {
        Score += int.Parse(score);
    }
}
