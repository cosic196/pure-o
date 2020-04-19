using UnityEngine;
using UnityEngine.UI;

public class MapPointController : MonoBehaviour {

    public string _levelName;
    [SerializeField]
    private GameObject _completedCross;
    [SerializeField]
    private Button _button;
    private bool _thisClicked = false;

    private void Start()
    {
        EventManager.StartListening("LoadNextLevel", LoadLevel);
    }

    private void LoadLevel()
    {
        if(!_thisClicked)
        {
            return;
        }
        EventManager.TriggerEvent("StartLoadingNextLevel", _levelName);
    }

    public void FadeOutAndLoadLevel()
    {
        EventManager.TriggerEvent("FadeOut");
        EventManager.TriggerEvent("StopPlayerController");
        _thisClicked = true;
    }

    public void Completed()
    {
        _completedCross.SetActive(true);
        _button.interactable = false;
    }
}
