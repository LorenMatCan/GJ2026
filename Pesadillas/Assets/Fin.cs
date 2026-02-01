using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fin : MonoBehaviour
{
    public GameObject fin;
    public GameObject personaje; 
   
    public void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag("Jugador"))
            { 
                Time.timeScale = 0f;
                fin.SetActive(true);
                Destroy(personaje);
            }
    }
}
