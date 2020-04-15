using UnityEngine;

public class BrainController : Shootable {

    private int _lives = 3;
    [SerializeField]
    IntroHpController _playerHpController;

    private void Start()
    {
        EventManager.StartListening("HpChanged", OnHpChanged);
    }

    public override void Shot(string shootInfo)
    {
        EventManager.TriggerEvent("BrainShot");
        EventManager.TriggerEvent("BrainShot", "");
    }

    private void OnHpChanged(string hpByMaxHp)
    {
        float _hp = float.Parse(hpByMaxHp.Split('/')[0]) / float.Parse(hpByMaxHp.Split('/')[1]);
        if (_hp >= 1)
        {
            _lives--;
            if (_lives <= 0)
            {
                EventManager.TriggerEvent("BrainDied");
                EventManager.TriggerEvent("AnEnemyDied");
                return;
            }
            _playerHpController.CurrentHp = 0;
            EventManager.TriggerEvent("BrainLostLife", _lives.ToString());
            EventManager.TriggerEvent("AnEnemyDied");
            if (_lives == 2)
            {
                EventManager.TriggerEvent("StartSpawningRight");
            }
            else if (_lives == 1)
            {
                EventManager.TriggerEvent("StartSpawningLeft");
            }
        }
    }
}
