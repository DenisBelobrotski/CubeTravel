using System;
using UnityEngine;

public class ColliderWrapper : MonoBehaviour
{
    public event Action<Collision> OnCollisionEnterEvent;
    public event Action<Collision> OnCollisionExitEvent;
    public event Action<Collider> OnTriggerEnterEvent;
    public event Action<Collider> OnTriggerExitEvent;


    void OnCollisionEnter(Collision other)
    {
        OnCollisionEnterEvent?.Invoke(other);
    }


    void OnCollisionExit(Collision other)
    {
        OnCollisionExitEvent?.Invoke(other);
    }


    void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }


    void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(other);
    }
}
