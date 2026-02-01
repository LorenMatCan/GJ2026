using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaSubeBaja: MonoBehaviour
{
    [Header("Las Plataformas")]
    public Transform plataforma1; 
    public Transform plataforma2; 

    [Header("Los Puntos de Altura ")]
    public Transform puntoArriba; 
    public Transform puntoAbajo;  

    [Header("Configuración")]
    public float velocidad = 3f;

    private bool subiendo = true;

    void Start()
    {
       
        plataforma1.position = new Vector3(plataforma1.position.x, puntoAbajo.position.y, plataforma1.position.z);
        plataforma2.position = new Vector3(plataforma2.position.x, puntoArriba.position.y, plataforma2.position.z);
    }

    void Update()
    {
        if (subiendo)
        {
  
            MoverSoloAltura(plataforma1, puntoArriba.position.y);
            MoverSoloAltura(plataforma2, puntoAbajo.position.y);

            
            if (Mathf.Abs(plataforma1.position.y - puntoArriba.position.y) < 0.1f)
            {
                subiendo = false;
            }
        }
        else
        {
            
            MoverSoloAltura(plataforma1, puntoAbajo.position.y);
            MoverSoloAltura(plataforma2, puntoArriba.position.y);

            if (Mathf.Abs(plataforma1.position.y - puntoAbajo.position.y) < 0.1f)
            {
                subiendo = true;
            }
        }
    }

    
    void MoverSoloAltura(Transform plataforma, float alturaObjetivo)
    {
       
        Vector3 destino = new Vector3(plataforma.position.x, alturaObjetivo, plataforma.position.z);

        
        plataforma.position = Vector3.MoveTowards(plataforma.position, destino, velocidad * Time.deltaTime);
    }
}