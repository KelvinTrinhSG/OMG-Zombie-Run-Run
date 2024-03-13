using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guns_Canves : MonoBehaviour
{
    public GameObject Nothings;
    public GameObject Pistol;
    public GameObject Uzi;
    public GameObject Shotgun;
    public GameObject Machinegun;
    public GameObject Plus;
    public Text Bullet_counter;
    public Text Bullet_Max;

    [HideInInspector]public int Gun_Nmaber;

    private GameObject Player;
    private GameObject Camera;
    private float traz;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Camera = GameObject.Find("Main Camera");
        traz = Camera.transform.position.z;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Add_Number();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Mines_Number();
        }
        if (Gun_Nmaber != 0)
        {
            Player.GetComponent<Player>().Had_Wepens = true;
            
            if (Camera.transform.position.z <= -1.4)
            {
                traz += Time.deltaTime*5;
                Camera.transform.position = new Vector3(Camera.transform.position.x,
                Camera.transform.position.y, traz);
            }
        }
        else
        {
            Player.GetComponent<Player>().Had_Wepens = false;
            if (Camera.transform.position.z >= -4.4)
            {
                traz -= Time.deltaTime*4;
                Camera.transform.position = new Vector3(Camera.transform.position.x,
                Camera.transform.position.y, traz);
            }
        }
       
        Player.GetComponent<Player>().Gun_Counter = Gun_Nmaber;
        if (Gun_Nmaber == 0)
        {
            Nothings.SetActive(true);
            Pistol.SetActive(false);
            Uzi.SetActive(false);
            Shotgun.SetActive(false);
            Machinegun.SetActive(false);
            Plus.SetActive(false);

            Bullet_counter.text = "???";
            Bullet_Max.text = "???";
        }
        if (Gun_Nmaber == 1)
        {
            Nothings.SetActive(false);
            Pistol.SetActive(true);
            Uzi.SetActive(false);
            Shotgun.SetActive(false);
            Machinegun.SetActive(false);
            Plus.SetActive(true);

            Bullet_counter.text = Player.GetComponent<Player>().Pistol_bullet.ToString("000");
            Bullet_Max.text = Player.GetComponent<Player>().Pistol_bullet_Max.ToString("000");
        }
        if (Gun_Nmaber == 2)
        {
            Nothings.SetActive(false);
            Pistol.SetActive(false);
            Uzi.SetActive(true);
            Shotgun.SetActive(false);
            Machinegun.SetActive(false);
            Plus.SetActive(true);

            Bullet_counter.text = Player.GetComponent<Player>().Uzi_bullet.ToString("000");
            Bullet_Max.text = Player.GetComponent<Player>().Uzi_bullet_Max.ToString("000");
        }
        if (Gun_Nmaber == 3)
        {
            Nothings.SetActive(false);
            Pistol.SetActive(false);
            Uzi.SetActive(false);
            Shotgun.SetActive(true);
            Machinegun.SetActive(false);
            Plus.SetActive(true);

            Bullet_counter.text = Player.GetComponent<Player>().Shotgun_bullet.ToString("000");
            Bullet_Max.text = Player.GetComponent<Player>().Shotgun_bullet_Max.ToString("000");

        }
        if (Gun_Nmaber == 4)
        {
            Nothings.SetActive(false);
            Pistol.SetActive(false);
            Uzi.SetActive(false);
            Shotgun.SetActive(false);
            Machinegun.SetActive(true);
            Plus.SetActive(true);

            Bullet_counter.text = Player.GetComponent<Player>().Machinegun_bullet.ToString("000");
            Bullet_Max.text = Player.GetComponent<Player>().Machinegun_Max.ToString("000");
        }
    }
    public void Add_Number ()
    {
        if (Gun_Nmaber < 4)
        {
            Gun_Nmaber++;
        }
        else
        {
            Gun_Nmaber = 0;
           
        }
        
    }
    public void Mines_Number()
    {
        if (Gun_Nmaber > 0)
        {
            Gun_Nmaber--;
        }
        else
        {
            Gun_Nmaber = 4;

        }
    }
       
}
