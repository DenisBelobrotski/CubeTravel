using System.Collections.Generic;
using UnityEngine;

public class FanObstacle : Obstacle
{
    [SerializeField] List<RotatingObject> fans;


    void Awake()
    {
        for (int i = 0; i < fans.Count; i++)
        {
            fans[i].IsRotationEnabled = true;
        }
    }
}
