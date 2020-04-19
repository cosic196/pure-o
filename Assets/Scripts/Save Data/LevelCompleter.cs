using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleter : MonoBehaviour {

    [SerializeField]
    private string _level;
    [SerializeField]
    private string _familyMember;
    [SerializeField]
    private float _bond;
    [SerializeField]
    private ScoreManager _scoreManager;
    [SerializeField]
    private string _unlockLevel;

    private void Start()
    {
        if(string.IsNullOrEmpty(_level))
        {
            _level = SceneManager.GetActiveScene().name;
        }
    }

    public void CompleteLevel()
    {
        SaveData saveData = SaveDataManager.Load();
        bool foundAndModifiedLevel = false;

        foreach (var level in saveData.Levels)
        {
            if(level.Name == _level)
            {
                level.Completed = true;
                level.Score = _scoreManager.Score;
                foundAndModifiedLevel = true;
                break;
            }
        }
        if(!foundAndModifiedLevel)
        {
            saveData.Levels.Add(new Level
            {
                Name = _level,
                Completed = true,
                Score = _scoreManager.Score
            });
        }
        saveData.Levels.Add(new Level
        {
            Completed = false,
            Name = _unlockLevel,
            Score = 0
        });


        bool foundAndModifiedMember = false;
        foreach (var familyMember in saveData.FamilyMembers)
        {
            if(familyMember.Name == _familyMember)
            {
                familyMember.Bond += _bond;
                foundAndModifiedMember = true;
                break;
            }
        }
        if(!foundAndModifiedMember)
        {
            saveData.FamilyMembers.Add(new FamilyMember
            {
                Bond = _bond,
                Killed = false,
                Name = _familyMember
            });
        }

        SaveDataManager.Save(saveData);
    }
}
