using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Voodoo.Utils;

public class MainMenuButtonsManager : MonoBehaviour
{
    [SerializeField] private GameObject m_fadeOut;
    public void PlayButton()
    {
        Vibrations.Haptic(HapticTypes.MediumImpact);
        m_fadeOut.SetActive(true);
        StartCoroutine(PlayGame());
    }

    private IEnumerator PlayGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        Debug.Log("HARIOM");
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel",2));
    }
}
