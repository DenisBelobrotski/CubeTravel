using UnityEngine;

public class GuiManager : MonoBehaviour
{
    [SerializeField] Transform screensRoot;
    [SerializeField] string screenPrefabsRootPath;
    [SerializeField] string gameScreenPrefabFileName;
    [SerializeField] string winScreenPrefabFileName;
    [SerializeField] string looseScreenPrefabFileName;

    GuiScreen currentScreen = null;


    void OnEnable()
    {
        LevelsManager.OnLevelCreated += LevelsManager_OnLevelCreated;
        LevelsManager.OnLevelRestarted += LevelsManager_OnLevelRestarted;
        LevelsManager.OnLevelCompleted += LevelsManager_OnLevelCompleted;
        LevelsManager.OnLevelFailed += LevelsManager_OnLevelFailed;
    }


    void OnDisable()
    {
        LevelsManager.OnLevelCreated -= LevelsManager_OnLevelCreated;
        LevelsManager.OnLevelRestarted -= LevelsManager_OnLevelRestarted;
        LevelsManager.OnLevelCompleted -= LevelsManager_OnLevelCompleted;
        LevelsManager.OnLevelFailed -= LevelsManager_OnLevelFailed;
    }
    
    
    T ShowScreen<T>(string screenPrefabFileName) where T : GuiScreen
    {
        if (currentScreen != null)
        {
            Destroy(currentScreen.gameObject);
        }

        GameObject screenPrefab = Resources.Load<GameObject>($"{screenPrefabsRootPath}/{screenPrefabFileName}");
        T screen = Instantiate(screenPrefab, screensRoot).GetComponent<T>();

        currentScreen = screen;

        return screen;
    }


    void ShowGameScreen(int levelNumber, int platformsMaxCount)
    {
        GameScreen gameScreen = ShowScreen<GameScreen>(gameScreenPrefabFileName);
        gameScreen.Initialize(levelNumber, platformsMaxCount);
    }


    void ShowWinScreen()
    {
        ShowScreen<WinScreen>(winScreenPrefabFileName);
    }


    void ShowLooseScreen()
    {
        ShowScreen<LooseScreen>(looseScreenPrefabFileName);
    }


    void LevelsManager_OnLevelCreated(LevelsManager levelsManager)
    {
        ShowGameScreen(levelsManager.CurrentLevelIndex, levelsManager.CurrentLevel.PlatformsMaxCount);
    }
    
    
    void LevelsManager_OnLevelRestarted(LevelsManager levelsManager)
    {
        ShowGameScreen(levelsManager.CurrentLevelIndex, levelsManager.CurrentLevel.PlatformsMaxCount);
    }


    void LevelsManager_OnLevelCompleted()
    {
        ShowWinScreen();
    }
    
    
    void LevelsManager_OnLevelFailed()
    {
        ShowLooseScreen();
    }
}
