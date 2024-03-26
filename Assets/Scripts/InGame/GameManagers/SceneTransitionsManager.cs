using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionsManager : MonoBehaviour
{
    [SerializeField] private GameObject m_fadeIn;
    [SerializeField] private GameObject m_fadeOut;

    private IEnumerator Start() {
        // Fade In
        Time.timeScale = 0;
        m_fadeIn.SetActive(true);
        yield return new WaitForSecondsRealtime(1.9f);
        m_fadeIn.SetActive(false);
        Time.timeScale = 1;
    }

    public void FadeOut(){
        m_fadeOut.gameObject.SetActive(true);
    }

}
