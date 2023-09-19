using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    Rigidbody2D rigid;
    public GameObject Bullet;
    public float BulletSpeed;
    public int dir;
    public int turn_amount;
    public float turn_speed;
    public float bullet_spawn_speed;
    public float Boom_speed;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        setdir();
        shoot();
        Invoke("Boom", Boom_speed);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Boom()
    {
        StartCoroutine("BoomBullet");
    }
    IEnumerator BoomBullet()
    {
        int cnt = turn_amount;
        float angle = 0;
        rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        float angle_offset = Random.Range(0, 360);
        AudioMGR.instance.Play_Slav_Attack3();
        while (cnt >= 0)
        {
            for (int i = 0; i < 360; i += 30)
            {
                EnemyBullet bullet = Instantiate(Bullet, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
                bullet.speed = BulletSpeed;
                bullet.direction = i + angle + angle_offset;
            }
            angle += turn_speed;
            yield return new WaitForSeconds(bullet_spawn_speed);
            cnt--;
            
        }
        Destroy(gameObject);
    }
    void setdir()
    {
        if(dir == 1)
        {
            Vector2 direction = new Vector2(-18, 20);
            transform.right = direction;
        }
        else if(dir == 2)
        {
            Vector2 direction = new Vector2(22, 25);
            transform.right = direction;
        }
    }
    void shoot()
    {
        rigid.velocity = transform.right * 10f;
    }
    public void setOBJ(GameObject obj)
    {
        Bullet = obj;
    }
}
