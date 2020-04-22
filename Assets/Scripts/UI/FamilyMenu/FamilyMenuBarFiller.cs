using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FamilyMenuBarFiller : MonoBehaviour {

    [SerializeField]
    private RectTransform _barTransform;
    [SerializeField]
    private GameObject _fillPrefab;

    void Start () {
        SaveData saveData = SaveDataManager.Load();
        var colorsDictionary = FamilyMemberColorsGlobal.GetUnityColors();
        float addedBond = 0f;

        foreach (var completedLevel in saveData.Levels.Where(x => x.Completed))
        {
            var fill = Instantiate(_fillPrefab, _barTransform);
            Image fillImage = fill.GetComponent<Image>();
            //Set color
            Color fillColor;
            if(saveData.FamilyMembersAlive.Contains(completedLevel.FamilyMember.Name))
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
	}
}
