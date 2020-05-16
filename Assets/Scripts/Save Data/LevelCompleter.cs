using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleter : MonoBehaviour {

    [SerializeField]
    private string _thisLevel;
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

    private bool _unlockedNewUpgrades = false;

    private void Start()
    {
        if(string.IsNullOrEmpty(_thisLevel))
        {
            _thisLevel = SceneManager.GetActiveScene().name;
        }
    }

    public void CompleteLevel()
    {
        SaveData saveData = SaveDataManager.Load();
        bool foundAndModifiedLevel = false;

        foreach (var level in saveData.Levels)
        {
            if(level.Name == _thisLevel)
            {
                if(!level.Completed)
                {
                    level.Completed = true;
                    level.FamilyMember = new FamilyMember
                    {
                        Bond = _bond,
                        Name = _familyMemberName
                    };
                    saveData.BoyDialogue = _boyDialogue;
                    saveData.Levels.Add(new Level
                    {
                        Completed = false,
                        Name = _unlockLevel,
                        Score = 0
                    });
                }
                level.Score = _scoreManager.Score;
                foundAndModifiedLevel = true;
                break;
            }
        }
        if(!foundAndModifiedLevel)
        {
            saveData.Levels.Add(new Level
            {
                Name = _thisLevel,
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
        if(!saveData.NewUpgradeNotification)
        {
            saveData.NewUpgradeNotification = _unlockedNewUpgrades;
        }
        SaveDataManager.Save(saveData);
    }

    public LevelCompletedInfo GetInfo()
    {
        string thisLevelName = _thisLevel;
        if (string.IsNullOrEmpty(_thisLevel))
        {
            thisLevelName = SceneManager.GetActiveScene().name;
        }
        return new LevelCompletedInfo(_bond, _familyMemberName, thisLevelName);
    }

    public class LevelCompletedInfo
    {
        public float Bond { get; private set; }
        public FamilyMemberName FamilyMemberName { get; private set; }
        public string ThisLevelName { get; private set; }

        public LevelCompletedInfo(float bond, FamilyMemberName familyMemberName, string thisLevelName)
        {
            Bond = bond;
            FamilyMemberName = familyMemberName;
            ThisLevelName = thisLevelName;
        }
    }

    public void UnlockUpgrade()
    {
        _unlockedNewUpgrades = true;
    }
}
