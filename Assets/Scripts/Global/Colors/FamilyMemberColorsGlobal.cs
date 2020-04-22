using System.Collections.Generic;
using UnityEngine;

public static class FamilyMemberColorsGlobal {
    public static Dictionary<FamilyMemberName, string> Colors = new Dictionary<FamilyMemberName, string>
    {
        { FamilyMemberName.Brother, "#9C0000" },
        { FamilyMemberName.Sister, "#D96900" },
        { FamilyMemberName.Father, "#446D32" },
        { FamilyMemberName.Mother, "#502EB5" }
    };

    public static Color DeadColor = Color.grey;

    public static Dictionary<FamilyMemberName, Color> GetUnityColors()
    {
        string brotherColorHex, sisterColorHex, fatherColorHex, motherColorHex;
        Color brotherColor, sisterColor, fatherColor, motherColor;

        Colors.TryGetValue(FamilyMemberName.Brother, out brotherColorHex);
        Colors.TryGetValue(FamilyMemberName.Sister, out sisterColorHex);
        Colors.TryGetValue(FamilyMemberName.Father, out fatherColorHex);
        Colors.TryGetValue(FamilyMemberName.Mother, out motherColorHex);

        ColorUtility.TryParseHtmlString(brotherColorHex, out brotherColor);
        ColorUtility.TryParseHtmlString(sisterColorHex, out sisterColor);
        ColorUtility.TryParseHtmlString(fatherColorHex, out fatherColor);
        ColorUtility.TryParseHtmlString(motherColorHex, out motherColor);

        return new Dictionary<FamilyMemberName, Color>
        {
            { FamilyMemberName.Brother, brotherColor },
            { FamilyMemberName.Sister, sisterColor },
            { FamilyMemberName.Mother, motherColor },
            { FamilyMemberName.Father, fatherColor },
        };
    }
}
