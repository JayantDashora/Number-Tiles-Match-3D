using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dolphin : MonoBehaviour
{
    // Variables 
    [SerializeField] private float m_movementSpeed;
    [SerializeField] private DolphinManager m_dolphinManager;
    [SerializeField] private float m_bound;
    public Action OnOutsideScreen;

    private void Update() {
        Swim();
        CheckBounds(m_bound);
        transform.Translate(m_movementSpeed * Time.deltaTime * Vector3.forward);
    }

    private void CheckBounds(float _val){
        if(Mathf.Abs(transform.position.x) > _val)
            m_dolphinManager.OutsideScreen();
    }

    private void Swim(){

    }
}
