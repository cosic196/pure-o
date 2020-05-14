using System.Collections;
using UnityEngine;

public class FamilyRoomEndTimer : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSourceToWait;

    void Start()
    {
        EventManager.StartListening("FamilyMemberDied", StartTimer);
    }

    private void StartTimer()
    {
        StartCoroutine(CoroutineTimer());
    }

    private IEnumerator CoroutineTimer()
    {
        yield return new WaitForSeconds(_audioSourceToWait.clip.length - 3f);
        EventManager.TriggerEvent("FadeOut", "LoadNextLevel");
    }
}
