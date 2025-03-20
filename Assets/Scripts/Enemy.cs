using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    private bool isFinished = false;
    
    private Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;


    void Start()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        SetRagdollActive(false);
    }
    
    void SetRagdollActive(bool isActive)
    {
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = !isActive;
        }
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = isActive;
        }

        GetComponent<Collider>().enabled = !isActive;
        GetComponent<Rigidbody>().isKinematic = isActive;
    }

    public void FinishEnemy()
    {
        isFinished = true;

        if (animator != null)
        {
            animator.enabled = false;
        }

        SetRagdollActive(true);
        UIManager.Instance.ShowFinishPrompt(false);

        Destroy(gameObject, 5f);
    }

}
