using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeManager : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    private int TotalNum => rows * columns;
    [SerializeField] List<GameObject> chimes = new List<GameObject>();

    private void Awake()
    {
        AssignIDsToChildren();
        SetChlidrenNeighbor();
    }

    void AssignIDsToChildren()//子物体的钟会从左往右，从上往下排列（不能打乱，为了后面的获取算法）
    {
        foreach(Transform child in transform)
        {
            chimes.Add(child.gameObject);
        }

        for(int i = 0; i < chimes.Count; i++)
        {
            GameObject child = chimes[i];
            Chime chime = child.GetComponent<Chime>();

            if (chime != null)
            {
                chime.id = i + 1;
            }
        }
    }

    void SetChlidrenNeighbor()
    {
        foreach(GameObject child in chimes)
        {
            Chime chime = child.GetComponent<Chime>();
            int id = chime.id;
            chime.Parent = gameObject;
            // 将一维ID转换为二维坐标
            int row = (id - 1) / columns;
            int col = (id - 1) % columns;

            if (col > 0)
                chime.neighbors.Add(chimes[(row * columns) + col - 1].GetComponent<Chime>());

            if (col < columns - 1)
                chime.neighbors.Add(chimes[(row * columns) + col + 1].GetComponent<Chime>());

            if (row > 0)
                chime.neighbors.Add(chimes[((row - 1) * columns) + col].GetComponent<Chime>());

            if (row < rows - 1)
                chime.neighbors.Add(chimes[((row + 1) * columns) + col].GetComponent<Chime>());
        }
    }

    public bool CheckFinish()
    {
        int activeCount = 0;
        int total = chimes.Count;

        foreach (GameObject chimeObject in chimes)
        {
            Chime chime = chimeObject.GetComponent<Chime>();

            if (chime == null) continue;

            if (chime.isSyntony)
            {
                activeCount++;
            }
            else
            {
                return false;
            }
        }

        Debug.Log("结束力");
        return activeCount == total;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var chime in chimes)
        {
            Vector3 pos = chime.transform.position;
            Gizmos.DrawWireSphere(pos, 0.2f);
            foreach (var neighbor in chime.GetComponent<Chime>().neighbors)
            {
                Gizmos.DrawLine(pos, neighbor.transform.position);
            }
        }
    }

}
