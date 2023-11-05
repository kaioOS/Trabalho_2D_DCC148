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
    
    void Start()
    {
        currentHealth = totalHealth;
        player = FindObjectOfType<Player>();        
    }

    void Update()
    {   
        if(!isDead)
        {
            if(Vector2.Distance(transform.position, player.transform.position) <= 1.5f) animControl.PlayAnim(2); //distancia mínima para atacar
            else animControl.PlayAnim(1);

            //rotaciona o inimigo
            float posX = player.transform.position.x - transform.position.x;

            if(posX > 0) {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else transform.eulerAngles = new Vector2(0, 180); 
        }

    }
}
