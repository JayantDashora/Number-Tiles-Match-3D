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
    public void BlockSelected(GameObject obj){
        CheckBoardStatus();
        MoveBlockToBucket(obj);
    }

    // Moves block to the bucket
    private void MoveBlockToBucket(GameObject _obj)
    {

        // Adds block to the bucket array
        m_bucket.AddBlockToBucket(_obj);


        // Tweens block from intial position to the bucket top position
        Vector3 destination = m_bucketGrid.CellToWorld(new Vector3Int(m_bucket.m_bucketTop-1,0,0));
        _obj.transform.parent.transform.position = destination;




    }

    // Checking if the board is empty or not

    private void CheckBoardStatus(){
        Debug.Log(GameObject.FindGameObjectsWithTag("NumberBlock").Length);
        if(GameObject.FindGameObjectsWithTag("NumberBlock").Length == 1){
            
            s_OnBoardEmpty?.Invoke();
        }
    }
}
