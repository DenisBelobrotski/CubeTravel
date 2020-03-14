using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] string guiManagerPrefabPath;
    [SerializeField] string levelsManagerPrefabPath;


    void Awake()
    {
        Instantiate(Resources.Load<GameObject>(guiManagerPrefabPath), transform);
        Instantiate(Resources.Load<GameObject>(levelsManagerPrefabPath), transform);
    }
}
