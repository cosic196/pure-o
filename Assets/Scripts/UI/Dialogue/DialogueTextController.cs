using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTextController : MonoBehaviour {

    [Header("Dialogue")]
    [SerializeField]
    private List<string> _nextLineTriggers;
    [SerializeField]
    private List<string> _dialogueLines;
    [Header("Names")]
    [SerializeField]
    private List<string> _nextNameTriggers;
    [SerializeField]
    private List<string> _names;
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI _dialogueTmPro;
    [SerializeField]
    private TextMeshProUGUI _nameTmPro;
    [SerializeField]
    private AudioSource _audioSource;
    [Space(20)]
    [SerializeField]
    private string _closeDialogueTrigger;
    [SerializeField]
    private bool _closeDialogueAfterLastLine;
    [SerializeField]
    private string _onCloseDialogue;
    [SerializeField]
    private bool _playAudioOnCloseDialogue;

    private TypewriterEffect _typewriterEffect;
    private int _dialoguePosition = 0;
    private int _namePosition = 0;

    private Animator _animator;

	void Start () {
        _typewriterEffect = _dialogueTmPro.GetComponent<TypewriterEffect>();
        _animator = GetComponent<Animator>();
        foreach (var trigger in _nextLineTriggers)
        {
            EventManager.StartListening(trigger, ChangeDialogue);
        }
        foreach (var trigger in _nextNameTriggers)
        {
            EventManager.StartListening(trigger, ChangeName);
        }
        if(!string.IsNullOrEmpty(_closeDialogueTrigger))
        {
            EventManager.StartListening(_closeDialogueTrigger, CloseDialogue);
        }
	}

    private void ChangeDialogue()
    {
        if(_dialoguePosition >= _dialogueLines.Count)
        {
            if(_closeDialogueAfterLastLine)
            {
                CloseDialogue();
            }
            return;
        }
        _dialogueTmPro.text = _dialogueLines[_dialoguePosition];
        _dialoguePosition++;
        _typewriterEffect.Play();
        _audioSource.Play();
    }

    private void ChangeName()
    {
        if (_namePosition >= _names.Count)
        {
            return;
        }
        _nameTmPro.text = _names[_namePosition];
        _namePosition++;
    }

    private void CloseDialogue()
    {
        if(_playAudioOnCloseDialogue)
        {
            _audioSource.Play();
        }
        _animator.SetTrigger("CloseDialogue");
        if(!string.IsNullOrEmpty(_onCloseDialogue))
        {
            EventManager.TriggerEvent(_onCloseDialogue);
        }
    }

    public void AddDialogueLine(string newDialogueLine)
    {
        _dialogueLines.Add(newDialogueLine);
    }
}
