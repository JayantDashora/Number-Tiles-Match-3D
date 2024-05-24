using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_text;
    void Start(){
        m_text.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex-1).ToString();
    }

}
