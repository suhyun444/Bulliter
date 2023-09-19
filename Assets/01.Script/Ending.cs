using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    bool can_skip = false;
    public GameObject skip_text;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("skip", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && can_skip)
        {
            SceneManager.LoadScene(0);

        }
    }
    void skip()
    {
        skip_text.SetActive(true);
        can_skip = true;
    }
}
