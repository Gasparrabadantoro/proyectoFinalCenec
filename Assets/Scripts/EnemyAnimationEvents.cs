using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{// creamos una referencia a Enemy

    public Enemy thisEnemy;

  
    void Start()
    {/*Como este objeto siempre es hijo de un objeto que contiene el componente Enemy, lo asignamos desde aqui directamente*/
        thisEnemy = GetComponentInParent<Enemy>();
        //Lo mismo que getComponente pero con el padre, en este caso Enemy. Asi todos los Enemy`s 
    }
    public void Attack()
    {
        if (thisEnemy.IsCloseToAttack())
        {
            thisEnemy.SendDamageToPlayer();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
