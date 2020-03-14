using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Levels/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] Vector2Int platformWeightsRange;

    public Vector2Int PlatformWeightsRange => platformWeightsRange;
}
