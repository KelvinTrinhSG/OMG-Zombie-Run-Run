using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // [HideInInspector] public bool Its_Mobile_Game;      
    public bool Its_Mobile_Game;
    [HideInInspector] public float Spawen_Enemy_Enemy_Conte;    
    [HideInInspector] public float TranX;    
    [HideInInspector] public bool Had_Wepens;    
    [HideInInspector] public bool It_Heart;   
    [HideInInspector] public int Pistol_bullet;
    [HideInInspector] public int Pistol_bullet_Max;
    [HideInInspector] public int Uzi_bullet;
    [HideInInspector] public int Uzi_bullet_Max;
    [HideInInspector] public int Shotgun_bullet;
    [HideInInspector] public int Shotgun_bullet_Max;
    [HideInInspector] public int Machinegun_bullet;
    [HideInInspector] public int Machinegun_Max;
    [HideInInspector] public int Gun_Counter;

    public GameObject Pistol;
    public GameObject Uzi;
    public GameObject Shotgun;
    public GameObject Machinegun;
    public GameObject bullet_Spawen_Playes;
    public GameObject bullet_Roatat_Plass;
    public GameObject Bullet;
    public FixedJoystick FixedJoystick;
    public GameObject Wepens;
    public GameObject Charcter;
    public List<GameObject> Spawen_Obj;
    public List<GameObject> Spawen_Weps;
    public List<GameObject> Spawen_Obj_Decoration;
    public List<GameObject> Spawen_Enemy_Gm;
    public List<GameObject> Spawen_Helath_Gm;

    private GameObject Camera;
    private float RotY;
    private float Heart_delay = 2;
    private int Ran;
    private float Spawen_Obj_Time = 0.1f;
    private float Spawen_Wepns_Time = 0.1f;
    private float Spawen_Deco_Time = 0.1f;
    private float Spawen_Enemy_Time = 2;
    private float Spawen_Health_Time = 3;
    private float CamX;
    private GameObject Game_Manger;
    private float First_Delay = 2;
    private bool EnemyTime;
    void Start()
    {

        Spawen_Enemy_Enemy_Conte = Spawen_Enemy_Time;
        Game_Manger = GameObject.FindGameObjectWithTag ("Game Manger");        
        Camera = GameObject.Find("Main Camera");
        
    }
    private void LateUpdate()
    {
        
       if (Camera.transform.position.x < transform.position.x)
        {
            CamX += Time.deltaTime *1.75f;
            Camera.transform.position = new Vector3(CamX, Camera.transform.position.y, Camera.transform.position.z);
        }
        if (Camera.transform.position.x > transform.position.x)
        {
            CamX -= Time.deltaTime *1.75f;
            Camera.transform.position = new Vector3(CamX, Camera.transform.position.y, Camera.transform.position.z);
        }

    }
    
    void Update()
    {
        Its_Mobile_Game = Game_Manger.GetComponent<Game_Manger>().Its_Mobile_Game;
        if (Input.GetKeyDown (KeyCode.H) )
        {
            Fire();
        }
        Guns_Activat();
        if (It_Heart)
        {
            Heart_delay -= Time.deltaTime;
            if (Heart_delay <= 0)
            {
                gameObject.GetComponent<CharacterController>().enabled = true;
                gameObject.GetComponent<CapsuleCollider>().enabled = true;
                Heart_delay = 2;
                It_Heart = false;
            }
        }
        if (EnemyTime)
        {
            if (Spawen_Enemy_Enemy_Conte >= 1)
            {
                Spawen_Enemy_Enemy_Conte = Spawen_Enemy_Enemy_Conte - 0.0002f;
            }
            EnemyTime = false;
        }
        
        if (First_Delay > 0 )
        {
            First_Delay -= Time.deltaTime;
        }
        Monment();
        if (First_Delay <= 0)
        {
            Spawen_Object();
            Spawen_Object_Decoration();
            Spawen_Enemy();
            Spawen_Health();
            Spawen_Wepens();
        }

        if (Had_Wepens)
        {
            Charcter.GetComponent<Animator>().SetBool("Had-Gun", true);
            
            Wepens.SetActive(true);
            

        }
        else
        {
            Charcter.GetComponent<Animator>().SetBool("Had-Gun", false);            
            Wepens.SetActive(false);
            
        }
       

    }

    public void Spawen_Object ()
    {
        Spawen_Obj_Time -= Time.deltaTime;
        if (Spawen_Obj_Time <= 0)
        {
            var Sp_Ob_Pl = new Vector3(Random.Range(transform.position.x + 30, transform.position.x - 30), 0, Random.Range(transform.position.z + 90, transform.position.z + 120));

            Instantiate(Spawen_Obj[Random.Range (0, Spawen_Obj.Count)], Sp_Ob_Pl, Quaternion.identity);
            Spawen_Obj_Time = Random.Range (1,5);
        }
    }
    public void Spawen_Object_Decoration()
    {
        Spawen_Deco_Time -= Time.deltaTime;
        if (Spawen_Deco_Time <= 0)
        {
            var Sp_Ob_Pl = new Vector3(Random.Range(transform.position.x + 30, transform.position.x - 30), 0, Random.Range(transform.position.z + 90, transform.position.z + 120));

            Instantiate(Spawen_Obj_Decoration[Random.Range(0, Spawen_Obj_Decoration.Count)], Sp_Ob_Pl, Quaternion.identity);
            Spawen_Deco_Time = Random.Range(2, 8); ;
        }
    }

    public void Spawen_Enemy()
    {
        Spawen_Enemy_Time -= Time.deltaTime;
        if (Spawen_Enemy_Time <= 0)
        {
            var Sp_Ob_Pl = new Vector3(Random.Range(transform.position.x + 30, transform.position.x - 30), -0.17f, Random.Range(transform.position.z + 60, transform.position.z + 120));

            Instantiate(Spawen_Enemy_Gm[Random.Range(0, Spawen_Enemy_Gm.Count)], Sp_Ob_Pl, Quaternion.Euler(0,180,0));
            
            Spawen_Enemy_Time = Spawen_Enemy_Enemy_Conte;
            EnemyTime = true;
        }
    }
    public void Spawen_Health()
    {
        Spawen_Health_Time -= Time.deltaTime;
        if (Spawen_Health_Time <= 0)
        {
            var Sp_Ob_Pl = new Vector3(Random.Range(transform.position.x + 10, transform.position.x - 10), -0.17f, Random.Range(transform.position.z + 100, transform.position.z + 120));
            
            if (Game_Manger.GetComponent<Game_Manger>().harts <= 2)
            {
                Ran = Random.Range(0, 2);
            }
            else
            {
                Ran = Random.Range(0, 10);
            }

            if (Ran == 0)
            {
                Instantiate(Spawen_Helath_Gm[Random.Range(0, Spawen_Helath_Gm.Count)], Sp_Ob_Pl, Quaternion.Euler(0, 180, 0));
            }            
            Spawen_Health_Time = Random.Range(3, 6);
        }
    }
    public void Spawen_Wepens()
    {
        Spawen_Wepns_Time -= Time.deltaTime;
        if (Spawen_Wepns_Time <= 0)
        {
            var Sp_Ob_Pl = new Vector3(Random.Range(transform.position.x + 20, transform.position.x - 20), 0, Random.Range(transform.position.z + 90, transform.position.z + 120));

            Instantiate(Spawen_Weps[Random.Range(0, Spawen_Weps.Count)], Sp_Ob_Pl, Quaternion.identity);
            Spawen_Wepns_Time = Random.Range(3, 10);
        }
    }

    public void Monment ()
    {
        if (Its_Mobile_Game)
        {
            if (FixedJoystick.Horizontal != 0)
            {

                var NewTransform = new Vector3(transform.position.x + (FixedJoystick.Horizontal / 15), transform.position.y
               , transform.position.z);
                if (transform.position.x <= TranX + 10 && transform.position.x >= TranX - 10 && transform.position.x <= 50 && transform.position.x >= -50)
                {
                    transform.position = NewTransform;
                }
                if (transform.position.x >= 50)
                {
                    transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                }
                if (transform.position.x <= -50)
                {
                    transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                }
            }

            if (FixedJoystick.Horizontal > 0)
            {
                if (transform.rotation.y <= 0.1f)
                {

                    RotY += Time.deltaTime;
                    transform.rotation = new Quaternion(transform.rotation.x, RotY / 10, transform.rotation.z, transform.rotation.w);
                    Camera.transform.rotation = new Quaternion(Camera.transform.rotation.x, Camera.transform.rotation.y, -RotY / 20, Camera.transform.rotation.w);
                }

            }
            if (FixedJoystick.Horizontal < 0)
            {
                if (transform.rotation.y >= -0.1f)
                {

                    RotY -= Time.deltaTime;
                    transform.rotation = new Quaternion(transform.rotation.x, RotY / 10, transform.rotation.z, transform.rotation.w);
                    Camera.transform.rotation = new Quaternion(Camera.transform.rotation.x, Camera.transform.rotation.y, -RotY / 20, Camera.transform.rotation.w);
                }

            }
            if (FixedJoystick.Horizontal == 0)
            {

                TranX = transform.position.x;
                if (transform.rotation.y > 0)
                {
                    RotY -= Time.deltaTime;
                    transform.rotation = new Quaternion(transform.rotation.x, RotY / 10, transform.rotation.z, transform.rotation.w);
                    Camera.transform.rotation = new Quaternion(Camera.transform.rotation.x, Camera.transform.rotation.y, -RotY / 20, Camera.transform.rotation.w);

                }
                if (transform.rotation.y < 0)
                {
                    RotY += Time.deltaTime;
                    transform.rotation = new Quaternion(transform.rotation.x, RotY / 10, transform.rotation.z, transform.rotation.w);
                    Camera.transform.rotation = new Quaternion(Camera.transform.rotation.x, Camera.transform.rotation.y, -RotY / 20, Camera.transform.rotation.w);

                }
            }
        }
        else
        {
            if (Input.GetButton("Horizontal"))
            {

                var NewTransform = new Vector3(transform.position.x + (Input.GetAxis("Horizontal") / 15), transform.position.y
               , transform.position.z);
                if (transform.position.x <= TranX + 10 && transform.position.x >= TranX - 10 && transform.position.x <= 50 && transform.position.x >= -50)
                {
                    transform.position = NewTransform;
                }
                if (transform.position.x >= 50)
                {
                    transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                }
                if (transform.position.x <= -50)
                {
                    transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                }
            }

            if (Input.GetAxis("Horizontal") > 0)
            {
                if (transform.rotation.y <= 0.1f)
                {

                    RotY += Time.deltaTime;
                    transform.rotation = new Quaternion(transform.rotation.x, RotY / 10, transform.rotation.z, transform.rotation.w);
                    Camera.transform.rotation = new Quaternion(Camera.transform.rotation.x, Camera.transform.rotation.y, -RotY / 20, Camera.transform.rotation.w);
                }

            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (transform.rotation.y >= -0.1f)
                {

                    RotY -= Time.deltaTime;
                    transform.rotation = new Quaternion(transform.rotation.x, RotY / 10, transform.rotation.z, transform.rotation.w);
                    Camera.transform.rotation = new Quaternion(Camera.transform.rotation.x, Camera.transform.rotation.y, -RotY / 20, Camera.transform.rotation.w);
                }

            }
            if (Input.GetAxis("Horizontal") == 0)
            {

                TranX = transform.position.x;
                if (transform.rotation.y > 0)
                {
                    RotY -= Time.deltaTime;
                    transform.rotation = new Quaternion(transform.rotation.x, RotY / 10, transform.rotation.z, transform.rotation.w);
                    Camera.transform.rotation = new Quaternion(Camera.transform.rotation.x, Camera.transform.rotation.y, -RotY / 20, Camera.transform.rotation.w);

                }
                if (transform.rotation.y < 0)
                {
                    RotY += Time.deltaTime;
                    transform.rotation = new Quaternion(transform.rotation.x, RotY / 10, transform.rotation.z, transform.rotation.w);
                    Camera.transform.rotation = new Quaternion(Camera.transform.rotation.x, Camera.transform.rotation.y, -RotY / 20, Camera.transform.rotation.w);

                }
            }
        }
        
       

        
    }

    private void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag == "Obst")
        {
            Game_Manger.GetComponent<Game_Manger>().Heart_P();
            if (transform.position.x >= Col.transform.position.x)
            {
                transform.position = new Vector3(Col.transform.position.x + Col.transform.localScale.x / 2, transform.position.y
                , transform.position.z);
            }
            if (transform.position.x <= Col.transform.position.x)
            {
                transform.position = new Vector3(Col.transform.position.x - Col.transform.localScale.x / 2, transform.position.y
                , transform.position.z);
            }
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            It_Heart = true;
        }

        
        if (Col.gameObject.tag == "Health")
        {
            Game_Manger.GetComponent<Game_Manger>().harts++;
            Destroy(Col.gameObject);
            GameObject.FindObjectOfType<AudioMManger>().Playe("Hart  Pick Up");
        }
        if (Col.gameObject.tag == "Wepen")
        {
            
            if (Col.gameObject.GetComponent<Wepen>().Current_Wepen_Number == 0)
            {
                if (Pistol_bullet + 20 <= Pistol_bullet_Max)
                {
                    Pistol_bullet = Pistol_bullet + 20;
                }
                else
                {
                    Pistol_bullet = Pistol_bullet_Max;
                }
                GameObject.FindObjectOfType<AudioMManger>().Playe("Pistol Reaille");
            }
            if (Col.gameObject.GetComponent<Wepen>().Current_Wepen_Number == 1)
            {
                if (Uzi_bullet + 20 <= Uzi_bullet_Max)
                {
                    Uzi_bullet = Uzi_bullet + 20;
                }
                else
                {
                    Uzi_bullet = Uzi_bullet_Max;
                }
                GameObject.FindObjectOfType<AudioMManger>().Playe("Uzi Reaille");
            }
            if (Col.gameObject.GetComponent<Wepen>().Current_Wepen_Number == 2)
            {
                if (Shotgun_bullet + 20 <= Shotgun_bullet_Max)
                {
                    Shotgun_bullet = Shotgun_bullet + 20;
                }
                else
                {
                    Shotgun_bullet = Shotgun_bullet_Max;
                }
                GameObject.FindObjectOfType<AudioMManger>().Playe("ShotGun Reaille");
            }
            if (Col.gameObject.GetComponent<Wepen>().Current_Wepen_Number == 3)
            {
                if (Machinegun_bullet + 20 <= Machinegun_Max)
                {
                    Machinegun_bullet = Machinegun_bullet + 20;
                }
                else
                {
                    Machinegun_bullet = Machinegun_Max;
                }
                GameObject.FindObjectOfType<AudioMManger>().Playe("MachintGun Reaille");
            }
            Destroy(Col.gameObject);
        }
    }

    public void Fire ()
    {
       
        if (Gun_Counter == 1 )
        {
            if (Pistol_bullet > 0)
            {
                Pistol_bullet--;
                var bulet = Instantiate(Bullet, bullet_Spawen_Playes.transform.position, bullet_Spawen_Playes.transform.rotation);
                bulet.GetComponent<Bullet>().Power = 5;
                GameObject.FindObjectOfType<AudioMManger>().Playe("Pistol Fire");
            }
        }
        if (Gun_Counter == 2)
        {
            if (Uzi_bullet > 0)
            {
                Uzi_bullet--;
                var bulet = Instantiate(Bullet, bullet_Spawen_Playes.transform.position, bullet_Spawen_Playes.transform.rotation);
                bulet.GetComponent<Bullet>().Power = 8;
                GameObject.FindObjectOfType<AudioMManger>().Playe("Uzi Fire");
            }
        }
        if (Gun_Counter == 3)
        {
            if (Shotgun_bullet > 0)
            {
                Shotgun_bullet--;
               var bulet = Instantiate(Bullet, bullet_Spawen_Playes.transform.position, bullet_Spawen_Playes.transform.rotation);
                bulet.GetComponent<Bullet>().Power = 10;
                GameObject.FindObjectOfType<AudioMManger>().Playe("Shotgun Fire");
            }
        }
        if (Gun_Counter == 4)
        {
            if (Machinegun_bullet > 0)
            {
                Machinegun_bullet--;
                
                var bulet = Instantiate(Bullet, bullet_Spawen_Playes.transform.position, bullet_Spawen_Playes.transform.rotation);
                bulet.GetComponent<Bullet>().Power = 9;
                GameObject.FindObjectOfType<AudioMManger>().Playe("Machin Gun Fire");
            }
        }
    }
    public void Guns_Activat ()
    {
        if (Gun_Counter == 0)
        {
            Pistol.SetActive(false);
            Uzi.SetActive(false);
            Shotgun.SetActive(false);
            Machinegun.SetActive(false);
        }
        if (Gun_Counter == 1)
        {
            Pistol.SetActive(true);
            Uzi.SetActive(false);
            Shotgun.SetActive(false);
            Machinegun.SetActive(false);
        }
        if (Gun_Counter == 2)
        {
            Pistol.SetActive(false);
            Uzi.SetActive(true);
            Shotgun.SetActive(false);
            Machinegun.SetActive(false);
        }
        if (Gun_Counter == 3)
        {
            Pistol.SetActive(false);
            Uzi.SetActive(false);
            Shotgun.SetActive(true);
            Machinegun.SetActive(false);
        }
        if (Gun_Counter == 4)
        {
            Pistol.SetActive(false);
            Uzi.SetActive(false);
            Shotgun.SetActive(false);
            Machinegun.SetActive(true);
        }
    }
}
