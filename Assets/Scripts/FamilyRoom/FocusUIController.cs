using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FocusUIController : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private List<string> _focusedItemsNamesAndDescriptions;
    [SerializeField]
    private TextMeshProUGUI _descTMPro;
    private Animator _animator;
    private Dictionary<string, string> _focusedItemsDictionary;
    private bool _focused = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        ParseFocusedItems();
        EventManager.StartListening("FocusedAnObject", TryFocusOnObject);
        EventManager.StartListening("Unfocused", Unfocus);
    }

    private void Unfocus()
    {
        if(_focused)
        {
            _animator.SetTrigger("Hide");
            _focused = false;
        }
    }

    private void TryFocusOnObject(string focusedObjectName)
    {
        string desc = "";
        if(_focusedItemsDictionary.TryGetValue(focusedObjectName, out desc))
        {
            _focused = true;
            _descTMPro.text = desc;
            _animator.SetTrigger("Show");
        }
    }

    private void ParseFocusedItems()
    {
        _focusedItemsDictionary = new Dictionary<string, string>();
        foreach (var nameDesc in _focusedItemsNamesAndDescriptions)
        {
            _focusedItemsDictionary.Add(nameDesc.Split('-')[0], nameDesc.Split('-')[1]);
        }
    }
}
