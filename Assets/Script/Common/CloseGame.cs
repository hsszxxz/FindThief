using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseGame : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(rp);
    }
    private void rp()
    {
        Application.Quit();
    }
}
