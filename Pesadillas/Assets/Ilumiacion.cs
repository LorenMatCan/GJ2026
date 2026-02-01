using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;

public class Ilumiacion : MonoBehaviour
{
    public GameObject lampara;
   
    public void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag("Jugador"))
            {
                 lampara.SetActive(true);  
            }
    }

}
