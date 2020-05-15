using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

public static class UpgradesGlobal  {
    public static List<Upgrade> Upgrades = new List<Upgrade>
    {
        new Upgrade
        {
            UpgradeName = UpgradeName.AutoRegenerate,
            bondRequirement = 0.4f,
            Description = "Your psyche automatically replenishes over time."
        },
        new Upgrade
        {
            UpgradeName = UpgradeName.SlowMotion,
            bondRequirement = 0.03f,
            Description = "Press right click to slow down time.\nDrains psyche slowly."
        }
    };
}

public class Upgrade
{
    public UpgradeName UpgradeName;
    public string Description;
    public float bondRequirement;

    public static string NameToString(UpgradeName value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attributes.Length > 0)
            return attributes[0].Description;
        else
            return value.ToString();
    }
}

public enum UpgradeName
{
    [Description("Slow motion")]
    SlowMotion,
    [Description("Auto regenerate")]
    AutoRegenerate
}
