using UnityEngine;
using UnityEngine.AI;

public class Transporter : MonoBehaviour
{
    public Transform targetPosition; // La posición de destino a la que quieres transportar al jugador
    private NavMeshAgent playerNavMeshAgent; // El NavMeshAgent del jugador
    public bool needRombo = false;

    private void Start()
    {
        // Encuentra el NavMeshAgent del jugador
        playerNavMeshAgent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
    }

    private void OnMouseDown()
    {
        if (!needRombo)
        {
            if (playerNavMeshAgent != null && targetPosition != null)
            {
                // Mover al jugador a la posición de destino usando NavMesh
                playerNavMeshAgent.Warp(targetPosition.position);
            }
        }
        else
        {
            if (playerNavMeshAgent != null && targetPosition != null && FindObjectOfType<PlayerController>().romboEquipado)
            {
                // Mover al jugador a la posición de destino usando NavMesh
                playerNavMeshAgent.Warp(targetPosition.position);

                FindObjectOfType<PlayerController>().romboEquipado=false;

                FindObjectOfType<CursorManager>().ChangeCursor(0,false);

                // poner luego transiciones
            }
        }
        
    }
}
