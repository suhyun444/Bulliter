using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public GameObject load_obj;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Click_Start()
    {
        load_obj.SetActive(true);
        SceneManager.LoadScene(3);
    }
    public void Click_tutorial()
    {
        load_obj.SetActive(true);
        SceneManager.LoadScene(1);
    }
    public void Click_Escape()
    {
        Application.Quit();
    }
}
