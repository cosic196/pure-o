using System.Collections.Generic;

[System.Serializable]
public class SaveData {

    public List<Level> Levels;
    public List<FamilyMemberName> FamilyMembersAlive;
    public List<string> BoyDialogue;

    public SaveData()
    {
        Levels = new List<Level>();
        FamilyMembersAlive = new List<FamilyMemberName>
        {
            FamilyMemberName.Brother,
            FamilyMemberName.Father,
            FamilyMemberName.Mother,
            FamilyMemberName.Sister
        };
        BoyDialogue = new List<string>
        {
            "Where should we go next?"
        };
    }
}

[System.Serializable]
public class Level
{
    public string Name;
    public int Score;
    public bool Completed;
    public FamilyMember FamilyMember;
}

[System.Serializable]
public class FamilyMember
{
    public FamilyMemberName Name;
    public float Bond;
}

public enum FamilyMemberName
{
    Brother,
    Sister,
    Father,
    Mother
}