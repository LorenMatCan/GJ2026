using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienteFondo : MonoBehaviour
{
    public static BGM BGMMenu;
    public AudioSource source; 
    public AudioClip[] musica;
    private bool SeguimosEnMenu = false; 
    private bool pausa = false; 

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    ///Paramos la música
    public void PararMusica()
    {
        pausa = true; 
        source.Pause(); 
    }

    //Continua con la música 
    public void ContinuarMusica()
    {
        pausa = false; 
        source.UnPause(); 
    }

    //Obtenemos el estado actual del AudioSource
    // Si esta sonando regresa true, en el caso contrario false
    public bool EstadoMusica()
    {
        return source.isPlaying; 
    }

    //Cambiar musica de fondo que esta sonando 
    /// In número de nivel
    public void CambiarMusica(int nivel)
    {
        //Si regresamos al menú evitamos iniciar otra vez la canción
        if(SeguimosEnMenu && nivel ==0 )
        {
            return; 
        }
        source.loop = true; 
        source.clip = musica[nivel]; 
        source.Play();
        //Al cambiar de clip en automatico sigue la música, por lo cual 
        // mediante la variable pausa volvems a pausar la música evitando que suene
        if (pausa)
        {
            PararMusica(); 
        }


    }

    public void CambiarMusicaNivel(AudioClip audio) 
    {
        source.loop = true; 
        source.clip = audio;
        source.Play();
    }

}
