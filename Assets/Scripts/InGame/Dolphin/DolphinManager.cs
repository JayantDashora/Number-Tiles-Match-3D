using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DolphinManager : MonoBehaviour
{
    [SerializeField] private GameObject m_dolphin;
    [SerializeField] private float m_upDownMovementFrequency;
    private void Start() {
        RespawnDolphin();
    }
    public void OutsideScreen(){
        m_dolphin.SetActive(false);
        Invoke("RespawnDolphin",Random.Range(1.0f,3.0f));
    }

    // Respawn the dolphin after some time
    private void RespawnDolphin(){
        if(Random.Range(0,2) == 1){
            m_dolphin.transform.position = new Vector3(15,-0.20f,Random.Range(9.0f,10.0f));
            m_dolphin.transform.rotation = Quaternion.Euler(0,Random.Range(-95,-70),0);
            m_dolphin.SetActive(true);
        }
        else{
            m_dolphin.transform.position = new Vector3(-15,-0.20f,Random.Range(9.0f,10.0f));
            m_dolphin.transform.rotation = Quaternion.Euler(0,Random.Range(95,70),0);
            m_dolphin.SetActive(true);
        }

    }
}
