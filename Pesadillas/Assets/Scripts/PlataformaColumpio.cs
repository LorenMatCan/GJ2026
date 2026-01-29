using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaColumpio: MonoBehaviour
{
    [Header("Configuración del Movimiento")]
    [Tooltip("Qué tan ancho se mueve hacia los lados")]
    public float anchura = 5f; 

    [Tooltip("Qué tan alto sube en las puntas")]
    public float altura = 3f; 

    [Tooltip("Velocidad del recorrido")]
    public float velocidad = 2f;

    private Vector3 centroInicial;

    void Start()
    {
        
        centroInicial = transform.position;
    }

    void Update()
    {
       
        float ciclo = Mathf.Sin(Time.time * velocidad);
        
        float x = ciclo * anchura;

       
        float y = (ciclo * ciclo) * altura;

     
        transform.position = centroInicial + new Vector3(x, y, 0);
    }

    // IMPORTANTE: Para que el personaje no se resbale 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador")) // O "Player"
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

    // DIBUJO DE LA RUTA EN EL EDITOR (Para que veas el camino antes de jugar)
    private void OnDrawGizmos()
    {
        // Esto dibuja puntitos verdes mostrando el camino de la U
        Gizmos.color = Color.green;
        Vector3 centro = Application.isPlaying ? centroInicial : transform.position;
        
        for (float i = -1; i <= 1; i += 0.1f)
        {
            float x = i * anchura;
            float y = (i * i) * altura;
            Vector3 punto = centro + new Vector3(x, y, 0);
            Gizmos.DrawSphere(punto, 0.1f);
        }
    }
}