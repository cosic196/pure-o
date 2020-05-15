using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {

    [SerializeField]
    private string _firstLevel;
    [SerializeField]
    private Texture2D _cursorTexture;

	void Start () {
        EventManager.StartListening("StartGame", StartGame);

        Cursor.SetCursor(_cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    private void StartGame()
    {
        var saveData = SaveDataManager.Load();
        if (saveData.Levels.Count == 0)
        {
            SceneManager.LoadScene(_firstLevel);
        }
        else
        {
            SceneManager.LoadScene(saveData.SceneToLoad);
        }
    }

    public void FadeOutAndStartGame()
    {
        Cursor.visible = false;
        EventManager.TriggerEvent("FadeOut", "StartGame");
        EventManager.TriggerEvent("FadeOutMusic");
    }
}
