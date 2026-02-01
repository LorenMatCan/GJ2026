using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;

public class LampController : MonoBehaviour
{
    public Light2D lampara;
    private bool activo = false; 
    //private bool modificarLuz = true; 

    // Start is called before the first frame update
    void Awake()
    {
        lampara.enabled = false;
    }

    void Update()
    {
        if (activo)
        {
            flicker(); 
        }
    }

    public void flicker()
    {
        int randomInt = Random.Range(0, 2);
        if(randomInt ==1)
        {
            //modificarLuz = false; 
            lampara.intensity = Random.Range(0f, 1f);
            //StartCoroutine(PierdeControl()); 
        }
    }

    private IEnumerator PierdeControl()
    {
        yield return new WaitForSeconds(1);
        //modificarLuz = true;   
    }

     public void TurnOnLight()
    {
        activo = true; 
        lampara.enabled = true;
    }

     public void TurnOffLight()
    {
        activo = false; 
        lampara.enabled = false;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag("Jugador"))
            {
                TurnOnLight();    
            }
    }

    public void OnTriggerExit2D (Collider2D other)
    {
         if (other.CompareTag("Jugador"))
            {
                TurnOffLight();  
            }
    }
}
