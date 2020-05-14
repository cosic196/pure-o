using System;
using UnityEngine;

public static class CustomTime {

    public static float _customScale = 1f;
    public static float slowDownUpgradeTimeScale = 1f;

	public static float GetDeltaTime()
    {
        return Time.deltaTime * Time.timeScale * _customScale;
    }

    internal static void SetPaused()
    {
        slowDownUpgradeTimeScale = 0f;
    }

    internal static void SetUnpaused()
    {
        slowDownUpgradeTimeScale = 1f;
    }
}
