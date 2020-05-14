using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradesController : MonoBehaviour
{
    [SerializeField]
    private List<UpgradePrefabPair> _upgradesControllersPrefabs;
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    void Start()
    {
        SaveData saveData = SaveDataManager.Load();
        float bond = 0f;

        foreach (var upgrade in UpgradesGlobal.Upgrades)
        {
            foreach (var upgradePrefabPair in _upgradesControllersPrefabs.Where(x => x.UpgradeName == upgrade.UpgradeName))
            {
                foreach (var completedLevel in saveData.Levels.Where(x => x.Completed))
                {
                    bond += completedLevel.FamilyMember.Bond;
                    if (bond >= upgrade.bondRequirement)
                    {
                        if (saveData.FamilyMembersAlive.Contains(completedLevel.FamilyMember.Name))
                        {
                            Instantiate(upgradePrefabPair.Prefab, _transform);
                        }
                        break;
                    }
                }
            }
        }
    }

    [Serializable]
    private class UpgradePrefabPair
    {
        public UpgradeName UpgradeName;
        public GameObject Prefab;
    }
}
