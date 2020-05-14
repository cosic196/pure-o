using System.Collections.Generic;
using UnityEngine;

public class FamilyMemberController : Shootable
{
    [SerializeField]
    private int _hp;
    [SerializeField]
    private List<int> _triggerAnimationOn;
    
    public override void Shot(string shootInfo)
    {
        if(_hp <= 0)
        {
            return;
        }
        _hp--;
        if(_triggerAnimationOn.Contains(_hp))
        {
            EventManager.TriggerEvent("FamilyMemberAnimate");
        }
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
