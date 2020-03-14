using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Transform objectsRoot;


    public void AddObject(Transform objectTransform, bool setRandomRotation = false)
    {
        objectTransform.SetParent(objectsRoot, false);
        objectTransform.localRotation = 
            setRandomRotation ? Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f) : Quaternion.identity;
    }
}
