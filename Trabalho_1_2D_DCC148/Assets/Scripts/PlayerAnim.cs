using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    private Player player;
    private Animator anim;
    private Casting cast;
    private bool isHitting;
    private float timeCount;
    private float recoveryTime = 1f;


    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        cast = FindObjectOfType<Casting>();
    }

    void Update()
    {
        onMove();
        onRun();
        onCut();
        onDig();
        onWater();
        
        if(isHitting)
        {
            timeCount += Time.deltaTime;
            if(timeCount >= recoveryTime)
            {
                player.healthBar--;
                isHitting = false;
                timeCount = 0f;
            }
            if(player.healthBar <= 0)
            {
                anim.SetTrigger("isDead");
                player.isDead();   
            }
        }
    }

    #region Attack

    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if(hit != null) //ataca o inimigo
        {
            hit.GetComponentInChildren<AnimationControl>().OnHit();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

    #endregion

    #region Movement
        void onMove()
        {
            if(player.direction.sqrMagnitude  > 0)
            {
                if(player.isRolling)
                {
                    onRoll();
                }
                else
                {
                    onWalk();
                }
            }
            else 
            {
                onIdle();
            }
            if(player.direction.x > 0)
            {
                transform.eulerAngles = new Vector2(0,0);
            }
            if(player.direction.x < 0)
            {
                transform.eulerAngles = new Vector2(0,180); //Rotaciona o personagem para a esquerda
            }
        }
        void onIdle()
        {
            anim.SetInteger("transition", 0);
        }
        void onWalk()
        {
            anim.SetInteger("transition",1);
        }
        void onRun()
        {
            if(player.isRunning && player.direction.sqrMagnitude > 0)
            {
                anim.SetInteger("transition",2);
            }
        }
        void onRoll()
        {
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("rolling")) //impede que execute uma animação por cima da outra
                anim.SetTrigger("isRolling");
        }
    #endregion
    #region Action
        void onCut()
        {
            if(player.isCutting)
            {
                anim.SetInteger("transition",3);
            }
        }
        void onDig()
        {
            if(player.isDigging)
            {
                anim.SetInteger("transition",4);
            }
        }
        void onWater()
        {
            if(player.isWatering)
            {
                anim.SetInteger("transition",5);
            }
        }
    #endregion

    //chamado quando o jogador pressiona o botão Q na área de pesca
    public void OnCastingStarted()
    {
        anim.SetTrigger("isCasting");
        player.isPaused = true;
    }

    //chamado quando termina a animação de pescaria
    public void OnCastingEnded()
    {
        cast.OnCasting();
        player.isPaused = false;
    }

    public void OnHammeringStarted()
    {
        anim.SetBool("isHammering", true);
    }

    public void OnHammeringEnded()
    {
        anim.SetBool("isHammering", false);
    }

    public void OnHit()
    {
        if(!isHitting)
        {
            anim.SetTrigger("hit");
            isHitting = true;
        }
    }
}
