using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleter : MonoBehaviour {

    [SerializeField]
    private string _level;
    [SerializeField]
    private FamilyMemberName _familyMemberName;
    [SerializeField]
    private float _bond;
    [SerializeField]
    private ScoreManager _scoreManager;
    [SerializeField]
    private string _unlockLevel;
    [SerializeField]
    private List<string> _boyDialogue;

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
                level.FamilyMember = new FamilyMember
                {
                    Bond = _bond,
                    Name = _familyMemberName
                };
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
                Score = _scoreManager.Score,
                FamilyMember = new FamilyMember
                {
                    Bond = _bond,
                    Name = _familyMemberName
                }
            });
            saveData.Levels.Add(new Level
            {
                Completed = false,
                Name = _unlockLevel,
                Score = 0
            });
            saveData.BoyDialogue = _boyDialogue;
        }

        SaveDataManager.Save(saveData);
    }
}
