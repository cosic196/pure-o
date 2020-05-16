using UnityEngine;
using UnityEngine.UI;

public class TwitterButton : MonoBehaviour
{
    [SerializeField]
    private string _url = "https://twitter.com/cosic196";
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { Application.OpenURL(_url); });
    }
}
