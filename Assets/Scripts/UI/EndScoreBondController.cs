using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EndScoreBondController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _barTransform;
    [SerializeField]
    private GameObject _fillPrefab;
    [SerializeField]
    private LevelCompleter _levelCompleter;

    void Start()
    {
        if(_levelCompleter == null)
        {
            Debug.LogError("Didn't attach Level Completer to End Score Bond Controller.");
            return;
        }

        SaveData saveData = SaveDataManager.Load();
        var colorsDictionary = FamilyMemberColorsGlobal.GetUnityColors();
        float addedBond = 0f;

        foreach (var completedLevel in saveData.Levels.Where(x => x.Completed))
        {
            var fill = Instantiate(_fillPrefab, _barTransform);
            Image fillImage = fill.GetComponent<Image>();
            //Set color
            Color fillColor;
            if (saveData.FamilyMembersAlive.Contains(completedLevel.FamilyMember.Name))
            {
                colorsDictionary.TryGetValue(completedLevel.FamilyMember.Name, out fillColor);
            }
            else
            {
                fillColor = FamilyMemberColorsGlobal.DeadColor;
            }
            fillImage.color = fillColor;
            //Set size
            RectTransform fillTransform = fill.GetComponent<RectTransform>();
            fillTransform.anchorMin = new Vector2(addedBond, 0f);
            fillTransform.anchorMax = new Vector2(addedBond + completedLevel.FamilyMember.Bond, 1f);
            fillTransform.pivot = new Vector2(0.5f, 0.5f);

            addedBond += completedLevel.FamilyMember.Bond;
        }
        //If this level is completed : return
        if(saveData.Levels.Where(x => x.Name == _levelCompleter.GetInfo().ThisLevelName && x.Completed).Count() > 0)
        {
            return;
        }
        AddCurrentLevelToBar(addedBond);
        NotifyIfNewUpgrade(addedBond, saveData);
    }

    private void AddCurrentLevelToBar(float addedBond)
    {
        var info = _levelCompleter.GetInfo();
        var fill = Instantiate(_fillPrefab, _barTransform);
        Image fillImage = fill.GetComponent<Image>();
        //Set color
        Color fillColor;
        var colorsDictionary = FamilyMemberColorsGlobal.GetUnityColors();
        colorsDictionary.TryGetValue(info.FamilyMemberName, out fillColor);
        fillImage.color = fillColor;
        //Set size
        RectTransform fillTransform = fill.GetComponent<RectTransform>();
        fillTransform.anchorMin = new Vector2(addedBond, 0f);
        fillTransform.anchorMax = new Vector2(addedBond + info.Bond, 1f);
        fillTransform.pivot = new Vector2(0.5f, 0.5f);
    }

    private void NotifyIfNewUpgrade(float addedBond, SaveData saveData)
    {
        var info = _levelCompleter.GetInfo();
        foreach (var upgrade in UpgradesGlobal.Upgrades.Where(x => x.bondRequirement > addedBond && x.bondRequirement <= addedBond + info.Bond))
        {
            EventManager.TriggerEvent("EndScoreUnlockedUpgrade", upgrade.UpgradeName + "/" + upgrade.Description);
            _levelCompleter.UnlockUpgrade();
        }
    }
}
