using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

    [SerializeField]
    private List<MonoBehaviour> _componentsToDisable;
    [SerializeField]
    private List<MonoBehaviour> _componentsToEnable;

    void Start () {
        EventManager.StartListening("Paused", Pause);
        EventManager.StartListening("Unpaused", Unpause);
    }
	
	private void Pause()
    {
        CustomTime._customScale = 0f;
        foreach(var component in _componentsToDisable)
        {
            component.enabled = false;
        }
        foreach (var component in _componentsToEnable)
        {
            component.enabled = true;
        }
    }

    private void Unpause()
    {
        foreach (var component in _componentsToDisable)
        {
            component.enabled = true;
        }
        foreach (var component in _componentsToEnable)
        {
            component.enabled = false;
        }
        CustomTime._customScale = 1f;
    }
}
