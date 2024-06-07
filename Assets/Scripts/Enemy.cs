using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float attackRange;
    public float detectionRange;
    public bool persiguiendo;
    public Transform playerTransform; // se arrastra desde el gameobject de fuera
    public NavMeshAgent navMeshReferencia;
    public bool vigilando;
    public bool contandoElTiempo;
    public float cooldownDeAtaque = 2;
    public float tiempoDesdeUltimoAtaque = 2;
    public float scaleX;
    public Animator enemyAnimator;
    public int contadorVida =10;
    public HealthBarController healthBarController;
    public BoxCollider colliderHead;
    public BoxCollider colliderBody;

    public void Awake()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        navMeshReferencia = GetComponent<NavMeshAgent>();
        navMeshReferencia.speed = speed;

        OpenEyes();

        scaleX = transform.localScale.x;


    }

    private void EnemieCheckDirection()
    {
     

        Vector3 distanciaAbsoluta = transform.position - playerTransform.position;

        if(distanciaAbsoluta.x < 0)
        {
            transform.localScale = new Vector3(-scaleX,transform.localScale.y,transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Update()
    {
        // Si esl jugador esta vivo... hazme todo esto 
        if (!playerTransform.GetComponent<PlayerController>().askIfImDeath())
        {

        
        EnemieCheckDirection();
        if (vigilando) //Estamos detectando  !!!
        {
            if (Vector3.Distance(transform.position, playerTransform.position) <= detectionRange) //dime si estoy en rango
            {
                StartFollowingPlayer();
            }
        }
        if (persiguiendo)
        {
            navMeshReferencia.SetDestination(playerTransform.position);
        }
        AttackDetection();

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

    //METODOS. HACER REGION 
    public void SendDamageToPlayer()
    {
        playerTransform.gameObject.GetComponent<PlayerController>().ReceiveDamage();
        print("Damage send to player ");
    }
    public bool IsCloseToAttack()
    {
        var dist = Vector3.Distance(transform.position, playerTransform.position);
        Debug.Log("distanceToAttack: " + dist);
        return dist < attackRange;
    }

    public void OpenEyes()
    {
        vigilando = true;
    }

    public void CloseEyes()
    {
        vigilando = false;
    }


    public void StartFollowingPlayer()
    {
        navMeshReferencia.SetDestination(playerTransform.position); // Usa el navmeshAgent para ordenar al enemigo que vaya hacia el player
        vigilando = false;
        persiguiendo = true;
        navMeshReferencia.isStopped = false;
        enemyAnimator.SetBool("isWalking",true);
        enemyAnimator.SetBool("isAttacking", false);
    }

    public void StopAttack()
    {
        enemyAnimator.SetBool("isAttacking", false);
    }
    public void StopFollowingPlayer()
    {
        persiguiendo = false;
        navMeshReferencia.isStopped = true;
        enemyAnimator.SetBool("isWalking", false);
    }

    public void AttackDetection()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            if (tiempoDesdeUltimoAtaque == cooldownDeAtaque) // dime si es cierto que NO  estoy en cooldown
            {
                //Si no estoy en cooldown
                StopFollowingPlayer();
                contandoElTiempo = true;
               
                enemyAnimator.SetBool("isAttacking",true);
               
                Invoke(nameof(StartFollowingPlayer), 2);
                Invoke(nameof(StopAttack),1);
            }
        }
    }

    public void ReceiveDamage(bool headDamage)
    {
        enemyAnimator.SetTrigger("receiveDamage");

        contadorVida -= 1;
        checkDeath(headDamage);
        healthBarController.SetHealth(contadorVida);
    }

    public void checkDeath(bool headDamage)
    {
        if (contadorVida <= 0)
        {
            if (headDamage)
            {
                /*ANIMACION MUERTE CABEZA*/
            }
            else
            {
                /*ANIMACION MUERTE Cuerpo*/
            }
            enemyAnimator.SetBool("imDead", true);
            Destroy(healthBarController.transform.parent.gameObject);
            // Todo lo que tiene la imagen, se queda huerfano
            enemyAnimator.transform.parent = null;
            // te cercioras de que el eenemigo este muerto, y no puedas pinchar ni nada por el estilo 
            Destroy(this.gameObject);

        }
    }

}
