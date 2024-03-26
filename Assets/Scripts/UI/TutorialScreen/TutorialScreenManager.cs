using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TutorialScreenManager : MonoBehaviour
{
    [SerializeField] private RectTransform m_messageBox;
    [SerializeField] private RectTransform m_tutorialBox;
    [SerializeField] private RectTransform m_inputInfoBox;
    [SerializeField] private GameObject m_tutorialCanvas;
    [SerializeField] private float m_intialDelay;
    public bool m_messageBoxCloseButtonPressed = false;
    public bool m_tutorialBoxCloseButtonPressed = false;
    public bool m_inputInfoBoxloseButtonPressed = false;

    private void Update() {
        MessageBoxIntroSequence();
        if(m_messageBoxCloseButtonPressed){
           MessageBoxOutroSequence();
        }
        if(m_tutorialBoxCloseButtonPressed){
           TutorialBoxOutroSequence();
        }
        if(m_inputInfoBoxloseButtonPressed){
           InputInfoBoxOutroSequence();
        }
    }

    private void MessageBoxIntroSequence(){
        m_messageBox.DOAnchorPosX(0,1f).SetEase(Ease.OutSine).SetUpdate(true);
    }

    public void MessageBoxOutroSequence(){
        m_messageBox.DOAnchorPosX(-1600,1f).SetEase(Ease.OutSine).SetUpdate(true).onComplete = TutorialBoxIntroSequence;
    }

    private void TutorialBoxIntroSequence(){
        m_tutorialBox.DOAnchorPosX(0,1f).SetEase(Ease.OutSine).SetUpdate(true);
    }

    public void TutorialBoxOutroSequence(){
        m_tutorialBox.DOAnchorPosX(-1600,1f).SetEase(Ease.OutSine).SetUpdate(true).onComplete = InputInfoBoxIntroSequence;
    }

    private void InputInfoBoxIntroSequence(){
        m_inputInfoBox.DOAnchorPosX(0,1f).SetEase(Ease.OutSine).SetUpdate(true);
    }

    public void InputInfoBoxOutroSequence(){
        m_inputInfoBox.DOAnchorPosX(-1600,1f).SetEase(Ease.OutSine).SetUpdate(true).onComplete = StopTutorial;
    }

    private void StopTutorial(){
        Time.timeScale = 1;
        m_tutorialCanvas.SetActive(false);
    }
}
