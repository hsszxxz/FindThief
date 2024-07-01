using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
///<summary>
///
///<summary>
public class InitiateMap : MonoBehaviour
{
    private int mapW = 8;
    private int mapH = 8;
    public GameObject mapPre;
    public GameObject mapAssemble;
    public List<List<Transform>> squareAssemble;
    public List<Transform> baarierAssemble;
    private void Start()
    {
        squareAssemble = new List<List<Transform>>();
        CreatSquare();
    }
    void CreatSquare()
    {
        for (int i = 0; i < mapW; i++)
        {
            List<Transform> row = new List<Transform>();
            for (int j = 0; j < mapH; j++)
            {
                GameObject mapSquare = Instantiate(mapPre);
                mapSquare.transform.position = new Vector3(i - 3.5f, j - 3.5f, 0);
                mapSquare.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                mapSquare.transform.SetParent(mapAssemble.transform, true);
                mapSquare.name = i.ToSafeString() + "_" + j.ToSafeString();
                mapSquare.tag = "MapSquare";
                mapSquare.GetComponent<EventTrigger>().squareX = i;
                mapSquare.GetComponent<EventTrigger>().squareY = j;
                row.Add(mapSquare.transform);
            }
            squareAssemble.Add(row);
        }
    }
}

