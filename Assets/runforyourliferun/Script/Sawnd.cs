using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sawnd  
{
    public AudioClip Clip;

    public string Name;


    [Range (0f , 1f)]
    public float Volume;

    [Range(1f, 3f)]
    public float Pitch;

    public bool Loop;

    [HideInInspector]
    public AudioSource Sourc;

}
