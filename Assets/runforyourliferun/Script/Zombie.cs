using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private GameObject Player;
    private float PosZ;
    private float PosX;
    private float Delay = 2f;    
    private int Walk;
    private Animator Anim;    
    private GameObject Game_Manger;
    private float Speed;

    public GameObject Dirt;

    [HideInInspector] public int Health;
    
    void Start()
    {
        Game_Manger = GameObject.FindGameObjectWithTag("Game Manger");
        Anim = gameObject.GetComponent<Animator>();
        Walk = Random.Range(0, 2);        
        Player = GameObject.FindGameObjectWithTag("Player");
        PosZ = transform.position.z;
        Instantiate(Dirt, new Vector3 (transform.position.x,0,transform.position.z), Quaternion.Euler(-90, 0, 180));
        Anim.SetInteger("Walk", Walk);
    }

    // Update is called once per frame
    void Update()
    {
        Speed = Game_Manger.GetComponent<Game_Manger>().Player_Speed;
        Delay -= Time.deltaTime;
        PosZ -= Time.deltaTime * Speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, PosZ);
        if (PosZ <= -20)
        {
            Destroy(gameObject, 0.1f);
        }
        
        if (Delay <= 0)
        {
            MovToPlayer();
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            Delay = 0;
        }
        if (Health <= 0)
        {
            if (Delay == 0)
            {
                Game_Manger.GetComponent<Game_Manger>().Add_Dead_Zombie();
                Anim.SetBool("Dead", true);
                GameObject.FindObjectOfType<AudioMManger>().Playe("Zombie Dead");
            }
            Delay ++;

        }
        else
        {
            transform.LookAt(Player.transform);
        }
    }
    public void FixedUpdate()
    {
       
    }
    public void MovToPlayer ()
    {
        
        Vector3 Direction = Player.transform.position - transform.position;
        transform.Translate(Direction.normalized * 1.3f * Time.deltaTime, Space.World);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (Health > 0)
        {
            if (other.gameObject.tag == "Player")
            {

                Anim.SetBool("Attack", true);
                Game_Manger.GetComponent<Game_Manger>().Heart_P();
                Game_Manger.GetComponent<Game_Manger>().harts--;
                other.gameObject.GetComponent<CharacterController>().enabled = false;
                other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                other.GetComponent<Player>().It_Heart = true;
                GameObject.FindObjectOfType<AudioMManger>().Playe("Zombie Attack");
            }
            if (other.gameObject.tag == "Bullet")
            {

                Instantiate(other.gameObject.GetComponent<Bullet>().BloodPArticl,
                    other.transform.position, other.transform.rotation);                
                Health = Health - other.GetComponent<Bullet>().Power;
                Destroy(other.gameObject);
                GameObject.FindObjectOfType<AudioMManger>().Playe("Zombie Hurt");

            }
        }
    }
    
}
