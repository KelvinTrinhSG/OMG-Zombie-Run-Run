using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grawnd_Script : MonoBehaviour
{
    private GameObject Player;
    private float PosZ;

    public int Speed;
    public GameObject Grawend;
    public List<Vector3> Grass_Playes;
    public GameObject Grass;
    public bool Spawen_GrassB = false;
    public bool Its_Side;
    void Start()
    {
        Player = GameObject.Find("Player");
        PosZ = transform.position.z;

        if (Its_Side)
        {
            transform.localScale = new Vector3(100, 4, 30);
            transform.eulerAngles =  new Vector3(0, 90, 0);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        PosZ -= Time.deltaTime * Speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, PosZ); 
        if (PosZ <= - 55)
        {
            GameObject Grwd =  Instantiate(Grawend, new Vector3(transform.position.x, transform.position.y, 145), transform.rotation);
            Grwd.GetComponent<Grawnd_Script>().Spawen_GrassB = true;
            Destroy(gameObject);
        }
        

    }

    
}
