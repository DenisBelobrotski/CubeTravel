using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField] Transform rotatableRoot;
    [SerializeField] float angularVelocity;


    public bool IsRotationEnabled { get; set; }


    void Update()
    {
        if (IsRotationEnabled)
        {
            Quaternion rotation = Quaternion.Euler(0.0f, angularVelocity * Time.deltaTime, 0.0f);
            rotatableRoot.localRotation *= rotation;
        }
    }


    public void ResetRotation()
    {
        rotatableRoot.localRotation = Quaternion.identity;
    }
}
