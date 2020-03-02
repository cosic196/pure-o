using UnityEngine;

public class MobilePauseButton : MonoBehaviour {

    private void Awake()
    {
        #if UNITY_ANDROID || UNITY_IOS
        return;
        #endif
        gameObject.SetActive(false);
    }

    public void MobilePause()
    {
        EventManager.TriggerEvent("Paused");
    }
}
