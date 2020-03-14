using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static event Action<LevelsManager> OnLevelCreated;
    public static event Action<LevelsManager> OnLevelRestarted;
    public static event Action OnLevelCompleted;
    public static event Action OnLevelFailed;

    [SerializeField] CameraBehaviour mainCamera;
    [SerializeField] string levelPrefabPath;
    [SerializeField] string levelConfigsRootPath;
    [SerializeField] List<string> levelConfigsFileNames;

    Level currentLevel = null;


    public int CurrentLevelIndex
    {
        get => PlayerPrefsWrapper.CurrentLevelIndex;
        private set => PlayerPrefsWrapper.CurrentLevelIndex = value;
    }

    public Level CurrentLevel => currentLevel;


    void Awake()
    {
        CreateCurrentLevel();
    }


    void OnEnable()
    {
        GameScreen.OnRestartButtonClicked += GameScreen_OnRestartButtonClicked;
        GameScreen.OnSkipButtonClicked += GameScreen_OnSkipButtonClicked;
        WinScreen.OnContinueButtonClicked += WinScreen_OnContinueButtonClicked;
        LooseScreen.OnRetryButtonClicked += LooseScreen_OnRetryButtonClicked;
    }


    void OnDisable()
    {
        GameScreen.OnRestartButtonClicked -= GameScreen_OnRestartButtonClicked;
        GameScreen.OnSkipButtonClicked -= GameScreen_OnSkipButtonClicked;
        WinScreen.OnContinueButtonClicked -= WinScreen_OnContinueButtonClicked;
        LooseScreen.OnRetryButtonClicked -= LooseScreen_OnRetryButtonClicked;
    }


    void CreateCurrentLevel()
    {
        GameObject levelPrefab = Resources.Load<GameObject>(levelPrefabPath);
        string levelConfigFileName = levelConfigsFileNames[CurrentLevelIndex % levelConfigsFileNames.Count];
        string levelConfigPath = $"{levelConfigsRootPath}/{levelConfigFileName}";
        LevelConfig levelConfig = Resources.Load<LevelConfig>(levelConfigPath);

        currentLevel = Instantiate(levelPrefab, transform).GetComponent<Level>();
        currentLevel.Initialize(levelConfig);
        currentLevel.OnFinished += Level_OnFinished;

        mainCamera.UpdateTargetPosition(currentLevel);

        OnLevelCreated?.Invoke(this);
    }


    void CreateNextLevel()
    {
        currentLevel.OnFinished -= Level_OnFinished;

        Destroy(currentLevel.gameObject);
        currentLevel = null;

        CurrentLevelIndex += 1;

        CreateCurrentLevel();
    }


    void RestartCurrentLevel()
    {
        currentLevel.Restart();
        OnLevelRestarted?.Invoke(this);
    }


    void Level_OnFinished(bool success)
    {
        if (success)
        {
            OnLevelCompleted?.Invoke();
        }
        else
        {
            OnLevelFailed?.Invoke();
        }
    }


    void GameScreen_OnRestartButtonClicked()
    {
        RestartCurrentLevel();
    }


    void GameScreen_OnSkipButtonClicked()
    {
        CreateNextLevel();
    }


    void WinScreen_OnContinueButtonClicked()
    {
        CreateNextLevel();
    }


    void LooseScreen_OnRetryButtonClicked()
    {
        RestartCurrentLevel();
    }
}
