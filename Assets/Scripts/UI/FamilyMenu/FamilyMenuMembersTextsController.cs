using TMPro;
using UnityEngine;

public class FamilyMenuMembersTextsController : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _brotherText;
    [SerializeField]
    private TextMeshProUGUI _sisterText;
    [SerializeField]
    private TextMeshProUGUI _fatherText;
    [SerializeField]
    private TextMeshProUGUI _motherText;

    void Start () {
        SaveData saveData = SaveDataManager.Load();

        if(!saveData.FamilyMembersAlive.Contains(FamilyMemberName.Brother))
        {
            _brotherText.fontStyle = FontStyles.Strikethrough | FontStyles.Bold;
            _brotherText.color = FamilyMemberColorsGlobal.DeadColor;
        }
        if (!saveData.FamilyMembersAlive.Contains(FamilyMemberName.Sister))
        {
            _sisterText.fontStyle = FontStyles.Strikethrough | FontStyles.Bold;
            _sisterText.color = FamilyMemberColorsGlobal.DeadColor;
        }
        if (!saveData.FamilyMembersAlive.Contains(FamilyMemberName.Father))
        {
            _fatherText.fontStyle = FontStyles.Strikethrough | FontStyles.Bold;
            _fatherText.color = FamilyMemberColorsGlobal.DeadColor;
        }
        if (!saveData.FamilyMembersAlive.Contains(FamilyMemberName.Mother))
        {
            _motherText.fontStyle = FontStyles.Strikethrough | FontStyles.Bold;
            _motherText.color = FamilyMemberColorsGlobal.DeadColor;
        }
    }
}
