using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinScreenManager : MonoBehaviour
{
    [SerializeField] private RectTransform m_banner;
    [SerializeField] private RectTransform m_bannerText;
    [SerializeField] private RectTransform m_nextLevelButton;
    [SerializeField] private RectTransform m_homeButton;

    [SerializeField] private float m_intialDelay;

    private void Update() {
        IntroSequence();
    }

    private void IntroSequence(){

        // Banner intro sequence
        m_banner.DOAnchorPosY(400f,1f).SetEase(Ease.OutSine).SetUpdate(true);
        m_bannerText.DOAnchorPosY(0,1.15f).SetEase(Ease.OutSine).SetUpdate(true);

        // Buttons intro sequence

        m_nextLevelButton.DOAnchorPosX(0,1f).SetEase(Ease.OutSine).SetUpdate(true);
        m_homeButton.DOAnchorPosX(0,1f).SetEase(Ease.OutSine).SetUpdate(true);
        
    }
}
