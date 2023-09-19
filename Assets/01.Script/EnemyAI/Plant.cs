using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    bool Plant_Left = true;
    GameObject Player;
    public GameObject bullet;
    public Transform FirePosition;
    public float bullet_speed;
    //public Transform head;
    public GameObject head;
    float time = 0;
    public float Shoot_cool = 3;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        Plant_Left = false;
        Player = GameObject.Find("Player");
        time = Random.Range(0, Shoot_cool);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        time -= Time.deltaTime;
        if(time <= 0 && distance < 12)
        {
            AudioMGR.instance.Play_Plant_Attack();
            EnemyBullet eb = Instantiate(bullet, FirePosition.position, Quaternion.identity).GetComponent<EnemyBullet>();
            eb.speed = bullet_speed;
            eb.chase_player = true;
            time = Shoot_cool;
        }
        float angle = Mathf.Atan2(Player.transform.position.y - head.transform.position.y, Player.transform.position.x - head.transform.position.x) * Mathf.Rad2Deg;
        head.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (this.transform.position.x > Player.transform.position.x && !Plant_Left)
        {
            head.transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, 1);
            Plant_Left = true;

        }
        else if (this.transform.position.x <= Player.transform.position.x && Plant_Left)
        {
            head.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
            Plant_Left = false;
        }
    }
}
