using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;

public class LuzPatioManager : MonoBehaviour
{
    public Light2D lampara;
    private bool activo = false; 
    public Rigidbody2D rb;
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
            if (rb.velocity.x > 0)
            {
                lampara.intensity -= 0.0001f; 
            }

            if (rb.velocity.x < 0)
            {
                lampara.intensity += 0.0001f; 
            }
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
