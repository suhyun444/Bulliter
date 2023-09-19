using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public GameObject[] bg;

    private void Start()
    {
    }
    public void ChangeBackground(int id)
    {
        for (int i = 0; i < bg.Length; i++)
        {
            if (i == id) bg[i].SetActive(true);
            else bg[i].SetActive(false);
        }
    }
}
