using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shake_power;
    public float shake_length;
    // Start is called before the first frame update
    void Update()
    {
        shake_length -= Time.deltaTime;
        if(shake_length > 0)
        {
            transform.localPosition = new Vector3(Random.Range(-shake_power, shake_power), Random.Range(-shake_power, shake_power), 0);
        }
        else
        {
            transform.localPosition = Vector3.zero;
            shake_power = 0;
            shake_length = 0;
        }
    }

    // Update is called once per frame
    public void AddScreenShake(float power, float length)
    {
        if (shake_power <= power)
        {
            shake_power = power;
            shake_length = length;
        }
    }

    public void StopScreenShake()
    {
        shake_power = 0;
        shake_length = 0;
    }
}
