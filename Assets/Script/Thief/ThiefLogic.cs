using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ThiefLogic : MonoBehaviour
{
    private GameObject scriptAssemble;
    private InitiateMap map;
    public GameObject thiefPre;
    private GameObject thief;
    [HideInInspector]
    public int currentX;
    [HideInInspector]
    public int currentY;
    bool[] canGo;
    private void Start()
    {
        scriptAssemble = GameObject.Find("ScriptAssemble");
        map = scriptAssemble.GetComponent<InitiateMap>();
        InitThief();
        canGo = new bool[4] { true,true,true,true};
    }
    public void InitThief()
    {
        int x = ProduceRandomNum(8);
        int y = ProduceRandomNum(8);
        currentX = x;
        currentY = y;
        thief = Instantiate(thiefPre);
        thief.transform.SetParent(map.squareAssemble[x][y]);
        thief.transform.localPosition = Vector3.zero;
    }
    public void ThiefMove()
    {
        bool isOk = true;
        bool isfinsh =true;
        for (int i = 0; i < canGo.Length; i++)
        {
           if (canGo[i])
            {
                isfinsh = false;
                break;
            }
        }
        if (isfinsh)
        {
            return;
        }
        switch (ProduceRandomNum(4))
        {
            case 0:
                if (currentX<7 && map.squareAssemble[currentX+1][currentY].childCount==0)
                {
                    currentX += 1;
                    break;
                }
                else
                {
                    isOk = false;
                    canGo[0] = false;
                }
                break;
            case 1:
                if (currentX > 0 && map.squareAssemble[currentX-1][currentY].childCount == 0)
                {
                    currentX -= 1;
                    break;
                }
                else
                {
                    isOk = false;
                    canGo[1] =false;
                }
                break;
            case 2:
                if (currentY < 7 && map.squareAssemble[currentX][currentY+1].childCount == 0)
                {
                   currentY += 1;
                    break;
                }
                else
                {
                    isOk = false;
                    canGo[2] =false;
                }
                break;
            case 3:
                if (currentY > 0 && map.squareAssemble[currentX][currentY-1].childCount == 0)
                {
                    currentY -= 1;
                    break;
                }
                else
                {
                    isOk = false;
                    canGo[3] =false;
                }
                break;
        }
        if (isOk)
        {
            thief.transform.SetParent(map.squareAssemble[currentX][currentY]);
            thief.transform.localPosition = Vector3.zero;
            for (int i = 0;i<canGo.Length; i++)
            {
                canGo[i]= true;
            }
        }
        else
        {
            ThiefMove();
        }
    }
    private int ProduceRandomNum(int max)
    {
        int result = Random.Range(0, max);
        return result;
    }
}
