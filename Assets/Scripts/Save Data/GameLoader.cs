using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {

    [SerializeField]
    private string _firstLevel;
    [SerializeField]
    private string _levelSelectLevel;
    [SerializeField]
    private Texture2D _cursorTexture;

	void Start () {
        Cursor.visible = false;
        Cursor.SetCursor(_cursorTexture, Vector2.zero, CursorMode.Auto);
        var saveData = SaveDataManager.Load();
        if(saveData.Levels.Count == 0)
        {
            SceneManager.LoadScene(_firstLevel);
        }
        else
        {
            SceneManager.LoadScene(_levelSelectLevel);
        }
    }
}
