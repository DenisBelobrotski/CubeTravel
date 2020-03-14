using UnityEngine;

public class RotatingObstacle : Obstacle
{
    [SerializeField] RotatingObject rotatingObject;


    void Awake()
    {
        rotatingObject.IsRotationEnabled = true;
    }
}
