using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{   
    public float cooldownDeAtaque = 2;
    public float tiempoDesdeUltimoAtaque = 2;
    public bool contandoElTiempo;
    public Transform enemyTransform;
    public float attackDistance;
    public NavMeshAgent miNavMesh;
    public Animator playerAnimator;
    public Enemy currentEnemy; 
    public float inputX;
    public float inputY;
    public float speed;
    public HealthBarController healthBarControllerJugador;

    public float valorInicialDelay;
    public int contadorVida = 10;
    Vector3 ultimaDireccion = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        miNavMesh = GetComponent<NavMeshAgent>();
        // Para que el contador de tiempo se ajusto a tiempo desde ultimo ataque. 
        valorInicialDelay = tiempoDesdeUltimoAtaque;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!askIfImDeath())
        {

       
        #region movement

        if (tiempoDesdeUltimoAtaque==valorInicialDelay)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");

            miNavMesh.Move(transform.forward * Time.deltaTime * inputY * speed);
            miNavMesh.Move(transform.right * Time.deltaTime * inputX * speed);
        }
       

        if ((inputX > 0.5f || inputX < -0.05f) || (inputY > 0.05f || inputY < -0.05f))
        {
            playerAnimator.SetBool("walking", true);
            ultimaDireccion = new Vector3(inputX, inputY, 0f).normalized;
        }
        else
        {
            playerAnimator.SetBool("walking", false);
        }

        if (ultimaDireccion.x < 0)
        {
            playerAnimator.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (ultimaDireccion.x > 0)
        {
            playerAnimator.transform.localScale = new Vector3(1, 1, 1);
        }
        #endregion

        #region golpeAEnemigo
        if (Input.GetMouseButtonDown(0))
        {
            // Ray=  dispara un rayo de un sitio a otro. Su origen esta en el punto de disparo del "rayo"
            /* y cuyo destino es aquel lugar donde choca "HIT",la clave es que podemos recibir los objetos 
             tocados por HIT. */

            // Esta linea hace un rayo desde la pantalla hasta posicion del ratón. 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Esta linea crea un objeto hit que contiene la distancia del rayo y el objeto con el que ha chocado. 
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Enemy")
                {
                    //aqui con el GetCurrentEnemy nos cercioramos de que estamos 
                    /*asignando la variable currentEnemy con el enemigo al que nos estamos enfrentando, pa que? 
                     Cuando vayas a hacer el metodo atack, no tienes a quien mandarselo, no sabes*/
                    GetCurrentEnemy(hit.collider);
                    // Verificar la distancia entre el jugador y el enemigo
                    float distanciaAlEnemigo = Vector3.Distance(transform.position, enemyTransform.position);

                    if (distanciaAlEnemigo <= attackDistance)
                    {
                        if (tiempoDesdeUltimoAtaque == cooldownDeAtaque)
                        {
                            contandoElTiempo = true;
                            // Reproducir animación de ataque
                            playerAnimator.SetTrigger("attack");

                        }


                    }
                    else
                    {
                        print("Estás demasiado lejos para atacar");
                    }
                }
                else
                {
                    print("Has fallado");
                }
            }
        }
        #endregion
        if (contandoElTiempo)
        {
            tiempoDesdeUltimoAtaque -= Time.deltaTime;
            if (tiempoDesdeUltimoAtaque <= 0)
            {
                contandoElTiempo = false;
                tiempoDesdeUltimoAtaque = cooldownDeAtaque;
            }
        }
        }
    }

    //METOOOODOS

    public void GetCurrentEnemy(Collider2D gameObjectCollider )
    {
        /*Herramienta para cambiar la variable currentEnemy a el enemigo actual*/
        if (gameObjectCollider.gameObject.tag== "Enemy")
        {
            currentEnemy = gameObjectCollider.gameObject.GetComponent<Enemy>();
        }
        //Aqui vamos a pillar el enemigo actual que estamos pegando 

    }
    public void ReceiveDamage()
    {
        playerAnimator.SetTrigger("receiveDamage");
        contadorVida-=1;
        checkDeath();
        healthBarControllerJugador.SetHealth(contadorVida);
    }
    public void checkDeath()
    {
        if (contadorVida<=0)
        {
            playerAnimator.SetBool("imDead",true);
            Destroy(healthBarControllerJugador.transform.parent.gameObject);
        }
    }
    public void SendDamageToEnemy()
    {
        if (currentEnemy != null)
        {
            currentEnemy.ReceiveDamage();
        }
    }

    public bool askIfImDeath()
    {
        return playerAnimator.GetBool("imDead");
    }
}
