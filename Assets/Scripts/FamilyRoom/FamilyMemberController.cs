using UnityEngine;

public class FamilyMemberController : Shootable
{
    [SerializeField]
    private int _hp;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public override void Shot(string shootInfo)
    {
        if(_hp <= 0)
        {
            return;
        }
        _hp--;
        if(_hp > 0)
        {
            EventManager.TriggerEvent("FamilyMemberDamaged");
        }
        else
        {
            EventManager.TriggerEvent("FamilyMemberDied");
        }
    }
}
