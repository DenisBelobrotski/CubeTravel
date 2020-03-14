using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    const float MovementLerpFactor = 0.05f;
    
    [SerializeField] float distanceToPlatform;

    Vector3 targetPosition;


    void OnEnable()
    {
        Level.OnCurrentPlatformChanged += Level_OnCurrentPlatformChanged;
    }


    void OnDisable()
    {
        Level.OnCurrentPlatformChanged -= Level_OnCurrentPlatformChanged;
    }


    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, targetPosition, MovementLerpFactor);
    }


    public void UpdateTargetPosition(Level level)
    {
        targetPosition = level.CurrentPlatform.transform.position - transform.forward * distanceToPlatform;
    }


    void Level_OnCurrentPlatformChanged(Level level)
    {
        UpdateTargetPosition(level);
    }
}
