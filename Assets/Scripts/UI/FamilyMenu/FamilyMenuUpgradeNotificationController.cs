using UnityEngine;

public class FamilyMenuUpgradeNotificationController : MonoBehaviour
{
    void Start()
    {
        if(!SaveDataManager.Load().NewUpgradeNotification)
        {
            gameObject.SetActive(false);
            return;
        }
        EventManager.StartListening("ShowFamily", FamilyMenuClicked);
    }

    private void FamilyMenuClicked()
    {
        var saveData = SaveDataManager.Load();
        saveData.NewUpgradeNotification = false;
        SaveDataManager.Save(saveData);
        gameObject.SetActive(false);
    }
}
