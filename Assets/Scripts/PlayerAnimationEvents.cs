using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{

    public PlayerController playerReference;
    // Start is called before the first frame update
    void Start()
    {
        playerReference = GetComponentInParent<PlayerController>();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {

        playerReference.SendDamageToEnemy();
       
    }
}
