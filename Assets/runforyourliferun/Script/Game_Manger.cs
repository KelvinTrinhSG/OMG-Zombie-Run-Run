using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manger : MonoBehaviour
{

    
    [HideInInspector]public int Max_Hart;
    [HideInInspector] public int harts;
    [HideInInspector] public int Dead_Zombie;
    [HideInInspector] public string Distens;
    [HideInInspector] public float _player_Speed;
    [HideInInspector] public bool Stop_Meters;

    public bool Its_Mobile_Game;    
    public Text Meters_Text;
    public Text Meters10_Text;
    public Text Meters100_Text;
    public Text Meters1K_Text;
    public Text Meters10K_Text;
    public Text Meters100K_Text;
    public Text Meters1M_Text;
    public Text Cama1_Text;
    public Text Cama2_Text;
    public GameObject Texts_Holder;    
    public GameObject Heart_Blod;
    public GameObject Hart01;
    public GameObject Hart02;
    public GameObject Hart03;
    public GameObject Hart04;
    public GameObject Hart05;
    public GameObject Hart06;
    public GameObject Hart07;
    public Sprite Hart_Zero;
    public Sprite Hart_Half;
    public Sprite Hart_Full;    
    public Text Dead_Zombie_Text;
    public float Player_Speed;

    
    private float Delay = 1f;
    private GameObject Player;
    private int Meters;
    private int Meters10;
    private int Meters100;
    private int Meters1K;
    private int Meters10K;
    private int Meters100K;
    private int Meters1M;
    private float Heart_V;
    private Vector3 Text_H_Playes;


    void Start()
    {
        
        Player = GameObject.FindGameObjectWithTag("Player");
        Text_H_Playes = Texts_Holder.GetComponent<RectTransform>().anchoredPosition;
        _player_Speed = Player_Speed;
        Stop_Meters = true;
    }

    
    void Update()
    {

        
        Dist_Calculator();
        Dead_Zombie_Text.text = Dead_Zombie.ToString();
        if (harts >= Max_Hart *2 && harts > 2)
        {
            Max_Hart++;
        }
        Hart_P();
        Hart_F();
        Heart_Blod.GetComponent<Image>().color = new Color(0.160f, 0.076f, 0.076f, Heart_V);
        if (Heart_V >= 0)
        {
            Heart_V -= Time.deltaTime / 30;
        }
        if (!Stop_Meters)
        {
            Delay -= Time.deltaTime;
        }
        
        if (Delay <= 0)
        {
            if (Meters < 9)
            {
                Meters++;
                Meters_Text.GetComponent<Animator>().Play("Meters");
                Delay = 1f;
            }
            else
            {
                Meters = 0;
                Meters10++;
                Meters_Text.GetComponent<Animator>().Play("Meters");
                Meters10_Text.GetComponent<Animator>().Play("Meters");
                Delay = 1f;
                if (Player.GetComponent<Player>().Spawen_Enemy_Enemy_Conte > 0.1f)
                {
                    Player.GetComponent<Player>().Spawen_Enemy_Enemy_Conte = Player.GetComponent<Player>().Spawen_Enemy_Enemy_Conte - 0.00001f;
                }
            }
            if (Meters10 > 9)
            {
                Meters100++;
                Meters10 = 0;
                Meters100_Text.GetComponent<Animator>().Play("Meters");
                
            }
            if (Meters100 > 9)
            {
                Meters1K++;
                Meters100 = 0;
                Meters1K_Text.GetComponent<Animator>().Play("Meters");
                if (Meters1K_Text.GetComponent<Text>().color.a < 1 )
                {
                    Meters1K_Text.GetComponent<Text>().color = new Color(1, 1, 1, 1);
                    Cama1_Text.GetComponent<Text>().color = new Color(1, 1, 1, 1);
                    Meters10K_Text.GetComponent<Text>().color = new Color(1, 1, 1, 0.2f);
                    Texts_Holder.GetComponent<RectTransform>().anchoredPosition = new Vector3(543.47f, Texts_Holder.GetComponent<RectTransform>().anchoredPosition.y
                   , 0);

                }
               
                   
                
                
            }
            if (Meters1K > 9)
            {
                Meters10K++;
                Meters1K = 0;
                Meters10K_Text.GetComponent<Animator>().Play("Meters");
                if (Meters10K_Text.GetComponent<Text>().color.a < 1)
                {
                    Meters10K_Text.GetComponent<Text>().color = new Color(1, 1, 1, 1);                    
                    Meters100K_Text.GetComponent<Text>().color = new Color(1, 1, 1, 0.2f);
                    Texts_Holder.GetComponent<RectTransform>().anchoredPosition = new Vector3(581.4f, Texts_Holder.GetComponent<RectTransform>().anchoredPosition.y
                    , 0);

                }
                
            }
            if (Meters10K > 9)
            {
                Meters100K++;
                Meters10K = 0;
                Meters100K_Text.GetComponent<Animator>().Play("Meters");
                if (Meters100K_Text.GetComponent<Text>().color.a < 1)
                {
                    Meters100K_Text.GetComponent<Text>().color = new Color(1, 1, 1, 1);
                    Meters1M_Text.GetComponent<Text>().color = new Color(1, 1, 1, 0.2f);
                    Cama2_Text.GetComponent<Text>().color = new Color(1, 1, 1, 0.2f);
                    Texts_Holder.GetComponent<RectTransform>().anchoredPosition = new Vector3(623f, Texts_Holder.GetComponent<RectTransform>().anchoredPosition.y
                  , 0);
                }
            }
            if (Meters100K > 9)
            {
                Meters1M++;
                Meters100K = 0;               
                if (Meters1M_Text.GetComponent<Text>().color.a < 1)
                {
                    Meters1M_Text.GetComponent<Text>().color = new Color(1, 1, 1, 1);                    
                    Cama2_Text.GetComponent<Text>().color = new Color(1, 1, 1, 1);
                }
            }
        }
        Meters_Text.text = Meters.ToString("0");
        Meters10_Text.text = Meters10.ToString("0");
        Meters100_Text.text = Meters100.ToString("0");
        Meters1K_Text.text = Meters1K.ToString("0");
        Meters10K_Text.text = Meters10K.ToString("0");
        Meters100K_Text.text = Meters100K.ToString("0");
        Meters1M_Text.text = Meters1M.ToString("000");
        if (Input.GetKeyDown (KeyCode.M))
        {
            harts++;
        }
    }

    public void Heart_P ()
    {
        if (Heart_V <= 0.6f)
        {
            Heart_V = Heart_V + 0.2f;           
        }
        harts--;
    }
    public void Hart_P ()
    {
        if (Max_Hart <= 2)
        {
            Hart01.SetActive(true);
            Hart02.SetActive(true);            
        }
        if (Max_Hart >= 3)
        {
            Hart01.SetActive(true);
            Hart02.SetActive(true);
            Hart03.SetActive(true);
        }
        if (Max_Hart >= 4)
        {
            Hart01.SetActive(true);
            Hart02.SetActive(true);
            Hart03.SetActive(true);
            Hart04.SetActive(true);
        }
        if (Max_Hart >= 5)
        {
            Hart01.SetActive(true);
            Hart02.SetActive(true);
            Hart03.SetActive(true);
            Hart04.SetActive(true);
            Hart05.SetActive(true);
        }
        if (Max_Hart >= 6)
        {
            Hart01.SetActive(true);
            Hart02.SetActive(true);
            Hart03.SetActive(true);
            Hart04.SetActive(true);
            Hart05.SetActive(true);
            Hart06.SetActive(true);
        }
        if (Max_Hart >= 7)
        {
            Hart01.SetActive(true);
            Hart02.SetActive(true);
            Hart03.SetActive(true);
            Hart04.SetActive(true);
            Hart05.SetActive(true);
            Hart06.SetActive(true);
            Hart07.SetActive(true);
        }
        
    }
    public void Hart_F ()
    {
        if (harts <= 0)
        {
            harts = 0;
            Hart01.GetComponent<Image>().sprite = Hart_Zero;
            Hart02.GetComponent<Image>().sprite = Hart_Zero;
            Hart03.GetComponent<Image>().sprite = Hart_Zero;
            Hart04.GetComponent<Image>().sprite = Hart_Zero;
            Hart05.GetComponent<Image>().sprite = Hart_Zero;
            Hart06.GetComponent<Image>().sprite = Hart_Zero;
            Hart07.GetComponent<Image>().sprite = Hart_Zero;
            //Debug.Log("Player Dead");

        }
        if (harts >= 1)
        {           
            Hart01.GetComponent<Image>().sprite = Hart_Half;
            Hart02.GetComponent<Image>().sprite = Hart_Zero;
            Hart03.GetComponent<Image>().sprite = Hart_Zero;
            Hart04.GetComponent<Image>().sprite = Hart_Zero;
            Hart05.GetComponent<Image>().sprite = Hart_Zero;
            Hart06.GetComponent<Image>().sprite = Hart_Zero;
            Hart07.GetComponent<Image>().sprite = Hart_Zero;
        }
        if (harts >= 2)
        {
            Hart01.GetComponent<Image>().sprite = Hart_Full;
            Hart02.GetComponent<Image>().sprite = Hart_Zero;
            Hart03.GetComponent<Image>().sprite = Hart_Zero;
            Hart04.GetComponent<Image>().sprite = Hart_Zero;
            Hart05.GetComponent<Image>().sprite = Hart_Zero;
            Hart06.GetComponent<Image>().sprite = Hart_Zero;
            Hart07.GetComponent<Image>().sprite = Hart_Zero;
        }
        if (harts >= 3)
        {
            Hart01.GetComponent<Image>().sprite = Hart_Full;
            Hart02.GetComponent<Image>().sprite = Hart_Half;
            Hart03.GetComponent<Image>().sprite = Hart_Zero;
            Hart04.GetComponent<Image>().sprite = Hart_Zero;
            Hart05.GetComponent<Image>().sprite = Hart_Zero;
            Hart06.GetComponent<Image>().sprite = Hart_Zero;
            Hart07.GetComponent<Image>().sprite = Hart_Zero;
        }
        if (harts >= 4)
        {
            Hart01.GetComponent<Image>().sprite = Hart_Full;
            Hart02.GetComponent<Image>().sprite = Hart_Full;
            Hart03.GetComponent<Image>().sprite = Hart_Zero;
            Hart04.GetComponent<Image>().sprite = Hart_Zero;
            Hart05.GetComponent<Image>().sprite = Hart_Zero;
            Hart06.GetComponent<Image>().sprite = Hart_Zero;
            Hart07.GetComponent<Image>().sprite = Hart_Zero;
        }
        if (harts >= 5)
        {
            Hart01.GetComponent<Image>().sprite = Hart_Full;
            Hart02.GetComponent<Image>().sprite = Hart_Full;
            Hart03.GetComponent<Image>().sprite = Hart_Half;
            Hart04.GetComponent<Image>().sprite = Hart_Zero;
            Hart05.GetComponent<Image>().sprite = Hart_Zero;
            Hart06.GetComponent<Image>().sprite = Hart_Zero;
            Hart07.GetComponent<Image>().sprite = Hart_Zero;
        }
        if (harts >= 6)
        {
            Hart01.GetComponent<Image>().sprite = Hart_Full;
            Hart02.GetComponent<Image>().sprite = Hart_Full;
            Hart03.GetComponent<Image>().sprite = Hart_Full;
            Hart04.GetComponent<Image>().sprite = Hart_Zero;
            Hart05.GetComponent<Image>().sprite = Hart_Zero;
            Hart06.GetComponent<Image>().sprite = Hart_Zero;
            Hart07.GetComponent<Image>().sprite = Hart_Zero;
        }
        if (harts >= 7)
        {
            Hart01.GetComponent<Image>().sprite = Hart_Full;
            Hart02.GetComponent<Image>().sprite = Hart_Full;
            Hart03.GetComponent<Image>().sprite = Hart_Full;
            Hart04.GetComponent<Image>().sprite = Hart_Half;
            Hart05.GetComponent<Image>().sprite = Hart_Zero;
            Hart06.GetComponent<Image>().sprite = Hart_Zero;
            Hart07.GetComponent<Image>().sprite = Hart_Zero;
        }
        if (harts >= 8)
        {
            Hart01.GetComponent<Image>().sprite = Hart_Full;
            Hart02.GetComponent<Image>().sprite = Hart_Full;
            Hart03.GetComponent<Image>().sprite = Hart_Full;
            Hart04.GetComponent<Image>().sprite = Hart_Full;
            Hart05.GetComponent<Image>().sprite = Hart_Zero;
            Hart06.GetComponent<Image>().sprite = Hart_Zero;
            Hart07.GetComponent<Image>().sprite = Hart_Zero;
        }
        if (harts >= 9)
        {
            Hart01.GetComponent<Image>().sprite = Hart_Full;
            Hart02.GetComponent<Image>().sprite = Hart_Full;
            Hart03.GetComponent<Image>().sprite = Hart_Full;
            Hart04.GetComponent<Image>().sprite = Hart_Full;
            Hart05.GetComponent<Image>().sprite = Hart_Half;
            Hart06.GetComponent<Image>().sprite = Hart_Zero;
            Hart07.GetComponent<Image>().sprite = Hart_Zero;
        }
        if (harts >= 10)
        {
            Hart01.GetComponent<Image>().sprite = Hart_Full;
            Hart02.GetComponent<Image>().sprite = Hart_Full;
            Hart03.GetComponent<Image>().sprite = Hart_Full;
            Hart04.GetComponent<Image>().sprite = Hart_Full;
            Hart05.GetComponent<Image>().sprite = Hart_Full;
            Hart06.GetComponent<Image>().sprite = Hart_Zero;
            Hart07.GetComponent<Image>().sprite = Hart_Zero;
        }
        if (harts >= 11)
        {
            Hart01.GetComponent<Image>().sprite = Hart_Full;
            Hart02.GetComponent<Image>().sprite = Hart_Full;
            Hart03.GetComponent<Image>().sprite = Hart_Full;
            Hart04.GetComponent<Image>().sprite = Hart_Full;
            Hart05.GetComponent<Image>().sprite = Hart_Full;
            Hart06.GetComponent<Image>().sprite = Hart_Half;
            Hart07.GetComponent<Image>().sprite = Hart_Zero;
        }
        if (harts >= 12)
        {
            Hart01.GetComponent<Image>().sprite = Hart_Full;
            Hart02.GetComponent<Image>().sprite = Hart_Full;
            Hart03.GetComponent<Image>().sprite = Hart_Full;
            Hart04.GetComponent<Image>().sprite = Hart_Full;
            Hart05.GetComponent<Image>().sprite = Hart_Full;
            Hart06.GetComponent<Image>().sprite = Hart_Full;
            Hart07.GetComponent<Image>().sprite = Hart_Zero;
        }
        if (harts >= 13)
        {
            Hart01.GetComponent<Image>().sprite = Hart_Full;
            Hart02.GetComponent<Image>().sprite = Hart_Full;
            Hart03.GetComponent<Image>().sprite = Hart_Full;
            Hart04.GetComponent<Image>().sprite = Hart_Full;
            Hart05.GetComponent<Image>().sprite = Hart_Full;
            Hart06.GetComponent<Image>().sprite = Hart_Full;
            Hart07.GetComponent<Image>().sprite = Hart_Half;
        }
        if (harts >= 14)
        {
            Hart01.GetComponent<Image>().sprite = Hart_Full;
            Hart02.GetComponent<Image>().sprite = Hart_Full;
            Hart03.GetComponent<Image>().sprite = Hart_Full;
            Hart04.GetComponent<Image>().sprite = Hart_Full;
            Hart05.GetComponent<Image>().sprite = Hart_Full;
            Hart06.GetComponent<Image>().sprite = Hart_Full;
            Hart07.GetComponent<Image>().sprite = Hart_Full;
            harts = 14;
        }
    }
    public void Add_Dead_Zombie ()
    {
        Dead_Zombie++;
    }
    public void Dist_Calculator ()
    {
        Distens = (Meters10.ToString()+ Meters.ToString() + "m");
        if (Meters100 > 0 )
        {
            Distens = Meters100.ToString() + Distens;
        }
        if (Meters1K > 0)
        {
            Distens = Meters1K.ToString()+","+Meters100.ToString()+Distens;
        }
        if (Meters10K > 0)
        {
            Distens = Meters10K.ToString()+Meters1K.ToString()+","+Meters100.ToString()+Distens;
        }
        if (Meters100K > 0)
        {
            Distens = Meters100K.ToString()+Meters10K.ToString()+Meters1K.ToString()+","+Meters100.ToString()+Distens;
        }
        if (Meters1M > 0)
        {
            Distens = Meters1M.ToString()+","+ Meters100K.ToString()+ Meters10K.ToString()+Meters1K.ToString()+","+Meters100.ToString()+Distens;
        }
    }
    public void Rest_Meters ()
    {
        Meters = 0;
        Meters10 = 0;
        Meters100 = 0;
        Meters1K = 0;
        Meters10K = 0;
        Meters100K = 0;
        Meters1M = 0;
    }
}
