using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GoalBlockUI : MonoBehaviour
{
    [SerializeField] private BoxCollider m_boxColliderGoalBox;
    [SerializeField] private Rigidbody m_rigdiBodyGoalBox;
    [SerializeField] private BoxCollider m_boxColliderGroundCollider;
    [SerializeField] private Animator m_goalBannerAnimator;

    private void Start() {
        Invoke("RemoveColliders", 8f);
        Invoke("GoalBannerAnimation", 3f);
        Invoke("GoalBannerAnimation", 4f);

    }
    private void GoalBannerAnimation(){
        m_goalBannerAnimator.SetTrigger("Pop");
    }

    private void RemoveColliders(){
        Destroy(m_boxColliderGoalBox);
        Destroy(m_boxColliderGroundCollider);
        Destroy(m_rigdiBodyGoalBox);
    }
}
