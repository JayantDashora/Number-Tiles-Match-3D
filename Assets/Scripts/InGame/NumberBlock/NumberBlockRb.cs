using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBlockRb : MonoBehaviour
{
    [SerializeField] private Rigidbody m_blockRb;
    private float m_gravityScale = 1;

    private void Start() {
        m_blockRb.useGravity = false;
        m_gravityScale = Random.Range(1f,7f);
        Invoke("RemoveRb",50f);
    }
    void FixedUpdate (){
        Vector3 gravity = -9.81f * m_gravityScale * Vector3.up;
        m_blockRb.AddForce(gravity, ForceMode.Acceleration);
    }
    private void RemoveRb(){
        Destroy(m_blockRb);
        Destroy(this);
    }
 

}
