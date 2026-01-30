using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
   public static SFX SFXMenu;
   private bool detenerSFX ; 
   [SerializeField] private AudioSource SFXObjeto; 

    private void Awake()
    {
        if (SFXMenu != null && SFXMenu  != this)
        {
            Destroy(this.gameObject);
            return;
        }
        SFXMenu  = this;
        DontDestroyOnLoad(this); 

    }

    ///Paramos la creación de SFX
    public void PararSFX()
    {
        detenerSFX = true; 
    }

    ///Continuamos con la creación de SFX
    public void ContinuarSFX()
    {
        detenerSFX = false; 
    }

    ///Dependiendo de estado de detenerSFX se crea o no el efecto de sonido 
    /// 
    public void SuenaFXClip(AudioClip clip, Transform posicion, float volumen = 0.8f)
    {
    
        if(!detenerSFX)
        { 
            //Creamos un nuevo audioSource el cual al terminar el efecto de sonido se va a destruir
            AudioSource source = Instantiate(SFXObjeto, posicion.position, Quaternion.identity); 
            source.clip= clip;
            source.volume = volumen;  
            source.Play(); 
            float duracion = source.clip.length; 
            Destroy(source.gameObject, duracion); 
        }
    }

}
