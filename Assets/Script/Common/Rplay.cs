using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Rplay : MonoBehaviour
{
    private CanvasGroup group;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(rp);
        group = GetComponentInParent<CanvasGroup>();
    }
    private void rp()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

