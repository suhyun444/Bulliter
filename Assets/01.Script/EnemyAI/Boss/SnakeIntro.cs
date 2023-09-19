using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeIntro : MonoBehaviour
{

    public void Scream()
    {
        ScreenShake ss = GameObject.Find("Player").GetComponentInChildren<ScreenShake>();
        ss.AddScreenShake(0.3f, 100f);
    }

    public void StopScream()
    {
        ScreenShake ss = GameObject.Find("Player").GetComponentInChildren<ScreenShake>();
        ss.StopScreenShake();
    }
}
