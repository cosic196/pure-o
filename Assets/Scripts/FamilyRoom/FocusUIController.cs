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

    void Start()
    {
        _animator = GetComponent<Animator>();
        ParseFocusedItems();
        EventManager.StartListening("FocusedAnObject", TryFocusOnObject);
        EventManager.StartListening("Unfocused", Unfocus);
    }

    private void Unfocus()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("ShowFocusUI"))
        _animator.SetTrigger("Hide");
    }

    private void TryFocusOnObject(string focusedObjectName)
    {
        string desc = "";
        if(_focusedItemsDictionary.TryGetValue(focusedObjectName, out desc))
        {
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
