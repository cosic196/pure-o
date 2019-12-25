using UnityEngine;

public static class CustomTime {

    public static float _customScale = 1f;

	public static float GetDeltaTime()
    {
        return Time.deltaTime * Time.timeScale * _customScale;
    }
}
