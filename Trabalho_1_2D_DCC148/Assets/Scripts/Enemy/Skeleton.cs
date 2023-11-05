using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton: MonoBehaviour
{
    [Header("Stats")]
    public float totalHealth;
    public float currentHealth;
    public Image healthBar;
    public bool isDead;

    [Header("Components")]
    [SerializeField] private AnimationControl animControl;

    private Player player;
    public float movementSpeed = 2.0f;
    void Start()
    {
        currentHealth = totalHealth;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if(!isDead)
        {
            Vector2 playerPosition = player.transform.position;
            Vector2 skeletonPosition = transform.position;
            Vector2 moveDirection = (playerPosition - skeletonPosition).normalized;
            float distanceToPlayer = Vector2.Distance(playerPosition, skeletonPosition);
            if(Vector2.Distance(transform.position, player.transform.position) <= 1.25f)
            {
                animControl.PlayAnim(2); //distancia mínima para atacar
            } 
            else 
            {
                animControl.PlayAnim(1);
                
                if(playerPosition.x > skeletonPosition.x)  
                    transform.Translate(moveDirection * movementSpeed * Time.deltaTime);
                else
                {
                    moveDirection.x = -moveDirection.x;
                    transform.Translate(moveDirection * movementSpeed * Time.deltaTime);
                }
            }
            //rotaciona o inimigo
            float posX = player.transform.position.x - transform.position.x;

            if(posX > 0) {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else transform.eulerAngles = new Vector2(0, 180); 
        }

        
    }
    
}