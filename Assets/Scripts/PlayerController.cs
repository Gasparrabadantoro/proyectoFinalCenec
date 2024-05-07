using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    public NavMeshAgent miNavMesh;
    public Animator playerAnimator;
    public float inputX;
    public float inputY;
    public float speed;
    Vector3 ultimaDireccion = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        miNavMesh=GetComponent<NavMeshAgent>();
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
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.collider!=null)
            {
                if (hit.collider.tag == "Enemy")
                {
                    print("Le has dado al enemigo");
                }
                else
                {
                    print("Has fallado");
                }
            }
        }
        #endregion

    }
}
