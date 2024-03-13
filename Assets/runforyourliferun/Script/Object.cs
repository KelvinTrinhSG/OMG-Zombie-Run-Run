using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private GameObject Player;
    private float PosZ;
    private GameObject Game_Manger;
    private float Speed;

    public bool Is_grass;
    public bool Is_Decoration;
    public bool Is_Dirt;
    
    void Start()
    {
        Game_Manger = GameObject.FindGameObjectWithTag("Game Manger");
        if (Is_Dirt)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        if (Is_Decoration)
        {
            transform.localScale = new Vector3(Random.Range(3, 5), Random.Range(3, 5), Random.Range(3, 5));
        }
        if (Is_grass)
        {
            transform.localScale = new Vector3(Random.Range(2, 3), Random.Range(2, 3), Random.Range(2, 3));
        }
        if (!Is_grass && !Is_Dirt && ! Is_Decoration)
        {
            transform.localScale = new Vector3(Random.Range(5, 9), Random.Range(5, 9), Random.Range(5, 9));
            transform.localRotation = new Quaternion(transform.localRotation.x, Random.Range(-90, 90), transform.localRotation.z, transform.localRotation.w);
        }
        Player = GameObject.Find("Player");
        PosZ = transform.position.z;
       
    }

    // Update is called once per frame
    void Update()
    {
        Speed = Game_Manger.GetComponent<Game_Manger>().Player_Speed;
        PosZ -= Time.deltaTime * Speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, PosZ);
        if (PosZ<=-20)
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
