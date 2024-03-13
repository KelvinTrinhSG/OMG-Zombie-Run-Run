using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Thirdweb;

public class Canvas_Manger : MonoBehaviour
{
    public Text Dist_Text;
    public GameObject Text_Holder;
    public GameObject Harts;
    public GameObject Guns;
    public GameObject Dead_Zombie;
    public GameObject Movment_Button;
    public GameObject Dead_Menu;
    public GameObject Mobile_Movment_Button;
    public GameObject Pc_Movment_Button;
    public GameObject Play_Menu;
    private const string DROP_ERC20_CONTRACT = "0xe827fACEe1De7320c8aeD25C01B90a0f4cFFCa2B";

    private GameObject Game_Manger;
    private bool Add = false;
    private int ShowAds;
    void Start()
    {
        Game_Manger = GameObject.FindGameObjectWithTag("Game Manger");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Add)
        {
           if (Game_Manger.GetComponent<Game_Manger>().Player_Speed < Game_Manger.GetComponent<Game_Manger>()._player_Speed)
           {
                Game_Manger.GetComponent<Game_Manger>().Player_Speed += Time.deltaTime;
           }
           

        }
        if (Game_Manger.GetComponent<Game_Manger>().Player_Speed >= Game_Manger.GetComponent<Game_Manger>()._player_Speed)
        {
            Add = false;
            
        }
        
        Dist_Text.text = Game_Manger.GetComponent<Game_Manger>().Distens;
        if (Game_Manger.GetComponent<Game_Manger>().harts <= 0 )
        {
            //GameObject.FindObjectOfType<AdmobAdss>().DestroyAd();
            GameObject.FindObjectOfType<AudioMManger>().stop("Breath");           
            //Text_Holder.SetActive(false);
            Harts.SetActive(false);
            Guns.SetActive(false);
            Dead_Zombie.SetActive(false);
            Movment_Button.SetActive(false);
            Dead_Menu.SetActive(true);
            Game_Manger.GetComponent<Game_Manger>().Stop_Meters = true;
            if (Game_Manger.GetComponent<Game_Manger>().Player_Speed > 0)
            {
                Game_Manger.GetComponent<Game_Manger>().Player_Speed -= Time.deltaTime;
            }
           
            
        }
        if (Game_Manger.GetComponent<Game_Manger>().Its_Mobile_Game)
        {
            Mobile_Movment_Button.SetActive(true);
            Pc_Movment_Button.SetActive(false);
        }
        else
        {
            Mobile_Movment_Button.SetActive(false);
            Pc_Movment_Button.SetActive(true);
        }
        
    }
    public void Reaplay ()
    {
        
        var Enemies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (var Enm in Enemies)
        {
            Destroy(Enm.gameObject);
        }
        var Object = GameObject.FindGameObjectsWithTag("Obst");
        foreach (var Obs in Object)
        {
            Destroy(Obs.gameObject);
        }
        var Health = GameObject.FindGameObjectsWithTag("Health");
        foreach (var Hlth in Health)
        {
            Destroy(Hlth.gameObject);
        }
        var Wepens = GameObject.FindGameObjectsWithTag("Wepen");
        foreach (var Wps in Wepens)
        {
            Destroy(Wps.gameObject);
        }
        Guns.SetActive(true);
        GameObject.FindObjectOfType<Guns_Canves>().Gun_Nmaber = 0;
        GameObject.FindObjectOfType<Player>().Pistol_bullet = 0;
        GameObject.FindObjectOfType<Player>().Uzi_bullet = 0;
        GameObject.FindObjectOfType<Player>().Shotgun_bullet = 0;
        GameObject.FindObjectOfType<Player>().Machinegun_bullet = 0;
        Game_Manger.GetComponent<Game_Manger>().Dead_Zombie = 0;
        Game_Manger.GetComponent<Game_Manger>().harts = 2;
        Game_Manger.GetComponent<Game_Manger>().Stop_Meters = false;
        Game_Manger.GetComponent<Game_Manger>().Rest_Meters();
        GameObject.FindObjectOfType<AudioMManger>().Playe("Breath");
        Text_Holder.SetActive(true);
        Harts.SetActive(true);        
        Dead_Zombie.SetActive(true);
        Movment_Button.SetActive(true);
        Dead_Menu.SetActive(false);
        Add = true;
        
        
    }    
    public void Play ()
    {
        Game_Manger.GetComponent<Game_Manger>().Stop_Meters = false;
        Play_Menu.SetActive(false);
        GameObject.FindObjectOfType<AudioMManger>().Playe("Breath");        
        //GameObject.FindObjectOfType<AdmobAdss>().LoadAd(); 
    }
    public async void Revive ()
    {
        
        // GameObject.FindObjectOfType<AdmobAdss>().ShowRewardedAd();
        try
        {
            Debug.Log("Minting ERC20");
            Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC20_CONTRACT);
            var results = await contract.ERC20.Claim("1");
            Debug.Log("ERC20 minted");
            Resume ();
        }
        catch (System.Exception)
        {
            Debug.Log("Error minting ERC20");
        }
    }
    public void Resume ()
    {

        
        Game_Manger.GetComponent<Game_Manger>().harts = 3;
        Game_Manger.GetComponent<Game_Manger>().Stop_Meters = false;
        GameObject.FindObjectOfType<AudioMManger>().Playe("Breath");

        Text_Holder.SetActive(true);
        Harts.SetActive(true);
        Guns.SetActive(true);
        Dead_Zombie.SetActive(true);
        Movment_Button.SetActive(true);
        Dead_Menu.SetActive(false);
        Add = true;
        
        
    }
    public void REviv_Replay()
    {
        ShowAds = Random.Range(0, 2);
        if (ShowAds == 0)
        {
            GameObject.FindObjectOfType<AdmobAdss>().ShowAd();
        }
        else
        {
            Reaplay();
        }    

    }
}
