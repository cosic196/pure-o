using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextOnTrigger : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tmPro;
    [SerializeField]
    private string _trigger;
    [SerializeField]
    private List<string> _texts;
    private int _counter = 0;

    void Start()
    {
        if(_texts == null)
        {
            return;
        }
        EventManager.StartListening(_trigger, ChangeText);
    }

    private void ChangeText()
    {   
        if(_counter >= _texts.Count)
        {
            return;
        }
        _tmPro.text = _texts[_counter];
        _counter++;
    }
}
