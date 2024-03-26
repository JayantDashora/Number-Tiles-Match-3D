using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberBlockMenuUI : MonoBehaviour
{
    private int m_currentLevelNumber;
    [SerializeField] private TextMeshPro m_text;
    void Start(){
        m_currentLevelNumber = PlayerPrefs.GetInt("CurrentLevel",1);
        m_text.text = m_currentLevelNumber.ToString();
    }

}
