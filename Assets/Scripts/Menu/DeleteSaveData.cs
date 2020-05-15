using UnityEngine;

public class DeleteSaveData : MonoBehaviour
{
    public void Delete()
    {
        SaveDataManager.DeleteSaveFile();
    }
}
