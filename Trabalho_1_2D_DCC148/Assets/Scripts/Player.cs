using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Varáveis relacionadas à movimentação do personagem
    public float speed;
    public float runSpeed;
    private float initialSpeed;

    private bool _isRunning;
    private bool _isRolling;
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;

    private Vector2 _direction;
    private int handlingObj;
    //Colisão
    private Rigidbody2D rig;
    private PlayerItems playerItems;
    

    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }
    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }
    public bool isCutting
    {
        get { return _isCutting; }
        set { _isCutting = value; }
    }
    public bool isDigging
    {
        get { return _isDigging; }
        set { _isDigging = value; }
    }
    public bool isWatering
    {
        get { return _isWatering; }
        set { _isWatering = value; }
    }
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerItems = GetComponent<PlayerItems>();
        initialSpeed = speed;
    }
    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            handlingObj = 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            handlingObj = 2;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            handlingObj = 3;
        }
        onInput();
        onRun(); 
        onRoll();
        if (handlingObj == 1)
        {
            onCut();
        }
        else if (handlingObj == 2)
        {
            onDig();
        }
        else if (handlingObj == 3)
        {
            onWater();
        }
    }
    
    private void FixedUpdate()
    {
        onMove();   
    }

    #region Movement
        void onInput()
        {
            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        void onMove()
        {
            rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime );
        }

        void onRun()
        {
            if(Input.GetKeyDown(KeyCode.LeftShift)) //Verifica se o "shift" esquerdo (botão para correr) foi pressionado
            {
                speed = runSpeed;
                isRunning = true;
            }
            if(Input.GetKeyUp(KeyCode.LeftShift)) //Verificar se o "shift" esquerdo (botão para correr) parou de ser pressionado
            {
                speed = initialSpeed; //Para voltar para a velocidade inicial
                isRunning = false;
            }
        }
        void onRoll()
        {
            if(Input.GetMouseButtonDown(1)) //Verifica se o botão direito do mouse (botão para esquiva) foi pressionado
            {
                speed = runSpeed;
                isRolling = true;
            }
            if(Input.GetMouseButtonUp(1)) 
            {
                speed = initialSpeed;
                isRolling = false;
            }
        }
    #endregion

    #region Action
        void onCut()
        {
            if(Input.GetMouseButtonDown(0)) //Verifica se o botão esquedo do mouse (botão para ação) foi pressionado
            {
                isCutting = true;
                speed = 0f;
            }
            if(Input.GetMouseButtonUp(0)) 
            {
                isCutting = false;
                speed = initialSpeed;
            }
        }
        void onDig()
        {
            if(Input.GetMouseButtonDown(0)) //Verifica se o botão esquedo do mouse (botão para ação) foi pressionado
            {
                isDigging = true;
                speed = 0f;
            } 
            if(Input.GetMouseButtonUp(0)) 
            {
                isDigging = false;
                speed = initialSpeed;
            }
        }
        void onWater()
        {
            if(Input.GetMouseButtonDown(0) && playerItems.currentWater > 0) //Verifica se o botão esquedo do mouse (botão para ação) foi pressionado
            {
                isWatering = true;          
                speed = 0f;
            }
             
            if(Input.GetMouseButtonUp(0) || playerItems.currentWater <= 0) //Parar de jogar água quando acabar ou parar de clicar o mouse
            {
                isWatering = false;
                speed = initialSpeed;
                if(playerItems.currentWater<0)
                    playerItems.currentWater = 0;
            }
            if(isWatering)
            {
                playerItems.currentWater-= 0.1f;
            }     
        }
    #endregion
}
