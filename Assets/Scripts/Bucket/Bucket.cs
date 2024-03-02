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
    [HideInInspector] public int m_bucketCapacity = 7; // Here 6 is the index till which we can go (as indexing starts from 0 in arrays)
    [SerializeField] private GameObject[] m_bucketBlocks = new GameObject[7]; // Here 7 is the length of the array or the capacity of the bucket
    [SerializeField] private GameObject[] m_blocks;

    // Start 

    private void Start() {
        for(int i = 0; i < 7; i++){
            m_bucketBlocks[i] = null;
        }
    }

    // Update function
    private void Update() {

        Debug.Log(m_bucketTop);

        CheckForDuplicatesAndUpdate();
        Reshuffle();
        CheckWinCondtion();

    }

    // Function used to add element to the bucket array
    public void AddBlockToBucket(GameObject _block){
        if(m_bucketTop <= m_bucketCapacity){

            // Change tag and layer of the blocks to make them different from the blocks on mainstage

            _block.transform.parent.tag = "NumberBlockBucket";
            _block.transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
            _block.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
            _block.layer = LayerMask.NameToLayer("Default");

            if(!SearchForDuplicates(_block)){
                m_bucketBlocks[m_bucketTop] = _block;
                m_bucketTop++;
            }


        }
        else{
            gameplayManager.GameOver();
        }

    }

    private bool SearchForDuplicates(GameObject _newBlock)
    {
        bool foundDuplicate = false;
        int newNumber = _newBlock.GetComponent<CubeNumber>().number;

        // Check if the new block is a duplicate of any existing block
        for (int i = 0; i < m_bucketTop; i++)
        {
            GameObject block = m_bucketBlocks[i];
            if (block == null) continue;

            int blockNumber = block.GetComponent<CubeNumber>().number;

            if (newNumber == blockNumber)
            {
                // Double the existing block
                Destroy(_newBlock.transform.parent.gameObject);
                DoubleTheBlock(block, blockNumber, i);
                foundDuplicate = true;
                break; // Exit loop as duplicate found and bucket has been modified
            }
        }

        return foundDuplicate;
    }

    private void CheckForDuplicatesAndUpdate()
    {
        bool duplicatesFound = true;
        while (duplicatesFound)
        {
            duplicatesFound = false;
            for (int i = 0; i < m_bucketTop; i++)
            {
                GameObject block = m_bucketBlocks[i];
                if (block == null) continue;

                int number = block.GetComponent<CubeNumber>().number;

                for (int j = i + 1; j < m_bucketTop; j++)
                {
                    GameObject otherBlock = m_bucketBlocks[j];
                    if (otherBlock == null) continue;

                    int otherNumber = otherBlock.GetComponent<CubeNumber>().number;

                    if (number == otherNumber)
                    {
                        // Double both blocks
                        Destroy(otherBlock.transform.parent.gameObject);
                        DoubleTheBlock(block, number, i);
                        duplicatesFound = true;
                    }
                }
            }
        }
    }



    // Double the block 
    private void DoubleTheBlock(GameObject _block, int _blockNumber, int _arrayPos)
    {
        int newBlockIndex = (int)(Mathf.Log((float) _blockNumber, 2.0f));
        Destroy(_block.transform.parent.gameObject);

        Debug.Log("Destroyed old");

        GameObject newBlock = (GameObject) Instantiate(m_blocks[newBlockIndex], _block.transform.parent.position, Quaternion.identity);
        Debug.Log("Added");
        newBlock.tag = "NumberBlockBucket";
        newBlock.transform.GetChild(0).transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
        newBlock.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
        newBlock.layer = LayerMask.NameToLayer("Default");

        m_bucketBlocks[_arrayPos] = newBlock.transform.GetChild(0).gameObject;


    }

    // Reshuffle the blocks to fill any gaps in the bucket

    private void Reshuffle()
    {
        int newIndex = 0;
        for (int i = 0; i <= m_bucketCapacity; i++)
        {
            if (m_bucketBlocks[i] != null)
            {
                if (i != newIndex)
                {
                    // Move the block to fill the gap
                    m_bucketBlocks[newIndex] = m_bucketBlocks[i];

                    // Move the block in the scene
                    Vector3 newPosition = m_bucketGrid.CellToWorld(new Vector3Int(newIndex, 0, 0));
                    m_bucketBlocks[i].transform.parent.position = newPosition;

                    m_bucketBlocks[i] = null;
                }
                newIndex++;
            }
        }
        m_bucketTop = newIndex;
    }


    // Check if the target block is in the bucket

    private void CheckWinCondtion(){

        for (int i = 0; i < m_bucketCapacity; i++){

            
            int currentBlockNumber = 0;

            if(m_bucketBlocks[i] != null)
                currentBlockNumber = m_bucketBlocks[i].GetComponent<CubeNumber>().number;
                
            if (m_bucketBlocks[i] != null && currentBlockNumber == gameplayManager.m_targetBlockNumber){
                gameplayManager.LevelComplete();
            }

        }

    }

}
