using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bucket : MonoBehaviour
{
    [SerializeField] private Grid m_bucketGrid;
    [SerializeField] private GameplayManager gameplayManager;
    [HideInInspector] public int m_bucketTop = 0;
    [HideInInspector] public int m_bucketCapacity = 6; // Here 6 is the index till which we can go (as indexing starts from 0 in arrays)
    private GameObject[] m_bucketBlocks = new GameObject[7]; // Here 7 is the length of the array or the capacity of the bucket

    // Function used to add element to the bucket array
    public void AddBlockToBucket(GameObject block){
        if(m_bucketTop <= m_bucketCapacity){

            // Change tag and layer of the blocks to make them different from the blocks on mainstage

            block.transform.parent.tag = "NumberBlockBucket";
            block.transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
            block.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
            block.layer = LayerMask.NameToLayer("Default");

            if(!SearchForDuplicates(block)){
                m_bucketBlocks[m_bucketTop] = block;
                m_bucketTop++;
            }


        }
        else{
            gameplayManager.GameOver();
        }

    }

    // Search for duplicates in the bucket

    private bool SearchForDuplicates(GameObject block)
    {
        int currentNumber = block.GetComponent<CubeNumber>().number;

        for(int i = 0; i < m_bucketTop; i++){
            if(m_bucketBlocks[i] != null){
                int blockNumber = m_bucketBlocks[i].GetComponent<CubeNumber>().number;
                if(currentNumber == blockNumber){
                    // Double the block
                    Destroy(block);
                }
            }
        }

        return false;
    }

}
