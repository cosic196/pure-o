using TMPro;
using UnityEngine;

public class EndScoreCalculator : MonoBehaviour {
    
    [SerializeField]
    private TextMeshProUGUI _killedTMPro;
    private int _killed;
    [SerializeField]
    private TextMeshProUGUI _shotsTMPro;
    private int _shots;
    [SerializeField]
    private TextMeshProUGUI _shotsOnTargetTMPro;
    private int _shotsOnTarget;
    [SerializeField]
    private TextMeshProUGUI _headshotsTMPro;
    private int _headshots;
    [SerializeField]
    private TextMeshProUGUI _scoreTMPro;

    void Start () {
        _killed = 0;
        _shots = 0;
        _shotsOnTarget = 0;
        _headshots = 0;
        EventManager.StartListening("NoteShot", AddShots);
        EventManager.StartListening("AnEnemyDied", AddKilled);
        EventManager.StartListening("AnEnemyWasShot", AddShotsOnTarget);
        EventManager.StartListening("ScoreChanged", ChangeScore);
        EventManager.StartListening("Headshot", AddHeadshots);
	}

    private void AddHeadshots()
    {
        _headshots++;
        _headshotsTMPro.text = _headshots.ToString();
    }

    private void ChangeScore(string score)
    {
        _scoreTMPro.text = score;
    }

    private void AddShotsOnTarget()
    {
        _shotsOnTarget++;
        _shotsOnTargetTMPro.text = _shotsOnTarget.ToString();
    }

    private void AddKilled()
    {
        _killed++;
        _killedTMPro.text = _killed.ToString();
    }

    private void AddShots(string _)
    {
        _shots++;
        _shotsTMPro.text = _shots.ToString();
    }
}
