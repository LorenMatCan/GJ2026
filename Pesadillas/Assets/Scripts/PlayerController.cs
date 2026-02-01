using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>PlaverController</c> Controla las habilidades del jugador al igual que las máscaras.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float velocidad;
    //Sirve para determinar si estamos en el aire o en una plataforma y asi poder hacer el salto
    [SerializeField] private Transform revisaHayPisoAbajo;
    [SerializeField] private LayerMask suelo;
    [SerializeField] private float poderSalto;
    //El tiempo coyote nos permite hacer que el jugador salte aunque tecnicamente no este en el suelo
    [SerializeField] private float tiempoCoyote;
    private float contadorTiempoCoyote;
    //El buffer de salto sirve para preparar el siguiente aunque no hayamos llegado al suelo
    [SerializeField] private float bufferSaltoTiempo;
    private float contadorBufferSalto;
    //Doble salto 
    private bool dobleSalto;
    //Sirve para saber si el sprite esta viendo hacia la derecha o izuierda 
    //Inicia en verdadero
    private bool viendoDerecha = true;
    private float xInput;
    //Lo necesario para las máscaras 
    [SerializeField] private GameObject[] Fases;
    //El estado actual de nuestro protagonista 0 es normal, 1 es luz, 2 es invisible
    private int state = 0;
    [SerializeField] private float duracionLuz;
    [SerializeField] private float recargaLuz;
    [SerializeField] private float duracionInvisible;
    [SerializeField] private float recargaInvisible;
    private bool estadoLuz = false, estadoInvisible = false, estadoMascara = true;
    public Text  mensajeVida;
    public GameObject MensajeLuz, MensajeInvisible; 
    private float tempVelocidad, tempPoderSalto;
    //Necesario para la vida 
    [SerializeField] public int vida;
    //Relacionaddo con el "flinch" del personaje 
    private bool puedeSerControlado = true;
    [SerializeField]private Vector2 velocidadFlinch;
    [SerializeField]  private float tiempoFlinch;
    //Variable publica para la invisibilidad 
    public bool invisible;
    public event System.Action Caminar; 
    public event System.Action Parar; 
    public AudioClip[] musica;



    //COSAS AGREGADAS POR ALAN
    //Variable para indicar si se murio para manda la pantalla de game ovver
    public bool seMurio = false;
    //Variable para indicar si solo se cayo pero puede reaparecer
    public bool seCayo = false;
    void Awake()
    {
        tempPoderSalto = poderSalto;
        tempVelocidad = velocidad;
    }

    void Update()
    {
        //Movimiento básico de izquierda a derecha 
        xInput = Input.GetAxis("Horizontal");
          if(EstaEnElSuelo()&& xInput !=0)
        {
            Caminar.Invoke();  
        }
        else
        {
            Parar.Invoke(); 
        }

        //Solo aceleramos cuando recibamos un input por partr del usuario
        if (xInput > 0.0f)
            transform.localScale = Vector3.one;

        #region Salto

        if (Input.GetButtonDown("Jump"))
        {
            contadorBufferSalto = bufferSaltoTiempo;
            if (dobleSalto | EstaEnElSuelo() || (contadorTiempoCoyote > 0f && contadorBufferSalto > 0f))
            {
                SFX.SFXMenu.SuenaFXClip(musica[5], rb.transform); 
                rb.velocity = new Vector2(rb.velocity.x, poderSalto);
                dobleSalto = !dobleSalto;
                contadorBufferSalto = 0f;
            }

        }
        else
        {
            contadorBufferSalto -= Time.deltaTime;
        }

        //Mientras mas tiempo se mantenga presinado mas salta
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            contadorTiempoCoyote = 0f;
        }

        //Restablecemos la variable para permitir en un futuro el doble salto
        if (EstaEnElSuelo() && !Input.GetButtonDown("Jump"))
            dobleSalto = !dobleSalto;

        //Implementación de Coyote time
        if (EstaEnElSuelo())
        {
            contadorTiempoCoyote = tiempoCoyote;
        }
        else
        {
            contadorTiempoCoyote -= Time.deltaTime;
        }
        #endregion
        Flip();

        //Necesarias para usar las máscaras 
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mascaraLuz();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mascaraInvisible();
        }
    }
    #region Movimiento en general 
    void FixedUpdate()
    {
        if (puedeSerControlado)
        {
            rb.velocity = new Vector2(xInput * velocidad, rb.velocity.y);
        }

    }
    
    /// <summary>
    /// Hace que el jugador reaccione a los ataques/golpes
    /// </summary> 
    /// 
    private void Flinch(Vector2 colision)
    {
        if (colision.y == 1)
        {
            // Destroy(gameObject);
            //continue;
        }
        else
        {
            rb.velocity = new Vector2(velocidadFlinch.x * colision.x, velocidadFlinch.y);
        }

    }

    #endregion
    
    /// <summary>
    /// Crea una esfera en la parte inferior del personaje.Nos permite saber si estamos en el aire o en una plataforma
    /// </summary>    
    private bool EstaEnElSuelo()
    {
        return Physics2D.OverlapCircle(revisaHayPisoAbajo.position, 0.2f, suelo);
    }
    
    /// <summary>
    /// Voltea el asset del personaje dependiendo de en que dirección esta viendo 
    /// </summary>  
    private void Flip()
    {
        if (viendoDerecha && xInput < 0f || !viendoDerecha && xInput > 0f)
        {
            //"volteamos" los valores de acuerdo a la nueva dirección del jugador 
            viendoDerecha = !viendoDerecha;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    #region Máscaras/Habilidades.

    /// <summary>
    /// Modifica el sprite acorde a que estado se este usando actualmente
    /// </summary> 
    private void ActivarEstado()
    {
        for (int i = 0; i < Fases.Length; i++)
        {
            if (i == state)
            {
                Fases[i].SetActive(true);
            }
            else
            {
                Fases[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// Aplica lo realcionado a la luz Se apoya de una biblioteca externa
    /// </summary> 
    private void mascaraLuz()
    {
        if (estadoLuz && estadoMascara)
        {
            SFX.SFXMenu.SuenaFXClip(musica[3], rb.transform); 
            estadoMascara = false;
            estadoLuz = false;
            state = 1;
            ActivarEstado();
            StartCoroutine(UsarLuz());
        }
    }
    /// <summary>
    /// Cambia a otro spirte y alentiza el movimiento
    /// </summary> 
    private void mascaraInvisible()
    {
        if (estadoInvisible && estadoMascara)
        {
            SFX.SFXMenu.SuenaFXClip(musica[2], rb.transform); 
            estadoMascara = false;
            estadoInvisible = false;
            state = 2;
            ActivarEstado();
            StartCoroutine(UsarInvisible());
        }
    }

    /// <summary>
    /// Alenta los movimientos del jugador
    /// </summary>
    private void efectosInvisibilidad()
    {
        //Se almacenan para, una vez terminado se puedan volver a usar
        invisible = true; 
        tempVelocidad = velocidad;
        tempPoderSalto = poderSalto;
        velocidad = velocidad / 2;
        poderSalto = poderSalto / 2;
    }

    /// <summary>
    /// Deshace los efectos de las dos máscaras anteriores
    /// </summary> 
    private void regresarAFaseNormal()
    {
        velocidad = tempVelocidad;
        poderSalto = tempPoderSalto;
        estadoMascara = true;
        invisible = false; 
        state = 0;
        ActivarEstado();
    }


    /// <summary>
    /// Activa la habilidad y el cronometro de su uso. Igualmetne cuenta hasta que esta se pueda vlver a usar
    /// </summary> 
    private IEnumerator UsarLuz()
    {
        MensajeLuz.SetActive(true); 
        yield return new WaitForSeconds(duracionLuz);
        regresarAFaseNormal();
        MensajeLuz.SetActive(false); 
        yield return new WaitForSeconds(recargaLuz);
        MensajeLuz.SetActive(true); 
        estadoLuz = true;
    }

    private IEnumerator UsarInvisible()
    {
        MensajeInvisible.SetActive(true); 
        efectosInvisibilidad();
        yield return new WaitForSeconds(duracionInvisible);
        regresarAFaseNormal();
        MensajeInvisible.SetActive(false);
        ActivarEstado();
        yield return new WaitForSeconds(recargaInvisible);
        MensajeInvisible.SetActive(true);
        estadoInvisible = true;
    }
    #endregion

    #region Interacción con el mundo 


    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Se hace el daño siempre y cuando no este usando uno de las mascaras
        if (collision.gameObject.tag == "Enemigo" && !invisible)
        {

            PerderVida(1, collision.GetContact(0).normal);

        }

        if (collision.gameObject.tag == "Picos")
        {

            PerderVida(1, collision.GetContact(0).normal);

        }
        if (collision.gameObject.tag == "MLuz")
        {
            SFX.SFXMenu.SuenaFXClip(musica[1], rb.transform);
            estadoLuz = true;
            MensajeLuz.SetActive(true);
            Destroy(collision.gameObject);  

        }
        if (collision.gameObject.tag == "MInvi")
        {
            SFX.SFXMenu.SuenaFXClip(musica[1], rb.transform);
            estadoInvisible = true;
            MensajeInvisible.SetActive(true); 
            Destroy(collision.gameObject);

        }

    }

    #endregion

    #region  Todo lo relacionado a la vida

    /// <summary>
    /// Disminuye la vida del jugador 
    /// </summary> 
    public void PerderVida(int dano, Vector2 posicion)
    {
        if (vida > 0)
        {
            SFX.SFXMenu.SuenaFXClip(musica[0], rb.transform); 
            vida -= dano;
            //Para que funcione bien el jugador debe de dejar de aplicar fuerza al objeto
            StartCoroutine(PierdeControl());
            Flinch(posicion);
            mensajeVida.text = vida.ToString();
        }
        else
        {
            Morir();
        }

    }
    
    /// <summary>
    /// Alza y baja la bandera respectivamente cuando el jugador pierde el control 
    /// y cuando lo recupera
    /// </summary> 
    private IEnumerator PierdeControl()
    {
        puedeSerControlado = false;
        yield return new WaitForSeconds(tiempoFlinch);
        puedeSerControlado = true;
    }

    /// <summary>
    /// Termina el juego 
    /// </summary> 
    private void Morir()
    {
        SFX.SFXMenu.SuenaFXClip(musica[4], rb.transform); 
        mensajeVida.text = "Ha muerto";
        //Destroy(gameObject);

        if (seMurio) return;
        seMurio = true;
        GameController.Instance.PlayerDied();

    }

    //Usado en el checkpoint
    public void ResetearVelocidad()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    //Este es solo para cuando pierda vida por caer en una plataforma que lo lleva a otro
    public void PerderVidaRespawn(int dano)
    {
        if (vida > 0)
        {
            SFX.SFXMenu.SuenaFXClip(musica[0], rb.transform); 
            vida -= dano;
           // vida = vida + 1;
            //Para que funcione bien el jugador debe de dejar de aplicar fuerza al objeto
          //  StartCoroutine(PierdeControl());
            //Flinch(posicion);
            mensajeVida.text = vida.ToString();
        }
        else
        {
            Morir();
        }

    }

    #endregion

}

