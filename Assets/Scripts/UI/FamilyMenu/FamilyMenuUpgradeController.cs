using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FamilyMenuUpgradeController : MonoBehaviour {

    private Upgrade _upgrade;
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private Image _foreground;
    [SerializeField]
    private Image _background;
    [SerializeField]
    private GameObject _collider;

    public void Init(Upgrade upgrade)
    {
        _upgrade = upgrade;
        _description.text = "<color=#FFF>-" + Upgrade.NameToString(upgrade.UpgradeName) + "-</color>";
        _description.text += "<size=75%>\n" + upgrade.Description + "</size>";
        if(_name != null)
        {
            _name.text = Upgrade.NameToString(upgrade.UpgradeName);
        }

        SaveData saveData = SaveDataManager.Load();
        float bond = 0f;

        foreach (var completedLevel in saveData.Levels.Where(x => x.Completed))
        {
            bond += completedLevel.FamilyMember.Bond;
            if(bond >= upgrade.bondRequirement)
            {
                if(saveData.FamilyMembersAlive.Contains(completedLevel.FamilyMember.Name))
                {
                    Unlock(completedLevel.FamilyMember.Name);
                }
                else
                {
                    Lock();
                }
                return;
            }
        }
        Lock();
    }

    private void Unlock(FamilyMemberName familyMemberName)
    {
        var colors = FamilyMemberColorsGlobal.GetUnityColors();
        Color unlockedWithColor;
        colors.TryGetValue(familyMemberName, out unlockedWithColor);
        _foreground.color = unlockedWithColor;
        _collider.SetActive(true);
        _description.color = Color.black;
    }

    private void Lock()
    {
        _collider.SetActive(false);
        _foreground.color = Color.black;
        _background.color = Color.black;
        GetComponent<Animator>().enabled = false;
    }
}
