using UnityEngine;
using UnityEngine.UI;

public class HpBarAnimation : MonoBehaviour {

    private Slider _slider;

	void Start () {
        _slider = GetComponent<Slider>();
        EventManager.StartListening("HpChanged", ChangeFill);
	}

    private void ChangeFill(string hpSlashMaxHp)
    {
        float currentHp = float.Parse(hpSlashMaxHp.Split('/')[0]);
        float maxHp = float.Parse(hpSlashMaxHp.Split('/')[1]);
        _slider.normalizedValue = currentHp / maxHp;
    }
}
