using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBlockSub : MonoBehaviour
{
    public static event Action s_OnBoardEmpty1;
    public void Selected(){
        CheckBoardStatus();
        Destroy(gameObject.transform.parent.gameObject);
        
    }

    // Checking if the board is empty or not

    private void CheckBoardStatus(){
        Debug.Log(GameObject.FindGameObjectsWithTag("NumberBlock").Length);
        if(GameObject.FindGameObjectsWithTag("NumberBlock").Length == 1){
            
            s_OnBoardEmpty1?.Invoke();
        }
    }
}
