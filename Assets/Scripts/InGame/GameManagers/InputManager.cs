using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using Voodoo.Controls;

public class InputManager : MonoBehaviour,ITapController
{
    [SerializeField] private LayerMask m_layerMask; // Layer mask to check for objects
    [SerializeField] private NumberBlocksManipulator numberBlocksManipulator;
    [SerializeField] private Bucket m_bucket;
    private GameObject[] m_numberBlocks;




    public void OnTap(Vector3 _Pos)
    {
        if(GameplayManager.m_levelComplete || !m_bucket.m_canSelectBlocks)
            return;

        // Cast a ray from the tapped position
        Ray ray = Camera.main.ScreenPointToRay(_Pos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_layerMask))
        {
            // Log information about the hit object
            //Debug.Log("Tapped on object: " + hit.collider.gameObject.name);
            //hit.collider.gameObject.GetComponent<NumberBlock>().Selected();
            numberBlocksManipulator.BlockSelected(hit.collider.gameObject);
        }
        else
        {
            Debug.Log("Tapped on empty space or object not on the layer mask.");
        }
    }


}
