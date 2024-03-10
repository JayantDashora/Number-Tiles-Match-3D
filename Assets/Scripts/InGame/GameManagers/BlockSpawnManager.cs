using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BlockSpawnManager : MonoBehaviour
{
    // Creating the data structure for the level using array of arrays
    // Each subsection will be spawned only when the all the blocks of the previous subsection are removed from the mainstage

    [Serializable]
    struct Section
    {
        public int[] m_sectionBlocks;
    }

    [SerializeField] private GameObject m_numberBlock;
    [SerializeField] private Section[] m_level;
    [SerializeField] private Grid m_mainStageGrid;
    [SerializeField] private LayerMask m_blockLayerMask; // Layer mask for blocks
    [SerializeField] private GameplayManager m_gameplayManager;
    private int m_currentSectionIndex = 0;

    private void Start()
    {
        NumberBlocksManipulator.s_OnBoardEmpty += SpawnSection;
        SpawnSection();
    }

    // Spawn the specified section of the level
    public void SpawnSection()
    {

        // Check whether is level is complete or not

        if(m_currentSectionIndex >= m_level.Length){
            m_gameplayManager.LevelComplete();
            return;
        }


        int[] currentSection = m_level[m_currentSectionIndex].m_sectionBlocks;

        Vector3Int spawnPoint = new Vector3Int(0, 0, 0);
        Vector3Int rayOffset = new Vector3Int(0, 5, 0);

        for (int i = 0; i < currentSection.Length; i++)
        {
            // Find an empty spot to spawn the block
            bool spotFound = false;
            while (!spotFound)
            {
                // Get a random spawn point
                spawnPoint = new Vector3Int(Random.Range(-3, 3), 0, Random.Range(-3, 3));

                // Check if the spawn point is empty
                if (!IsOccupied(spawnPoint))
                {
                    
                    // Spawn the block on an empty spot
                    GameObject numberBlock = Instantiate(m_numberBlock, m_mainStageGrid.CellToWorld(spawnPoint), Quaternion.identity);
                    numberBlock.gameObject.transform.GetChild(0).GetComponent<CubeNumber>().number = currentSection[i];
                    numberBlock.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro>().text = currentSection[i].ToString();
                    spotFound = true;
                }
            }
        }

        m_currentSectionIndex++;
    }

    // Check if a cell is occupied by any object on the specified layer
    private bool IsOccupied(Vector3Int _cellPosition)
    {
        Vector3 worldPosition = m_mainStageGrid.CellToWorld(_cellPosition);

        // Cast a ray downwards to check for any objects on the specified layer
        RaycastHit hit;

        if (Physics.Raycast(worldPosition + Vector3.up * 50, Vector3.down, out hit, Mathf.Infinity, m_blockLayerMask))
        {
            return true;
        }

        return false; // Cell is not occupied
    }
}