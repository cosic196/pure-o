public class EnemyHeadCollisionController : EnemyCollisionController {

    public override void Shot(string shootInfo)
    {
        base.Shot(shootInfo);
        gameObjectEventManager.TriggerEvent("Headshot", shootInfo);
        EventManager.TriggerEvent("Headshot");
    }
}
