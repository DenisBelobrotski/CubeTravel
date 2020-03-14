using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    const int ObstaclesGeneratorMaxRetryCount = 3;

    public static event Action<Level> OnCurrentPlatformChanged;

    public event Action<bool> OnFinished;

    [SerializeField] string platformPrefabPath;
    [SerializeField] string mainCharacterPrefabPath;
    [SerializeField] int platformsCount;
    [SerializeField] float distanceBetweenPlatforms;
    [SerializeField] string obstaclesRootPath;
    [SerializeField] List<string> obstaclePrefabFileNames;

    GameObject platformPrefab = null;
    GameObject mainCharacterPrefab = null;

    readonly List<Platform> platforms = new List<Platform>();
    int currentPlatformIndex = 0;
    MainCharacter mainCharacter = null;


    public int CurrentPlatformIndex
    {
        get => currentPlatformIndex;

        private set
        {
            currentPlatformIndex = value;
            OnCurrentPlatformChanged?.Invoke(this);
        }
    }

    public Platform CurrentPlatform => platforms[currentPlatformIndex];

    public int PlatformsMaxCount => platformsCount;

    bool IsMainCharacterEnabled
    {
        get => mainCharacter.IsEnabled;

        set
        {
            if (mainCharacter.IsEnabled != value)
            {
                mainCharacter.IsEnabled = value;

                if (value)
                {
                    mainCharacter.OnExitReached += MainCharacter_OnExitReached;
                    mainCharacter.OnObstacleHit += MainCharacter_OnObstacleHit;
                }
                else
                {
                    mainCharacter.OnExitReached -= MainCharacter_OnExitReached;
                    mainCharacter.OnObstacleHit -= MainCharacter_OnObstacleHit;
                }
            }
        }
    }


    public void Initialize(LevelConfig levelConfig)
    {
        platformPrefab = Resources.Load<GameObject>(platformPrefabPath);
        mainCharacterPrefab = Resources.Load<GameObject>(mainCharacterPrefabPath);

        Platform startPlatform = GeneratePlatform(levelConfig);
        platforms.Add(startPlatform);
        CurrentPlatformIndex = 0;

        mainCharacter = Instantiate(mainCharacterPrefab).GetComponent<MainCharacter>();
        IsMainCharacterEnabled = true;
        startPlatform.AddObject(mainCharacter.transform);

        for (int i = 1; i < platformsCount; i++)
        {
            platforms.Add(GeneratePlatform(levelConfig));
            platforms[i].transform.position = platforms[i - 1].transform.position + Vector3.forward * distanceBetweenPlatforms;

            var range = levelConfig.PlatformWeightsRange;
            AddObstacles(platforms[i], Random.Range(range.x, range.y + 1));
        }
    }


    public void Restart()
    {
        if (platforms.Count > 0)
        {
            CurrentPlatformIndex = 0;
            platforms[CurrentPlatformIndex].AddObject(mainCharacter.transform);
            mainCharacter.ResetRotation();
            IsMainCharacterEnabled = true;
        }
    }


    Platform GeneratePlatform(LevelConfig levelConfig)
    {
        Platform platform = Instantiate(platformPrefab, transform).GetComponent<Platform>();

        return platform;
    }


    void AddObstacles(Platform platform, int platformWeight)
    {
        int retryCount = 0;

        GameObject randomObstaclePrefab = null;
        Obstacle obstacleBehaviour = null;

        do
        {
            randomObstaclePrefab = GetRandomObstaclePrefab();
            obstacleBehaviour = randomObstaclePrefab.GetComponent<Obstacle>();
        }
        while (obstacleBehaviour.Weight > platformWeight && retryCount < ObstaclesGeneratorMaxRetryCount);

        int obstaclesCount = platformWeight / obstacleBehaviour.Weight;

        for (int i = 0; i < obstaclesCount; i++)
        {
            platform.AddObject(Instantiate(randomObstaclePrefab).transform, true);
        }
    }


    GameObject GetRandomObstaclePrefab()
    {
        int randomPrefabIndex = Random.Range(0, obstaclePrefabFileNames.Count);
        string obstaclePrefabFilePath = $"{obstaclesRootPath}/{obstaclePrefabFileNames[randomPrefabIndex]}";
        GameObject randomObstaclePrefab = Resources.Load<GameObject>(obstaclePrefabFilePath);

        return randomObstaclePrefab;
    }


    void Finish(bool success)
    {
        IsMainCharacterEnabled = false;
        OnFinished?.Invoke(success);
    }


    void MainCharacter_OnExitReached()
    {
        if (CurrentPlatformIndex < platforms.Count - 1)
        {
            CurrentPlatformIndex += 1;
            platforms[CurrentPlatformIndex].AddObject(mainCharacter.transform);
            mainCharacter.ResetRotation();
        }
        else if (CurrentPlatformIndex == platforms.Count - 1)
        {
            Finish(true);
        }
    }


    void MainCharacter_OnObstacleHit()
    {
        Finish(false);
    }
}
