using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public int m_targetBlockNumber;
    // Level complete function

    public void LevelComplete(){
        Debug.Log("OM");
    }

    // GameOver function

    public void GameOver(){
        Debug.Log("SHIV");
    }

}
