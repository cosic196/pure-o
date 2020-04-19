using System.Collections.Generic;

[System.Serializable]
public class SaveData {

    public List<Level> Levels;
    public List<FamilyMember> FamilyMembers;

    public SaveData()
    {
        Levels = new List<Level>();
        FamilyMembers = new List<FamilyMember>();
    }
}

[System.Serializable]
public class Level
{
    public string Name;
    public int Score;
    public bool Completed;
}

[System.Serializable]
public class FamilyMember
{
    public string Name;
    public float Bond;
    public bool Killed;
}
