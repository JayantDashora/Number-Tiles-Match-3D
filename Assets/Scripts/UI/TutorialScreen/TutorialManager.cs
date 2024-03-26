using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject m_tutorialScreenCanvas;

    private void Start() {
        Invoke("PauseGame",0.05f);
        StartCoroutine(StartTutorialIntro());
    }

    private IEnumerator StartTutorialIntro(){
        yield return new WaitForSecondsRealtime(1f);
        m_tutorialScreenCanvas.SetActive(true);
    }

    private void PauseGame(){
        Time.timeScale = 0;
    }

}
