using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButtonFunctionality : MonoBehaviour {

	public void Continue()
    {
        EventManager.TriggerEvent("UnpauseStarted");
    }

    public void ContinueToNextScene(string sceneName)
    {
        EventManager.StartListening("ContinueToNextScene", () =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);
        });
        EventManager.TriggerEvent("FadeOut", "ContinueToNextScene");
    }
}
