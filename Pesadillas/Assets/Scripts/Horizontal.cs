using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Qué tan lejos se mueve hacia los lados")]
    public float distancia = 3f; 

    [Tooltip("Qué tan rápido se mueve")]
    public float velocidad = 2f;

    private Vector3 posicionInicial;

    void Start()
    {
      
        posicionInicial = transform.position;
    }

    void Update()
    {
  
        float movimientoX = Mathf.Sin(Time.time * velocidad) * distancia;

   
        transform.position = posicionInicial + new Vector3(movimientoX, 0, 0);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            collision.transform.SetParent(null);
        }
    }

    
}