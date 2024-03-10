using UnityEngine;

public class BucketGrid : MonoBehaviour
{
    [SerializeField] private GameObject m_indicatorPrefab;
    [SerializeField] private Grid m_grid;

    private void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            Vector3Int cellPosition = new Vector3Int(i, 0, 0);
            Vector3 worldPosition = m_grid.CellToWorld(cellPosition) + new Vector3(0.5f, 0.1f, 0.5f);
            GameObject indicator = Instantiate(m_indicatorPrefab, worldPosition, Quaternion.Euler(0, 0, 0));
        }
    }
}
