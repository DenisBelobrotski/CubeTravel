using UnityEngine;

public static class PlayerPrefsWrapper
{
    const string CurrentLevelIndexKey = "CurrentLevelIndex";

    public static int CurrentLevelIndex
    {
        get => PlayerPrefs.GetInt(CurrentLevelIndexKey, 0);
        set => PlayerPrefs.SetInt(CurrentLevelIndexKey, value);
    }
}
