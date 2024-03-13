using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave_Yard : MonoBehaviour
{
    private float Spawen_Obj_Time = 1f;
    public List<GameObject> Spawen_Obj;
    public bool Left;
    public bool Right;
    private GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Left)
        {
            if (Player.transform.position.x >= 40)
            {
                Spawen_Obj_Time -= Time.deltaTime;
                if (Spawen_Obj_Time <= 0)
                {
                    var Sp_Ob_Pl = new Vector3(Random.Range(transform.position.x + transform.localScale.x / 2, transform.position.x - transform.localScale.x / 2), 0, Random.Range(transform.position.z + transform.localScale.z / 2, transform.position.z + transform.localScale.z / 2));

                    Instantiate(Spawen_Obj[Random.Range(0, Spawen_Obj.Count)], Sp_Ob_Pl, Quaternion.identity);
                    Spawen_Obj_Time = 1f;
                }
            }
        }
        if (Right)
        {
            if (Player.transform.position.x <= -40)
            {
                Spawen_Obj_Time -= Time.deltaTime;
                if (Spawen_Obj_Time <= 0)
                {
                    var Sp_Ob_Pl = new Vector3(Random.Range(transform.position.x + transform.localScale.x / 2, transform.position.x - transform.localScale.x / 2), 0, Random.Range(transform.position.z + transform.localScale.z / 2, transform.position.z + transform.localScale.z / 2));

                    Instantiate(Spawen_Obj[Random.Range(0, Spawen_Obj.Count)], Sp_Ob_Pl, Quaternion.identity);
                    Spawen_Obj_Time = 1f;
                }
            }
        }
        

    }
}
