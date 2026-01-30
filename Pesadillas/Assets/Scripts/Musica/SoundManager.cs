using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundM;
    [SerializeField] Image sonido; 
    [SerializeField] Image sinSonido;

    public bool silencio;

    private void Awake()
    {
        if (soundM != null && soundM  != this)
        {
            Destroy(this.gameObject);
            return;
        }
        soundM = this; 
        DontDestroyOnLoad(this); 
        silencio = !BGM.BGMMenu.EstadoMusica();
        ActualizarBoton();  
    }

    /// Al presionar el boton, dependiendo del estado actual se activan o desactivan los sonidos 
    public void OnButtonPress()
    {
        if(!silencio)
        {
            silencio  = true;
            SFX.SFXMenu.PararSFX(); 
            BGM.BGMMenu.PararMusica(); 
        }
        else
        {
            silencio = false;
            SFX.SFXMenu.ContinuarSFX(); 
            BGM.BGMMenu.ContinuarMusica();
        }
        ActualizarBoton(); 
    } 

    //Modificamos la iamgen del boton dependiendo del estado actual
    private void ActualizarBoton()
    {
        if(!silencio)
        {
            sonido.gameObject.SetActive(true);
            sinSonido.gameObject.SetActive(false);
        }
        else
        {
            sonido.gameObject.SetActive(false);
            sinSonido.gameObject.SetActive(true);
        }
    }
    
}
