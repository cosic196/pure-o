using UnityEngine;

public class FamilyMenuUpgradesInstantiator : MonoBehaviour {

    [SerializeField]
    private GameObject _upgradePrefab;
    [SerializeField]
    private RectTransform _parent;

	void Start () {
        foreach (var upgrade in UpgradesGlobal.Upgrades)
        {
            var upgradeTransform = Instantiate(_upgradePrefab, _parent).GetComponent<RectTransform>();
            upgradeTransform.anchorMin = new Vector2(upgrade.bondRequirement * 0.5f, 1f);
            upgradeTransform.anchorMax = new Vector2(upgrade.bondRequirement * 1.5f, 1f);
            upgradeTransform.GetComponent<FamilyMenuUpgradeController>().Init(upgrade);
        }
    }
}
