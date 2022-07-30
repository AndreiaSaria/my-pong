using System;
using UnityEngine;

[RequireComponent(typeof (BoxCollider2D))]
public class GoalDetector : MonoBehaviour
{
    public event Action GoalDetected;
    private void OnTriggerEnter2D(Collider2D col)
    { //Can check if it is ball. Not needed in our case.
        GoalDetected?.Invoke();
    }
}
