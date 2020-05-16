using UnityEngine;

public class UIElementFollowGameobject : MonoBehaviour
{
    public GameObject Obj;
    private RectTransform _transform;

    void Start()
    {
        _transform = GetComponent<RectTransform>();
    }

    void OnGUI()
    {
        var gotransform = Obj.GetComponent<Transform>();
        _transform.position = Camera.main.WorldToScreenPoint(gotransform.position);
    }
}
