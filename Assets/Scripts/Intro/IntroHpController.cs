﻿using System;
using UnityEngine;

public class IntroHpController : MonoBehaviour
{

    [SerializeField]
    private float _maxHp;
    private float _currentHp;
    [SerializeField]
    private float _noteHitRegen;
    [SerializeField]
    private float _missedNoteDamage;
    [SerializeField]
    private bool _autoRegen = false;
    [SerializeField]
    private float _regenerationSpeed;
    private bool _dead = false;

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

    void Start()
    {
        EventManager.StartListening("NoteOutOfRange", Damaged);
        EventManager.StartListening("BrainShot", Regenerate);
        EventManager.StartListening("OutOfRhythmShot", DamagedNoInput);
        _currentHp = 0;
    }

    private void Regenerate(string json)
    {
        if (CurrentHp == _maxHp)
        {
            return;
        }
        CurrentHp += _noteHitRegen;
    }

    private void Damaged(string noteInfo)
    {
        if (!enabled)
            return;
        CurrentHp -= _missedNoteDamage;
        EventManager.TriggerEvent("PlayerDamaged");
    }

    private void DamagedNoInput()
    {
        if (!enabled)
            return;
        CurrentHp -= _missedNoteDamage;
        EventManager.TriggerEvent("PlayerDamaged");
    }

    void Update()
    {
        if (!_autoRegen)
        {
            return;
        }
        if (CurrentHp < _maxHp)
        {
            CurrentHp += _regenerationSpeed * CustomTime.GetDeltaTime();
        }
        if (CurrentHp > _maxHp)
        {
            CurrentHp = _maxHp;
        }
    }
}
