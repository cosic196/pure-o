﻿using System;
using UnityEngine;

public class HpController : MonoBehaviour {

    [SerializeField]
    private float _maxHp;
    private float _currentHp;
    [SerializeField]
    private float _regen;
    [SerializeField]
    private float _missedNoteDamage;
    [SerializeField]
    private bool _autoRegen = false;
    [SerializeField]
    private float _regenerationSpeed;
    private bool _dead = false;
    private bool _canRegen = true;

    public float CurrentHp
    {
        get
        {
            return _currentHp;
        }
        set
        {
            if (_dead)
                return;
            float returnValue;
            if (value < 0)
                returnValue = 0;
            else if (value > _maxHp)
                returnValue = _maxHp;
            else
                returnValue = value;
            _currentHp = returnValue;
            EventManager.TriggerEvent("HpChanged", returnValue.ToString() + "/" + _maxHp.ToString());
        }
    }
    
	void Start () {
        EventManager.StartListening("NoteOutOfRange", Damaged);
        EventManager.StartListening("AnEnemyDied", Regenerate);
        EventManager.StartListening("AnEnemyWasShot", RegenerateALittle);
        EventManager.StartListening("OutOfRhythmShot", DamagedNoInput);
        EventManager.StartListening("DamagePlayerByInput", DamagedByInput);
        EventManager.StartListening("DisableRegen", () => { _canRegen = false; });
        EventManager.StartListening("EnableRegen", () => { _canRegen = true; });
        _currentHp = _maxHp;
	}

    private void RegenerateALittle()
    {
        if(!_canRegen)
        {
            return;
        }
        if (CurrentHp == _maxHp)
        {
            return;
        }
        CurrentHp += _regen / 3f;
        EventManager.TriggerEvent("HpRegenerated", (_regen / 4f).ToString());
    }

    private void Regenerate()
    {
        if(!_canRegen)
        {
            return;
        }
        if(CurrentHp == _maxHp)
        {
            return;
        }
        CurrentHp += _regen;
        EventManager.TriggerEvent("HpRegenerated", _regen.ToString());
    }

    private void Damaged(string noteInfo)
    {
        if (!enabled)
            return;
        if (_dead)
            return;
        CurrentHp -= _missedNoteDamage;
        EventManager.TriggerEvent("PlayerDamaged");
        if(CurrentHp <= 0f)
        {
            _dead = true;
            EventManager.TriggerEvent("PlayerDied");
        }
    }

    private void DamagedByInput(string input)
    {
        if (!enabled)
            return;
        if (_dead)
            return;
        var damage = float.Parse(input);
        CurrentHp -= _maxHp * (damage / 100f);
        //EventManager.TriggerEvent("PlayerDamaged");
        if (CurrentHp <= 0f)
        {
            _dead = true;
            EventManager.TriggerEvent("PlayerDied");
        }
    }

    private void DamagedNoInput()
    {
        if (!enabled)
            return;
        if (_dead)
            return;
        CurrentHp -= _missedNoteDamage;
        EventManager.TriggerEvent("PlayerDamaged");
        if (CurrentHp <= 0f)
        {
            _dead = true;
            EventManager.TriggerEvent("PlayerDied");
        }
    }

    void Update () {
        if(!_autoRegen)
        {
            return;
        }
		if(CurrentHp < _maxHp)
        {
            CurrentHp += _regenerationSpeed * CustomTime.GetDeltaTime();
        }
        if(CurrentHp > _maxHp)
        {
            CurrentHp = _maxHp;
        }
	}
}
