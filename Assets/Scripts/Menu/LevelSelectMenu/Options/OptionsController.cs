using UnityEngine;

public class OptionsController : MonoBehaviour {

    IOption[] options;

	void Start () {
        options = GetComponents<IOption>();
	}

    public void Accept()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].Apply();
        }
    }

    public void Reset()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].Reset();
        }
    }
}

public interface IOption
{
    void Apply();
    void Reset();
}