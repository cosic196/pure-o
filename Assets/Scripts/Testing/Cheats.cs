﻿using UnityEngine;

public class Cheats : MonoBehaviour {

    [SerializeField]
    private HpController _hpController;

    private void Awake()
    {
        if(!Debug.isDebugBuild)
        {
            enabled = false;
        }
    }

    void Update () {
		if(Input.GetKeyDown(KeyCode.F1))
        {
            if(_hpController.enabled)
            {
                _hpController.enabled = false;
            }
            else
            {
                _hpController.enabled = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.F3))
        {
            Time.timeScale += 0.1f;
            Debug.Log(Time.timeScale);
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            Time.timeScale -= 0.1f;
            Debug.Log(Time.timeScale);
        }
        if(Input.GetKeyDown(KeyCode.F4))
        {
            Time.timeScale = 1f;
            _hpController.enabled = true;
        }
	}
}
