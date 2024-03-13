using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepen : MonoBehaviour
{
    
    public List <GameObject> Wepens;    

    private GameObject Currents_Wepen;
    private float PosZ;   
    private Vector3 angularVelocity;
    private Space space = Space.Self;
    private GameObject Game_Manger;
    private float Speed;

    [HideInInspector] public int Current_Wepen_Number;
    [HideInInspector] public List<GameObject> Childern;
    [HideInInspector] public List<Material> matrerls;
    void Start()
    {
        Game_Manger = GameObject.FindGameObjectWithTag("Game Manger");
        Current_Wepen_Number = Random.Range(0, Wepens.Count);
        Currents_Wepen = Wepens[Current_Wepen_Number];
        Currents_Wepen.SetActive(true);
        PosZ = transform.position.z;
        transform.position = new Vector3(transform.position.x,0.7f,PosZ);
        transform.localScale = new Vector3(20, 20, 20);
        angularVelocity = new Vector3(0, 150, 0);
        foreach (Transform child in Currents_Wepen.transform)
        {
            Childern.Add(child.gameObject);
        }
        for (var i = 0; i < Childern.Count; i++)
        {
            foreach (Material Mat in Childern[i].GetComponent<MeshRenderer>().materials)
            {
                if (!matrerls.Contains(Mat))
                {
                    matrerls.Add(Mat);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Speed = Game_Manger.GetComponent<Game_Manger>().Player_Speed;
        foreach (var Mat in matrerls)
        {
            Mat.color = new Color(108, 245, 181, 0);
            
        }


        PosZ -= Time.deltaTime * Speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, PosZ);
        if (PosZ <= -20)
        {
            Destroy(gameObject, 0.1f);
        }

        transform.Rotate(angularVelocity * Time.deltaTime, space);
    }
}
