using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voodoo.Utils;

public class GameplayManager : MonoBehaviour
{
    public int m_targetBlockNumber;
    public static bool m_levelComplete;
    [SerializeField] private ParticleSystem m_confetti;
    [SerializeField] private GameObject m_winGameScreen;
    [SerializeField] private GameObject m_loseGameScreen;

    private void Start() {
    }

    // Level complete function

    public void LevelComplete()
    {
        m_confetti.Play();
        Vibrations.Haptic(HapticTypes.Success);
        Invoke("WinGame",4f);
        Debug.Log("OM");
    }

    private void WinGame()
    {
        m_winGameScreen.SetActive(true);
        Time.timeScale = 0;
    }

    // GameOver function

    public void GameOver(){
        m_loseGameScreen.SetActive(true);
        Vibrations.Haptic(HapticTypes.Failure);
        Time.timeScale = 0;
        Debug.Log("SHIV");
    }


}
