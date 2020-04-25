using TMPro;
using UnityEngine;

public class BoyDialogueInit : MonoBehaviour {

    [SerializeField]
    private DialogueTextController _boyDialogueController;
    [SerializeField]
    private TextMeshProUGUI _boyDialogueText;
    [SerializeField]
    private string _defaultText;

    void Start () {
        SaveData saveData = SaveDataManager.Load();
        if(saveData.BoyDialogue == null)
        {
            _boyDialogueText.text = _defaultText;
        }
        else if(saveData.BoyDialogue.Count == 0)
        {
            _boyDialogueText.text = _defaultText;
        }
        else
        {
            _boyDialogueText.text = saveData.BoyDialogue[0];
            for (int i = 1; i < saveData.BoyDialogue.Count; i++)
            {
                _boyDialogueController.AddDialogueLine(saveData.BoyDialogue[i]);
            }
        }
	}
}
