using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource source; 
    public AudioClip musica;
    public PlayerController playerController;
    private bool activo=false ;
    private IEnumerator tiempo; 

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
            //source.loop = true; 
            source.clip = musica; 
            //source.Play();
            tiempo = PlayAudioWithDelay(); 
            StartCoroutine(tiempo);
        }
    }

    

    private void PararPasos()
    {
        StopCoroutine(tiempo);
        source.Pause(); 
        activo =false;
    }

    IEnumerator PlayAudioWithDelay()
    {
        while (true) // Loop indefinitely
        {
            source.Play();
            // Wait for the duration of the clip
            yield return new WaitForSeconds(source.clip.length);

            // Wait for the specified delay before the next loop
            yield return new WaitForSeconds(0.125f);
        }
    }

    void OnDestroy()
    {
        playerController.Caminar -= InicirPasos; 
        playerController.Parar -= PararPasos; 
    }

}
