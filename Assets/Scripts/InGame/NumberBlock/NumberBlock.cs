using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberBlock : MonoBehaviour
{
    [SerializeField] private BlocksDataSO m_blocksData;
    [SerializeField] private int m_number;
    [SerializeField] private Color m_color;
    [SerializeField] private Material m_material;
    [SerializeField] private Material m_transparent;

    private void Start() {
        int number = transform.GetChild(0).GetComponent<CubeNumber>().number;
        Color color = m_blocksData.m_blockColors[(int)(Mathf.Log((float) number, 2.0f))-1];
        transform.GetChild(0).GetComponent<Renderer>().material.color = color;
        m_number = number;
        m_color = color;
    
    }
    private void OnEnable() {
        Bucket.m_cannotSelectBlock += StopBlockSelection;
        Bucket.m_canSelectBlock += StartBlockSelection;
    }

    private void OnDisable() {
        Bucket.m_cannotSelectBlock -= StopBlockSelection;
        Bucket.m_canSelectBlock -= StartBlockSelection;
    }

    private void StartBlockSelection()
    {
        if(!transform.CompareTag("NumberBlock"))
            return;

        transform.GetChild(0).GetComponent<Renderer>().material = m_material;
        transform.GetChild(0).GetComponent<Renderer>().material.color = m_color;
        transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
    }

    private void StopBlockSelection()
    {
        if(!transform.CompareTag("NumberBlock"))
            return;
            
        transform.GetChild(0).GetComponent<Renderer>().material = m_transparent;
        transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);       
    }
}
