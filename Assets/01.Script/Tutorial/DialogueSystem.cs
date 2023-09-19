using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DialogueSystem : MonoBehaviour
{
    public float text_speed;
    public int curr_text_id;
    public Text name_ui;
    public Text text_ui;
    public GameObject boss_pic;

    [System.Serializable]
    public class dialogue_data
    {
        public string name;
        [TextArea(3, 10)]
        public string text;
        public int id;
    }
    public dialogue_data[] dialogue;
    int curr_char_id;
    void Start()
    {
        curr_text_id--;
        NextText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) NextText();
    }

    void NextText()
    {
        StopCoroutine("DrawText");
        curr_char_id = 0;
        
        text_ui.text = "";
        curr_text_id++;
        if (curr_text_id == dialogue.Length) Destroy(gameObject);
        curr_text_id = Mathf.Clamp(curr_text_id, 0, dialogue.Length - 1);

        if (dialogue[curr_text_id].id == 1)
        {
            boss_pic.SetActive(true);
        }
        if (dialogue[curr_text_id].id == 2)
        {
            boss_pic.SetActive(false);
        }
        if (dialogue[curr_text_id].id == 3)
        {
            SceneManager.LoadScene(2);
        }

        name_ui.text = dialogue[curr_text_id].name;
        StartCoroutine("DrawText");
    }

    IEnumerator DrawText()
    {
        foreach (char c in dialogue[curr_text_id].text.ToCharArray())
        {
            text_ui.text += c;
            yield return new WaitForSeconds(text_speed);
        }
    }
}
