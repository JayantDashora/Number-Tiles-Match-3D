using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberBlock : MonoBehaviour
{
    [SerializeField] private BlocksDataSO m_blocksData;

    private void Start() {
        int number = transform.GetChild(0).GetComponent<CubeNumber>().number;
        transform.GetChild(0).GetComponent<Renderer>().material.color = m_blocksData.m_blockColors[(int)(Mathf.Log((float) number, 2.0f))-1];
    }
}
