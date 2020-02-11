using UnityEngine;

[RequireComponent(typeof (GameObjectEventManager))]
public class EnemyCollisionController : Shootable {

    private GameObjectEventManager gameObjectEventManager;

    private void Start()
    {
        gameObjectEventManager = GetComponent<GameObjectEventManager>();
    }

    public override void Shot(string shootInfo)
    {
        gameObjectEventManager.TriggerEvent("Shot", shootInfo);
    }
}