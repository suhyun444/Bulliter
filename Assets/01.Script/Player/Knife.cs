using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    Player player;
    Weapon weapon;
    public GameObject Bullet;
    public float bullet_time_scale;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInParent<Weapon>();
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == bullet_time_scale)
        {
            if (!Input.GetMouseButton(1)) setTime();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            AudioMGR.instance.Play_Player_Knife();
            player.bonusTime = true;
            Invoke("setbonus", 0.2f);
            Destroy(collision.gameObject);
            player.GetComponentInChildren<ScreenShake>().AddScreenShake(0.3f, 0.06f);
            bullet tmp = Instantiate(Bullet, transform.position, Quaternion.identity).GetComponent<bullet>();
            tmp.setdir(weapon.angle + Random.Range(-15, 15));
            tmp.setatk(10);
            if (weapon.bullet_time)
            {
                Time.timeScale = bullet_time_scale;
                Invoke("setTime", bullet_time_scale * 3);
            }
        }
    }
    void setbonus()
    {
        player.bonusTime = false;
    }
    void setTime()
    {
        Time.timeScale = 1f;
    }
}
