using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FamilyMenuLegendColorAssigner : MonoBehaviour {

    [SerializeField]
    private Image BrotherImage;
    [SerializeField]
    private Image SisterImage;
    [SerializeField]
    private Image FatherImage;
    [SerializeField]
    private Image MotherImage;

    void Start () {
        var colors = FamilyMemberColorsGlobal.GetUnityColors();
        Color brotherColor, sisterColor, fatherColor, motherColor;

        colors.TryGetValue(FamilyMemberName.Brother, out brotherColor);
        colors.TryGetValue(FamilyMemberName.Sister, out sisterColor);
        colors.TryGetValue(FamilyMemberName.Father, out fatherColor);
        colors.TryGetValue(FamilyMemberName.Mother, out motherColor);

        //Set color to grey if family member is dead
        SaveData saveData = SaveDataManager.Load();
        if (!saveData.FamilyMembersAlive.Contains(FamilyMemberName.Brother))
        { brotherColor = FamilyMemberColorsGlobal.DeadColor; }
        if (!saveData.FamilyMembersAlive.Contains(FamilyMemberName.Sister))
        { sisterColor = FamilyMemberColorsGlobal.DeadColor; }
        if (!saveData.FamilyMembersAlive.Contains(FamilyMemberName.Father))
        { fatherColor = FamilyMemberColorsGlobal.DeadColor; }
        if (!saveData.FamilyMembersAlive.Contains(FamilyMemberName.Sister))
        { motherColor = FamilyMemberColorsGlobal.DeadColor; }

        BrotherImage.color = brotherColor;
        SisterImage.color = sisterColor;
        FatherImage.color = fatherColor;
        MotherImage.color = motherColor;
    }
}
