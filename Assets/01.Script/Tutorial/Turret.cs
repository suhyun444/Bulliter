using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float direction;
    public float bullet_spped;
    public float bullet_cool;
    public float Bullet_Life_Time;
    public Transform FirePosition;
    public GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("shoot", 0, bullet_cool);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void shoot()
    {
        EnemyBullet tmp = Instantiate(Bullet, FirePosition.position, Quaternion.identity).GetComponent<EnemyBullet>();
        tmp.direction = direction;
        tmp.speed = bullet_spped;
        tmp.Penetrate = false;
        tmp.setDeadTime(Bullet_Life_Time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
