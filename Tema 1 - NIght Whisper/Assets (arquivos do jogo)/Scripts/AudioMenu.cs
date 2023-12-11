using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    public AudioSource mscFundo;
    public AudioClip musicas;

    // Start is called before the first frame update
    void Start()
    {
        AudioClip fundoFase = musicas;
        mscFundo.clip = fundoFase;
        mscFundo.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
