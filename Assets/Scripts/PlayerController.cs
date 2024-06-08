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
    public CursorManager cursorManagerReference;
    public bool romboEquipado = false;

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if (hit.collider != null && hit.collider.GetComponent<TextBasedInteractionPoint>() ) 
        {
     
            if (hit.collider.GetComponent<TextBasedInteractionPoint>() && !romboEquipado)
            {
                cursorManagerReference.ChangeCursor(1,true);
            }
            else if(!romboEquipado)
            {
                cursorManagerReference.ChangeCursor(0, false);
            }     
        }
        if (hit.collider == null && !romboEquipado)
        {
            cursorManagerReference.ChangeCursor(0, false);
        }
        /*Primero, se verifica si el
         * personaje no está muerto llamando 
         * a una función askIfImDeath(). 
         * Si el personaje está muerto,
         * el resto del código dentro de Update no se ejecutará.*/
        if (!askIfImDeath())
        {


            #region movement
            /*Se verifica si 
             * tiempoDesdeUltimoAtaque es igual a valorInicialDelay. 
             * asegura que el personaje no se mueva si está en un   ESTADO DE ATAQUE  o acaba de atacar.*/
            if (tiempoDesdeUltimoAtaque==valorInicialDelay)
        {

                /*Una vez hecho esto vamos a declarar 2 variables inputX e imput y 
                 que corresponde a los ejes Horizontal (inputX ) y Vertical(inputY)
                Input.GetAxisRaw devuelve valores discretos de -1, 0 o 1,
                lo que indica la dirección de la entrada sin suavizado.
                Lo mismo pasa con el Vertical. */
                inputX = Input.GetAxisRaw("Horizontal");
                inputY = Input.GetAxisRaw("Vertical");

                /*Movimiento de personaje, esto es un componente del propio NavMesh llamado .Move, que hace
                 que el personaje se mueva y que tiene por componente el transform del propio personaje, un Time.detlTime
                 que es el  tiempo que ha pasado desde el último frame. 
                Se utiliza para asegurar que el movimiento sea independiente de la tasa de frames.
                
                 speed que es la variable que se encarga de determinar la velocidad de nuestro personaje. */
                miNavMesh.Move(transform.forward * Time.deltaTime * inputY * speed);
            miNavMesh.Move(transform.right * Time.deltaTime * inputX * speed);
        }
       

        if ((inputX > 0.5f || inputX < -0.05f) || (inputY > 0.05f || inputY < -0.05f))
        {
            playerAnimator.SetBool("walking", true);
            ultimaDireccion = new Vector3(inputX, inputY, 0f).normalized;
                // Buscar normalized 
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
       

            if (hit.collider != null)
            {
                    print("HIT COLIDER ES DISTINTO DE NULL  "+hit.collider.gameObject.name);
                if (hit.collider.tag == "Enemy")
                    {   
                        print("HE ENTRADO EN ENEMY COLLIDER ");
                        //aqui con el GetCurrentEnemy nos cercioramos de que estamos 
                        /*asignando la variable currentEnemy con el enemigo al que nos estamos enfrentando, pa que? 
                         Cuando vayas a hacer el metodo atack, no tienes a quien mandarselo, no sabes*/
                        currentEnemy =hit.collider.transform.parent.GetComponent<Enemy>();
                        enemyTransform = currentEnemy.transform;
                        // Verificar la distancia entre el jugador y el enemigo
                   
                       
                    float distanciaAlEnemigo = Vector3.Distance(transform.position, enemyTransform.position);

                    if (distanciaAlEnemigo <= attackDistance)
                    {
                        if (tiempoDesdeUltimoAtaque == cooldownDeAtaque)
                        {
                            contandoElTiempo = true;
                                print("ESTAMOSAQUI");
                                // Reproducir animación de ataque
                                if (hit.collider == currentEnemy.colliderBody)
                                {
                                    print("hasta aqui llega ataque body");
                                    playerAnimator.SetTrigger("attackBody");
                                }
                                else if (hit.collider == currentEnemy.colliderHead)
                                {
                                    print("hasta aqui llega ataque cabeza");
                                    playerAnimator.SetTrigger("attackHead");
                                    
                                }
                            // AQUI- HAY QUE CAMBIAR PARA QUE HAYA DOS SET TRIGGER

                             // 1 ATAQUE CABEZA 2 ATAQUE CUERPO. 
                        }


                    }
                    else
                    {
                        print("Estás demasiado lejos para atacar");
                    }
                }

                    if (hit.collider.tag == "Item")
                    {
                        hit.collider.GetComponent<Item>().PickUpThisObject();
                    }
                    if (hit.collider.tag == "TextBasedInteractionPoint")
                    {
                        print("Ha tocado en la ventana");
                        hit.collider.GetComponent<TextBasedInteractionPoint>().ClickOnInteractionPoint();
                    }
                    if (hit.collider.tag == "PuzzleTrigger")
                    {
                        hit.collider.GetComponent<PuzzleInteractionPoint>().CallPuzzle();
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

    public void GetCurrentEnemy(Collider gameObjectCollider )
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
    public void SendDamageToEnemy(bool headDamage)
    {
        if (currentEnemy != null)
        {
            currentEnemy.ReceiveDamage(headDamage);
        }
    }

    public bool askIfImDeath()
    {
        return playerAnimator.GetBool("imDead");
    }
}
