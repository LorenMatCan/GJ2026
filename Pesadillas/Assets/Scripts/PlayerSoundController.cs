using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource source; 
    public AudioClip musica;
    public PlayerController playerController;
    private bool activo=false ;

    void Start()
    {
        playerController.Caminar += InicirPasos; 
        playerController.Parar += PararPasos; 
    }

    private void InicirPasos()
    {
        if(!activo)
        {
            activo=true;
            source.loop = true;
            source.clip = musica;
            source.Play();
        }
    }
    

    private void PararPasos()
    {
        source.Pause(); 
        activo =false;
    }


    void OnDestroy()
    {
        playerController.Caminar -= InicirPasos; 
        playerController.Parar -= PararPasos; 
    }

}
