using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voodoo.Utils;
using UnityEngine.SceneManagement;

public class LoseScreenButtonsManager : MonoBehaviour
{

    [SerializeField] private GameObject m_fadeOut;
    public void HomeButton(){
        Vibrations.Haptic(HapticTypes.MediumImpact);
        Debug.Log("OM Home");
        m_fadeOut.SetActive(true);
        StartCoroutine(LoadHome());
    }

    private IEnumerator LoadHome(){
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }


    public void RetryButton(){
        Vibrations.Haptic(HapticTypes.MediumImpact);
        m_fadeOut.SetActive(true);
        StartCoroutine(LoadSameLevel());
    }

    private IEnumerator LoadSameLevel()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        Debug.Log("HARIOM");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
