using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hart_PichUp : MonoBehaviour
{
    private float PosZ;

    private Vector3 angularVelocity;
    private Space space = Space.Self;
    private GameObject Game_Manger;
    private float Speed;
    void Start()
    {
        Game_Manger = GameObject.FindGameObjectWithTag("Game Manger");
        PosZ = transform.position.z;
        transform.position = new Vector3(transform.position.x, 0.82f, PosZ);
        transform.localScale = new Vector3(2, 2, 2);
        //angularVelocity = new Vector3(transform.rotation.x, 150, 0);
    }

    
    void Update()
    {
        Speed = Game_Manger.GetComponent<Game_Manger>().Player_Speed;
        PosZ -= Time.deltaTime * Speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, PosZ);
        if (PosZ <= -20)
        {
            Destroy(gameObject, 0.1f);
        }

        transform.Rotate(angularVelocity * Time.deltaTime, space);
    }
    

}
