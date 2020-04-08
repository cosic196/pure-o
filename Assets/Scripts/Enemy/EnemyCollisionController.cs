using UnityEngine;

[RequireComponent(typeof (GameObjectEventManager))]
public class EnemyCollisionController : Shootable {

    internal GameObjectEventManager gameObjectEventManager;

    internal void Start()
    {
        gameObjectEventManager = GetComponent<GameObjectEventManager>();
    }

    public override void Shot(string shootInfo)
    {
        gameObjectEventManager.TriggerEvent("Shot", shootInfo);
        EventManager.TriggerEvent("AnEnemyWasShot");
    }
}