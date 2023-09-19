using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rain : MonoBehaviour
{
    public GameObject Bullet;
    public float bullet_speed;
    float time = 0;
    float shoot_cool;
    float moveP;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Think", 0, 3);
        time = Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            EnemyBullet tmp = Instantiate(Bullet, transform.position, transform.rotation).GetComponent<EnemyBullet>();
            tmp.speed = bullet_speed;
            tmp.direction = -90;
            time = shoot_cool;
        }
        if(moveP <= 0 && transform.position.x >= -15)
        {
            transform.Translate(moveP, 0, 0);
        }
        else if(moveP >= 0 && transform.position.x <= 14)
        {
            transform.Translate(moveP, 0, 0);
        }
    }
    void Think()
    {
        shoot_cool = Random.Range(0.5f, 1.5f);
        moveP = Random.Range(-0.05f, 0.05f);
    }
}
