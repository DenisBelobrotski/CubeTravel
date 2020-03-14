using System;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public event Action OnExitReached;
    public event Action OnObstacleHit;
    
    [SerializeField] RotatingObject rotatingObject;
    [SerializeField] ColliderWrapper mainCollider;
    
    int platformExitLayer;
    int obstacleLayer;


    bool IsExitTriggering { get; set; }
    
    public bool IsEnabled { get; set; }


    void Awake()
    {
        platformExitLayer = LayerMask.NameToLayer("PlatformExit");
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }


    void OnEnable()
    {
        mainCollider.OnTriggerEnterEvent += ColliderWrapper_OnTriggerEnter;
        mainCollider.OnTriggerExitEvent += ColliderWrapper_OnTriggerExit;
    }


    void OnDisable()
    {
        mainCollider.OnTriggerEnterEvent -= ColliderWrapper_OnTriggerEnter;
        mainCollider.OnTriggerExitEvent -= ColliderWrapper_OnTriggerExit;
    }


    void Update()
    {
        rotatingObject.IsRotationEnabled = Input.GetMouseButton(0) && IsEnabled;

        if (IsExitTriggering && Input.GetMouseButtonUp(0) && IsEnabled)
        {
            OnExitReached?.Invoke();
            IsExitTriggering = false;
        }
    }


    public void ResetRotation()
    {
        transform.localRotation = Quaternion.identity;
        rotatingObject.ResetRotation();
    }


    void ColliderWrapper_OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == platformExitLayer)
        {
            IsExitTriggering = true;
        }
        else if (other.gameObject.layer == obstacleLayer)
        {
            OnObstacleHit?.Invoke();
            rotatingObject.ResetRotation();
        }
    }
    
    
    void ColliderWrapper_OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == platformExitLayer)
        {
            IsExitTriggering = false;
        }
    }
}
