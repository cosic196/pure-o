using UnityEngine;

public class EnemyAppearManager : MonoBehaviour {
    
    void Start()
    {
        EventManager.StartListening("EnemiesAppearedKoreo", EnemyAppear);
    }

    private void EnemyAppear(string indexString)
    {
        EventManager.TriggerEvent("EnemiesAppeared", indexString);
    }
}