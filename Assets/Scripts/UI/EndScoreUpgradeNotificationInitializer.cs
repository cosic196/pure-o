using TMPro;
using UnityEngine;

public class EndScoreUpgradeNotificationInitializer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _title;
    [SerializeField]
    private TextMeshProUGUI _description;

    public void Initialize(string title, string description)
    {
        _title.text = "<color=#B00>" + title + "</color> unlocked!";
        _description.text = description;
    }
}
