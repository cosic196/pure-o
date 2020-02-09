using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private int _score;

	void Start () {
        EventManager.StartListening("GiveScore", GiveScore);
	}

    private void GiveScore(string score)
    {
        _score += int.Parse(score);

        // TODO : Remove after testing
        Debug.Log("SCORE : " + _score);
    }
}
