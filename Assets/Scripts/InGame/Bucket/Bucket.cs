using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Bucket : MonoBehaviour
{
    [SerializeField] private Grid m_bucketGrid;
    [SerializeField] private GameplayManager gameplayManager;
    [HideInInspector] public int m_bucketTop = 0;
    [HideInInspector] public int m_bucketCapacity = 7; // Here 6 is the index till which we can go (as indexing starts from 0 in arrays)
    [SerializeField] private GameObject[] m_bucketBlocks = new GameObject[7]; // Here 7 is the length of the array or the capacity of the bucket
    //[SerializeField] private GameObject[] m_blocks;
    [SerializeField] private BlocksDataSO m_blocksData;
    private bool m_levelComplete = false;
    private bool m_isDoubling = false;

    // Start 

    private void Start() {
        for(int i = 0; i < 7; i++){
            m_bucketBlocks[i] = null;
        }
    }

    // Update function
    private void Update() {

        if(!m_levelComplete){
            
            if(!m_isDoubling)
                CheckForDuplicatesAndUpdate();
            
            Reshuffle();
            CheckWinCondtion();
        }


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
                _block.transform.parent.transform.DOJump(m_bucketGrid.CellToWorld(new Vector3Int(m_bucketTop-1,0,0)),1,1,1f).SetEase(Ease.OutSine);
                _block.transform.parent.transform.DOShakeRotation(0.8f,20,5,60,true,ShakeRandomnessMode.Harmonic).SetEase(Ease.OutSine);
            }

            

        }
        else{
            Invoke("GameOver",0.3f);
        }

    }

    private void GameOver(){
        gameplayManager.GameOver();
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
                //Destroy(_newBlock.transform.parent.gameObject);
                _newBlock.transform.DOJump(block.transform.position,1,1,1f).SetEase(Ease.OutSine);
                StartCoroutine(RemoveBlock(_newBlock,1.2f));
                StartCoroutine(DoubleTheBlock(block,blockNumber, i));
                foundDuplicate = true;
                break; // Exit loop as duplicate found and bucket has been modified
            }
        }

        return foundDuplicate;
    }

    private void CheckForDuplicatesAndUpdate()
    {
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
                    otherBlock.transform.DOJump(block.transform.position, 0.3f, 1, 0.4f).SetEase(Ease.OutSine);
                    StartCoroutine(DoubleAndRemoveBlocks(block, otherBlock, number, i, j));
                }
            }
        }
    }

    private IEnumerator DoubleAndRemoveBlocks(GameObject block, GameObject otherBlock, int number, int index1, int index2)
    {
        yield return new WaitForSeconds(0.2f); // Adjust delay as needed
        StartCoroutine(DoubleTheBlock(block, number, index1));
        StartCoroutine(RemoveBlock(otherBlock, 0.4f)); // No delay for immediate removal
    }



    // Double the block 
    private IEnumerator DoubleTheBlock(GameObject _block, int _blockNumber, int _arrayPos)
    {
        /*
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
        */
        m_isDoubling = true;
        yield return new WaitForSeconds(1f);
        m_isDoubling = false;


        _block.tag = "NumberBlockBucket";
        _block.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
        _block.transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
        _block.layer = LayerMask.NameToLayer("Default");

        _block.GetComponent<CubeNumber>().number = (2 * _blockNumber);
        _block.transform.GetChild(0).GetComponent<TextMeshPro>().text = (2 * _blockNumber).ToString();
        _block.GetComponent<Renderer>().material.color = m_blocksData.m_blockColors[(int)(Mathf.Log((float) _blockNumber, 2.0f))];


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
                    //m_bucketBlocks[i].transform.parent.position = newPosition;
                    m_bucketBlocks[i].transform.parent.transform.DOJump(newPosition,0.5f,1,0.5f);

                    m_bucketBlocks[i] = null;
                }
                newIndex++;
            }
        }
        m_bucketTop = newIndex;
    }


    // Check if the target block is in the bucket

    private void CheckWinCondtion(){

        for (int i = 0; i <= m_bucketCapacity; i++){

            
            int currentBlockNumber = 0;

            if(m_bucketBlocks[i] != null)
                currentBlockNumber = m_bucketBlocks[i].GetComponent<CubeNumber>().number;
                
            if (m_bucketBlocks[i] != null && currentBlockNumber == gameplayManager.m_targetBlockNumber && !m_levelComplete){
                
                m_levelComplete = true;
                GameplayManager.m_levelComplete = true;
                gameplayManager.LevelComplete();

            }

        }

    }

    // Function to remove a block

    private IEnumerator RemoveBlock(GameObject _newBlock, float _timeDelay){
        yield return new WaitForSeconds(_timeDelay);
        if(_newBlock != null)
            Destroy(_newBlock.transform.parent.gameObject);
    }

}
