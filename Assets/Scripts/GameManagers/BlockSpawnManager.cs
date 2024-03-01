using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockSpawnManager : MonoBehaviour
{
    // Creating the data structure for the level using array of arrays
    // Each subsection will be spawned only when the all the blocks of the previous subsection are removed from the mainstage

    [Serializable]
    struct Section
    {
        public GameObject[] m_sectionBlocks;
    }

    [SerializeField] private Section[] m_level;
    [SerializeField] private Grid m_mainStageGrid;
    [SerializeField] private LayerMask m_blockLayerMask; // Layer mask for blocks
    [SerializeField] private GameplayManager gameplayManager;
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
            gameplayManager.LevelComplete();
            return;
        }


        GameObject[] currentSection = m_level[m_currentSectionIndex].m_sectionBlocks;

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
                    Instantiate(currentSection[i], m_mainStageGrid.CellToWorld(spawnPoint), Quaternion.identity);
                    spotFound = true;
                }
            }
        }

        m_currentSectionIndex++;
    }

    // Check if a cell is occupied by any object on the specified layer
    private bool IsOccupied(Vector3Int cellPosition)
    {
        Vector3 worldPosition = m_mainStageGrid.CellToWorld(cellPosition);

        // Cast a ray downwards to check for any objects on the specified layer
        RaycastHit hit;
        if (Physics.Raycast(worldPosition + Vector3.up * 5, Vector3.down, out hit, Mathf.Infinity, m_blockLayerMask))
        {
            // If ray hits something, check if it's on the specified layer
            return true;
        }

        return false; // Cell is not occupied
    }
}
