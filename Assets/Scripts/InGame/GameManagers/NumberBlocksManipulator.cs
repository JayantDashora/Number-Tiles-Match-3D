using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NumberBlocksManipulator : MonoBehaviour
{
    public static event Action s_OnBoardEmpty;
    [SerializeField] private Bucket m_bucket;
    [SerializeField] private Grid m_bucketGrid;
    
    // Called when a block is selected
    public void BlockSelected(GameObject _obj){
        CheckBoardStatus();
        MoveBlockToBucket(_obj);
    }

    // Moves block to the bucket
    private void MoveBlockToBucket(GameObject _obj)
    {
        // Adds block to the bucket array
        m_bucket.AddBlockToBucket(_obj);
    }

    // Checking if the board is empty or not

    private void CheckBoardStatus(){
        Debug.Log(GameObject.FindGameObjectsWithTag("NumberBlock").Length);
        if(GameObject.FindGameObjectsWithTag("NumberBlock").Length == 1){
            
            s_OnBoardEmpty?.Invoke();
        }
    }
}
