using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioMManger : MonoBehaviour
{
    public Sawnd [] Sawends;

    void Awake()
    {
        foreach (Sawnd s in Sawends)
        {
          s.Sourc =  gameObject.AddComponent<AudioSource>();
          s.Sourc.clip = s.Clip;

          s.Sourc.volume = s.Volume;
            s.Sourc.pitch = s.Pitch;
            s.Sourc.loop = s.Loop;
        }
    }

    
    public void Playe (string name)
    {
        Sawnd s =  Array.Find(Sawends, Sawnd => Sawnd.Name == name);
        s.Sourc.Play();
        
        
    }
    public void stop (string name)
    {
        Sawnd s = Array.Find(Sawends, Sawnd => Sawnd.Name == name);
        s.Sourc.Stop();

    }
}
