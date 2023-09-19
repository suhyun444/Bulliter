using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class merchant : MonoBehaviour
{
    bool shop = false;
    bool onUI = false;
    public GameObject ShopUI;
    public GameObject HelpUI;
    Weapon weapon;
    Player player;
    bool caching = false;
    float[,] cost = new float[11,3];
    public Text[] cost_txt = new Text[3];
    // Start is called before the first frame update
    void Start()
    {
        ShopUI.SetActive(false);
        HelpUI.SetActive(false);
        weapon = GameObject.Find("Player").GetComponent<Weapon>();
        player = weapon.gameObject.GetComponent<Player>();
        cost[0,0] = 50;cost[0,1] = 10;cost[0,2] = 20;
        for(int i = 1; i < 11; i++)
        {
            for (int j = 0; j < 3; j++) cost[i, j] = cost[i - 1, j] += 10;
        }
        for(int i = 0; i < 3; i++)
        {
            if(i == 0)
            {
                if (player.HP_level < 2) cost_txt[i].text = cost[player.HP_level, 0].ToString() + "G";
                else cost_txt[i].text = "MAX";
            }
            if (i == 1)
            {
                if (weapon.dmg_level < 10) cost_txt[i].text = cost[weapon.dmg_level, 1].ToString() + "G";
                else cost_txt[i].text = "MAX";
            }
            if (i == 2)
            {
                if (weapon.speed_level < 10) cost_txt[i].text = cost[weapon.speed_level, 2].ToString() + "G";
                else cost_txt[i].text = "MAX";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shop)
        {
            if (Input.GetKeyDown(KeyCode.F) && !onUI)
            {
                AudioMGR.instance.Play_Shop_Oper();
                weapon.onShopping = true;
                ShopUI.SetActive(true);
                onUI = true;
            }
            else if(Input.GetKeyDown(KeyCode.F) && onUI)
            {
                AudioMGR.instance.Play_Shop_Close();
                weapon.onShopping = false;
                ShopUI.SetActive(false);
                onUI = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                AudioMGR.instance.Play_Shop_Close();
                weapon.onShopping = false;
                ShopUI.SetActive(false);
                onUI = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!caching)
            {
                caching = true;
            }
            shop = true;
            HelpUI.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onUI)
        {
            AudioMGR.instance.Play_Shop_Close();
        }
        shop = false;
        onUI = false;
        HelpUI.SetActive(false);
        ShopUI.SetActive(false);
        weapon.onShopping = false;
    }
    public void HPUP()
    {
        if (player.HP_level < 2)
        {
            int cur_cost = (int)cost[player.HP_level, 0];
            if (player.money >= cur_cost)
            {
                player.money -= cur_cost;
                AudioMGR.instance.Play_Shop_Buy();
                player.max_HPUP();
                cur_cost = (int)cost[player.HP_level, 0];
                cost_txt[0].text = cur_cost.ToString() + "G";
                if (player.HP_level == 2)
                {
                    cost_txt[0].text = "MAX";
                }
            }
        }
    }
    public void ATTACKUP()
    {
        if(weapon.dmg_level < 10)
        {
            int cur_cost = (int)cost[weapon.dmg_level, 1];
            if (player.money >= cur_cost)
            {
                player.money -= cur_cost;
                AudioMGR.instance.Play_Shop_Buy();
                weapon.damage += 1;
                weapon.dmg_level++;
                cur_cost = (int)cost[weapon.dmg_level, 1];
                cost_txt[1].text = cur_cost.ToString() + "G";
                if (weapon.dmg_level == 10)
                {
                    cost_txt[1].text = "MAX";
                }
            }
        }
    }
    public void ATTACKSPPEDUP()
    {
        if(weapon.speed_level < 10)
        {
            int cur_cost = (int)cost[weapon.speed_level, 2];
            if (player.money >= cur_cost)
            {
                player.money -= cur_cost;
                AudioMGR.instance.Play_Shop_Buy();
                weapon.bullet_cooldown -= 0.01f;
                weapon.speed_level++;
                cur_cost = (int)cost[weapon.speed_level, 2];
                cost_txt[2].text = cur_cost.ToString() + "G";
                if (weapon.speed_level == 10)
                {
                    cost_txt[2].text = "MAX";
                }
            }
        }
    }
}
