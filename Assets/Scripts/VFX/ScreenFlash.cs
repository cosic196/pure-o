using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour {

    private Image _flashImage;
    [SerializeField]
    private int _numberOfFrames;
	
	void Awake () {
        _flashImage = GetComponent<Image>();
	}

    private void Start()
    {
        EventManager.StartListening("AnEnemyDied", () => StartCoroutine(Flash()));
    }

    private IEnumerator Flash()
    {
        _flashImage.color = Color.white;
        for (int i = 0; i < _numberOfFrames; i++)
        {
            yield return 0;
        }
        _flashImage.color = Color.clear;
        yield return null;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _flashImage.color = Color.clear;
    }
}
