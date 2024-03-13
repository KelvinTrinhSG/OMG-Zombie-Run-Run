using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]public float bulletSpeed = 10;    
    [HideInInspector]public int Power;

    public GameObject BloodPArticl;
    public GameObject OtherPArticl;

    private Rigidbody bullet;
    void Start()
    {
        bullet = gameObject.GetComponent<Rigidbody>();
    }

   
    void Update()
    {
        
        bullet.velocity = transform.forward * bulletSpeed;
        Destroy(gameObject, 1.5f);
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Zombie")
        {

            Instantiate(OtherPArticl, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        

        
    }
    
}
