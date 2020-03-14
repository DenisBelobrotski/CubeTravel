using System;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : GuiScreen
{
    public static event Action OnRestartButtonClicked;
    public static event Action OnSkipButtonClicked;

    const string LevelCounterFormat = "LEVEL {0}";
    const string PlatformProgressFormat = "{0}/{1}";

    [SerializeField] Text levelCounterLabel;
    [SerializeField] Text platformProgressLabel;
    [SerializeField] Button restartButton;
    [SerializeField] Button skipButton;


    int LevelCounter
    {
        set => levelCounterLabel.text = string.Format(LevelCounterFormat, value);
    }


    void OnEnable()
    {
        Level.OnCurrentPlatformChanged += Level_OnCurrentPlatformChanged;
        restartButton.onClick.AddListener(RestartButton_OnClick);
        skipButton.onClick.AddListener(SkipButton_OnClick);
    }


    void OnDisable()
    {
        Level.OnCurrentPlatformChanged -= Level_OnCurrentPlatformChanged;
        restartButton.onClick.RemoveListener(RestartButton_OnClick);
        skipButton.onClick.RemoveListener(SkipButton_OnClick);
    }


    public void Initialize(int levelNumber, int platformsCount)
    {
        LevelCounter = levelNumber + 1;
        UpdatePlatformProgress(0, platformsCount);
    }


    void UpdatePlatformProgress(int completedPlatformsCount, int maxPlatformsCount)
    {
        platformProgressLabel.text = string.Format(PlatformProgressFormat, completedPlatformsCount, maxPlatformsCount);
    }


    void Level_OnCurrentPlatformChanged(Level level)
    {
        UpdatePlatformProgress(level.CurrentPlatformIndex, level.PlatformsMaxCount);
    }


    void RestartButton_OnClick()
    {
        OnRestartButtonClicked?.Invoke();
    }


    void SkipButton_OnClick()
    {
        OnSkipButtonClicked?.Invoke();
    }
}
