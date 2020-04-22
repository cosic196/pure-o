using System.Linq;
using TMPro;
using UnityEngine;

public class FamilyMenuPercentageWriter : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _percentageText;

    void Start () {
        SaveData saveData = SaveDataManager.Load();
        float bond = 0f;
        foreach (var completedLevel in saveData.Levels.Where(x => x.Completed))
        {
            bond += completedLevel.FamilyMember.Bond;
        }
        _percentageText.text = (bond * 100f) + "%";
	}
}
