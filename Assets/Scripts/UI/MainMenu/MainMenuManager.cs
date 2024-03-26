using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform m_headingText;
    [SerializeField] private RectTransform m_playButton;
    private bool m_canStart = false;

    private void Start() {
        Invoke("CanStart",0.5f);
    }

    private void Update() {
        if(m_canStart)
            IntroSequence();
    }

    private void IntroSequence(){

        m_headingText.DOAnchorPosY(-350f,1.5f).SetEase(Ease.OutSine).SetUpdate(true);
        m_playButton.DOAnchorPosY(200f,1f).SetEase(Ease.OutSine).SetUpdate(true);
        
    }

    private void CanStart(){
        m_canStart = true;
    }
    
}
