using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Varáveis relacionadas à movimentação do personagem
    public float speed;
    public float runSpeed;
    private float initialSpeed;
    public float healthBar = 10;
    public float initialHealth;
    public bool isPaused;
    private bool _isRunning;
    private bool _isRolling;
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;
    private bool gamePaused = false;

    private Vector2 _direction;
    [HideInInspector] public int handlingObj;

    //Colisão
    private Rigidbody2D rig;
    private PlayerItems playerItems;
    [SerializeField] private Canvas canvas;   

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
        initialHealth = healthBar;
        canvas.enabled = false;
    }
    private void Update()
    {
        if(!isPaused){
            
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                handlingObj = 0;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                handlingObj = 1;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                handlingObj = 2;
            }

            if(!canvas.enabled){
                onInput();
                onRun(); 
                onRoll();
                onCut();
                onDig();
                onWater();

            }

            if (gamePaused)
            {
                Time.timeScale = 0; // Pausa a cena
            }
            else
            {
                Time.timeScale = 1; // Despausa a cena
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                gamePaused = !gamePaused;
                canvas.enabled = !canvas.enabled;
                                     
            }
        }
    }
    
    private void FixedUpdate()
    {
        if(!isPaused)
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
            if (handlingObj == 0){
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
            else
                isCutting = false;
        }
        void onDig()
        {
            if (handlingObj == 1){
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
            else
                isDigging = false;
        }
        void onWater()
        {
            if (handlingObj == 2){
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
            else
                isWatering = false;  
        }
    #endregion
    public void isDead()
    {
        isPaused = true;
        healthBar = initialHealth;
        playerItems.currentWood = 0;
        playerItems.currentWater = 0;
        playerItems.carrots = 0;
        playerItems.fishes = 0; 
        isPaused = false;  
    }
}