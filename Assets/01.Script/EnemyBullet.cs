using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    GameObject Player;
    public bool chase_player;
    public float direction;
    //float time = 0;
    public float speed;
    public bool Penetrate = false;
    float deadtime = 8;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        if (chase_player)
        {
            float angle = Mathf.Atan2(Player.transform.position.y - this.transform.position.y, Player.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(direction, Vector3.forward);
        }

        Destroy(gameObject, deadtime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
        //time += Time.deltaTime;
        //if (time >= 3f)
        //{
        //    this.gameObject.SetActive(false);

        //}
    }
    public void chase()
    {
        if (chase_player)
        {
            float angle = Mathf.Atan2(Player.transform.position.y - this.transform.position.y, Player.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            direction = angle;
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(direction, Vector3.forward);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Solid") && !Penetrate){
            Destroy(gameObject);
            Instantiate(Resources.Load("Dust", typeof(GameObject)) as GameObject, transform.position, Quaternion.identity);//.transform.rotation = transform.rotation;
        }
        else if (collision.CompareTag("Block"))
        {
            Destroy(gameObject);
            Instantiate(Resources.Load("Dust", typeof(GameObject)) as GameObject, transform.position, Quaternion.identity);//.transform.rotation = transform.rotation;
        }
    }
    public void setDeadTime(float time)
    {
        deadtime = time;
    }
}