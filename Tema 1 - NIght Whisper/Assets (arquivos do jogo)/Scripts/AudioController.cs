using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource mscFundo;
    public AudioClip[] musicas;
    // Start is called before the first frame update
    void Start()
    {
        int IndexMsc = Random.Range(0, 2);
        AudioClip fundoFase = musicas[IndexMsc];
        mscFundo.clip = fundoFase;
        mscFundo.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
