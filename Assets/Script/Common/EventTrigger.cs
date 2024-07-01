using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public delegate void PointerEventHandler(PointerEventData eventData);
public class EventTrigger : MonoBehaviour
{
    private GameObject scriptAssemble;
    private InitiateMap map;
    private SpriteRenderer squareSpriteRenderer;
    private ThiefLogic thiefLogic;
    public Color selectColor;
    private Color currentColor;
    public Color invisibleColor;
    public GameObject canvasPre;
    private GameObject nowCanvas;
    private GameObject FinishCanvas;
    public GameObject barrierPre;
    private bool isShowNumber = false;
    [HideInInspector]
    public int squareX = 0;
    [HideInInspector]
    public int squareY = 0;
    private bool isEnter = false;
    [HideInInspector]
    public bool isBarrier =false;
    private void Start()
    {
        squareSpriteRenderer = GetComponent<SpriteRenderer>();
        currentColor = squareSpriteRenderer.color;
        scriptAssemble = GameObject.Find("ScriptAssemble");
        map = scriptAssemble.GetComponent<InitiateMap>();
        thiefLogic = scriptAssemble.GetComponent<ThiefLogic>();
        FinishCanvas = GameObject.Find("Canvas");
    }

    private IEnumerator DestroyCanvas()
    {
        yield return new WaitForSeconds(3);
        Destroy(nowCanvas);
        squareSpriteRenderer.color = currentColor;
        isShowNumber =false;
    }
    private int CountThiefSteps()
    {
        int xCount = Math.Abs(thiefLogic.currentX - squareX);
        int yCount = Math.Abs(thiefLogic.currentY - squareY);
        return xCount + yCount;
    }
    public event PointerEventHandler PointerDown;
    public event PointerEventHandler PointerUp;
    public event PointerEventHandler PointerClick;
    private Transform GetHitTransform(string tag)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null && hit.transform.CompareTag(tag))
        {
            return hit.transform;
        }
        else
        {
            return null;
        }
    }
    private void Update()
    {
        if (!isShowNumber)
        {
            if (GetHitTransform(this.tag) == this.transform)
            {
                squareSpriteRenderer.color = selectColor;
                if (Input.GetMouseButtonDown(1))
                {
                    GameObject barrier = Instantiate(barrierPre);
                    barrier.transform.SetParent(transform);
                    barrier.transform.localPosition = Vector3.zero;
                    if (map.baarierAssemble.Count >= 3) 
                    {
                        Transform extrolBarrier = map.baarierAssemble[0];
                        map.baarierAssemble.Remove(extrolBarrier);
                        Destroy(extrolBarrier.gameObject);
                    }
                    map.baarierAssemble.Add(barrier.transform);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    nowCanvas = Instantiate(canvasPre);
                    nowCanvas.transform.position = transform.position;
                    Text text = nowCanvas.GetComponentInChildren<Text>();
                    text.text = CountThiefSteps().ToString();
                    thiefLogic.ThiefMove();
                    squareSpriteRenderer.color = invisibleColor;
                    isShowNumber = true;
                    StartCoroutine(DestroyCanvas());
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        if (transform.GetChild(i).tag == "Thief")
                        {
                            FinishCanvas.GetComponent<CanvasGroup>().alpha = 1;
                            return;
                        }
                    }
                }
                if (!isEnter)
                {
                    isEnter = true;
                }
            }
            else if (isEnter)
            {
                squareSpriteRenderer.color = currentColor;
                isEnter = false;
            }
        }
    }
}

