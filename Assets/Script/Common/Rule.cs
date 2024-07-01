using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Rule : MonoBehaviour
{
    public GameObject text;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(rp);
    }
    private void rp()
    {
        text.SetActive(true);
    }
}

