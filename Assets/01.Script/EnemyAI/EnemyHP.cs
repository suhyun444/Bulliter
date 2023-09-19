using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHP : MonoBehaviour
{
    public GameObject HealthUI;
    public float MAX_HP;
    public float CUR_HP;

    private Material mat_white;
    private Material def_mat;
    public SpriteRenderer enemy_spr;

    GameObject corpse_obj;
    public Sprite corpse_spr;
    public GameObject coin;
    public int coin_cost;
    bool bullet_time = false;
    bool dash_upgrade = false;
    public GameObject Black;
    public GameObject HELP_DASH_UI;
    public GameObject HELP_BULLT_TIME_UI;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Guard>() != null)
        {
            bullet_time = true;
        }
        if(GetComponent<Snake>() != null)
        {
            dash_upgrade = true;
        }
        CUR_HP = MAX_HP;
        mat_white = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        def_mat = enemy_spr.material;
        corpse_obj = Resources.Load("Corpse", typeof(GameObject)) as GameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            CUR_HP -= collision.GetComponentInParent<bullet>().atk;
            enemy_spr.material = mat_white;
            Invoke("MaterialToDef", 0.1f);
            Instantiate(Resources.Load("S_Explosion", typeof(GameObject)) as GameObject, collision.transform.position, Quaternion.identity);
        }
        if (CUR_HP <= 0)
        {
            if (bullet_time)
            {
                Weapon weapon = GameObject.Find("Player").GetComponent<Weapon>();
                weapon.bullet_time = true;
                GameObject[] objects = GameObject.FindGameObjectsWithTag("EnemyBullet");
                for (int i = 0; i < objects.Length; i++)
                    Destroy(objects[i]);
                GameObject[] boss_bullets = GameObject.FindGameObjectsWithTag("BossBullet");
                for (int i = 0; i < boss_bullets.Length; i++)
                    Destroy(boss_bullets[i]);
                AudioMGR.instance.Play_OWO_Dead();
                Instantiate(HELP_BULLT_TIME_UI, new Vector3(0,2,0), Quaternion.identity);
            }
            if (dash_upgrade)
            {
                Player player = GameObject.Find("Player").GetComponent<Player>();
                player.DashUpgrade();
                GameObject[] objects = GameObject.FindGameObjectsWithTag("EnemyBullet");
                for (int i = 0; i < objects.Length; i++)
                    Destroy(objects[i]);
                GameObject[] boss_bullets = GameObject.FindGameObjectsWithTag("BossBullet");
                for (int i = 0; i < boss_bullets.Length; i++)
                    Destroy(boss_bullets[i]);
                AudioMGR.instance.Play_Snake_Dead();
                Instantiate(HELP_DASH_UI, new Vector3(0, 2, 0), Quaternion.identity);
            }
            if (GetComponent<Rus>() != null)
            {
                AudioMGR.instance.Play_Slav_Dead();
                GameObject[] objects = GameObject.FindGameObjectsWithTag("EnemyBullet");
                for (int i = 0; i < objects.Length; i++)
                    Destroy(objects[i]);
                GameObject[] boss_bullets = GameObject.FindGameObjectsWithTag("BossBullet");
                for (int i = 0; i < boss_bullets.Length; i++)
                    Destroy(boss_bullets[i]);
                AudioMGR.instance.Play_Snake_Dead();
                Instantiate(GetComponent<Rus>().EndingDialog);
                Instantiate(GetComponent<Rus>().Ending_BGM);
                GameObject.Find("RandomMapGenerator").GetComponent<AudioSource>().Stop();


            }
            else if (GetComponent<Crocodile>() != null) AudioMGR.instance.Play_Crocodile_Dead();
            else if (GetComponent<Plant>() != null) AudioMGR.instance.Play_Plant_Dead();
            else if (GetComponent<Scorpion>() != null) AudioMGR.instance.Play_Scolpion_Dead();
            else if (GetComponent<Mummy>() != null) AudioMGR.instance.Play_Mummy_Dead();
            else if (GetComponent<Bear>() != null) AudioMGR.instance.Play_Bear_Dead();
            else if (GetComponent<Snowman>() != null) AudioMGR.instance.Play_Snowman_Dead();
            else if (gameObject.name == "target") { SceneManager.LoadScene(3); Black.SetActive(true);}
            if(coin != null)
            {
                int size = Random.Range(4, 7);
                for (int i = 0; i < size; i++)
                {
                    Coin tmp = Instantiate(coin, transform.position, Quaternion.identity).GetComponent<Coin>();
                    tmp.cost = coin_cost;
                }
            }
            //this.gameObject.SetActive(false);
            Destroy(gameObject);
            if (corpse_spr != null)
            {
                GameObject t_corpse = Instantiate(corpse_obj, transform.position, transform.rotation);
                t_corpse.transform.localScale = transform.localScale;
                t_corpse.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = corpse_spr;
            }
        }
        float HP_Per = CUR_HP / MAX_HP;
        Debug.Log(MAX_HP + " + " + CUR_HP);
        Debug.Log(HP_Per);
        HealthUI.transform.localScale = new Vector3(HP_Per, HealthUI.transform.localScale.y, 1);
    }

    void MaterialToDef()
    {
        enemy_spr.material = def_mat;
    }
}
