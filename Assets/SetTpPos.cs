using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTpPos : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().tp_point = transform;
        }
    }
}
