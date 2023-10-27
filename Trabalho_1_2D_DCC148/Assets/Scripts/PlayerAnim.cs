using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Player player;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        onMove();
        onRun();
        onCut();
        onDig();
    }
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
            if(player.isRunning)
            {
                anim.SetInteger("transition",2);
            }
        }
        void onRoll()
        {
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
    #endregion
}
