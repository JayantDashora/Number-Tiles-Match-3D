using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonsManager : MonoBehaviour
{
    [SerializeField] private TutorialScreenManager m_tutorialScreenManager;
    
    public void MessageBoxCloseButton(){
        m_tutorialScreenManager.m_messageBoxCloseButtonPressed = true;
    }

    public void TutorialBoxCloseButton(){
        m_tutorialScreenManager.m_tutorialBoxCloseButtonPressed = true;
    }

    public void InputInfoBoxCloseButton(){
        m_tutorialScreenManager.m_inputInfoBoxloseButtonPressed = true;
    }

}
