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
    public float inputX;
    public float inputY;
    public float speed;
    Vector3 ultimaDireccion = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        miNavMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        #region movement
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        miNavMesh.Move(transform.forward * Time.deltaTime * inputY * speed);
        miNavMesh.Move(transform.right * Time.deltaTime * inputX * speed);

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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Enemy")
                {
                    // Verificar la distancia entre el jugador y el enemigo
                    float distanciaAlEnemigo = Vector3.Distance(transform.position, enemyTransform.position);

                    if (distanciaAlEnemigo <= attackDistance)
                    {
                        if (tiempoDesdeUltimoAtaque == cooldownDeAtaque)
                        {
                            contandoElTiempo = true;
                            // Reproducir animaci�n de ataque
                            playerAnimator.SetTrigger("attack");

                        }


                    }
                    else
                    {
                        print("Est�s demasiado lejos para atacar");
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

    //METOOOODOS

    public void ReceiveAttack()
    {
        playerAnimator.SetTrigger("receiveDamage");
    }
}
