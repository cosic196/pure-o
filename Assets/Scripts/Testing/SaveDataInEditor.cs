using UnityEngine;

public class SaveDataInEditor : MonoBehaviour {

    public SaveData saveData;

    void Awake()
    {
        SaveDataManager.Save(saveData);
    }

}
