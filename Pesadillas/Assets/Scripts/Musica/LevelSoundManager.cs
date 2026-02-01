using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSoundManager : MonoBehaviour
{
    public AudioSource source; 
    public AudioClip[] musica;
    public static BGM BGMMenu;
    private bool pausa = false; 
    private int estado = 0; 
    private bool entro = false; 

    private void Awake()
    {
        CambiarMusica(1);
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

    public void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag("Jugador"))
            {
                Debug.Log(estado); 
                if(!entro)
                {
                    entro = true; 
                    if(estado == 1)
                    {
                        CambiarMusica(0); 
                        estado = 0;
                        BGM.BGMMenu.CambiarMusica(1);  
                        }
                        else
                        {
                            CambiarMusica(1); 
                            estado = 1;
                            BGM.BGMMenu.CambiarMusica(2); 
                    }
                    StartCoroutine(Estado()); 
                }
            }
    }

    private IEnumerator Estado()
    {
        yield return new WaitForSeconds(2);
        entro = !entro  ; 
    }
}
