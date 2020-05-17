using UnityEngine;

public class PopUpInstantiator : MonoBehaviour
{
    [SerializeField]
    private GameObject _popUpPrefab;
    [SerializeField]
    private string _tag;
    [SerializeField]
    private Color _color;
    [Space]
    [SerializeField]
    private string _triggerWithText;
    [Space]
    [SerializeField]
    private string _trigger;
    [SerializeField]
    private string _text;
    private Transform _transform;

    void Start()
    {
        _transform = GetComponent<Transform>();
        if(!string.IsNullOrEmpty(_triggerWithText))
        {
            EventManager.StartListening(_triggerWithText, InstantiatePopUp);
            if(!string.IsNullOrEmpty(_trigger))
            {
                Debug.LogError("PopUpInstantiator Error - Can't use both Trigger With Text and Trigger");
            }
        }
        else
        {
            EventManager.StartListening(_trigger, () => InstantiatePopUp(_text));
        }
    }

    private void InstantiatePopUp(string popUpText)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.tag == _tag)
            {
                var popup = Instantiate(_popUpPrefab, _transform);
                popup.GetComponent<PopUpController>().Init("<color=#"+ColorUtility.ToHtmlStringRGBA(_color)+">"+popUpText+"</color>", hit.point);
                popup.SetActive(true);
            }
        }
    }
}
