using UnityEngine;

public class HealthPopUpInstantiator : MonoBehaviour
{
    [SerializeField]
    private GameObject _popUpPrefab;
    private Transform _transform;

    void Start()
    {
        _transform = GetComponent<Transform>();
        EventManager.StartListening("HpRegenerated", InstantiatePopUp);
    }

    private void InstantiatePopUp(string amount)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.tag == "Shootable")
            {
                var popup = Instantiate(_popUpPrefab, _transform);
                popup.GetComponent<HealthPopUpController>().Init(amount, hit.transform);
                popup.SetActive(true);
            }
        }
    }
}
