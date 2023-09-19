using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private void Start()
    {
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
