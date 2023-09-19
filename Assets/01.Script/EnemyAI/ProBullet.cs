using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProBullet : MonoBehaviour
{
    GameObject Player;
    Rigidbody2D rigid;
    public GameObject Bullet;
    public float BulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
        setdir();
        Invoke("shoot", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void setdir()
    {
        Vector2 direction = new Vector2(Player.transform.position.x - transform.position.x,
                                        Player.transform.position.y + 15 - transform.position.y);
        transform.right = direction;
    }
    void shoot()
    {
        AudioMGR.instance.Play_Snowman_Attack();
        rigid.gravityScale = 1;
        rigid.velocity = transform.right * 10f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Solid")
        ||  collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AudioMGR.instance.Play_Snow_Break();
            int angle_offset = Random.Range(0, 30);
            for (int i = 0; i < 360; i += 30)
            {
                EnemyBullet bullet = Instantiate(Bullet, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
                bullet.speed = BulletSpeed;
                bullet.direction = i + angle_offset;
            }
            Destroy(gameObject);
        }
    }
    public void setOBJ(GameObject obj)
    {
        Bullet = obj;
    }
}
