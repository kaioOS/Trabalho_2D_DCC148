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

    private Vector2 _direction;
    //Colisão
    private Rigidbody2D rig;
    

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
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
    }
    private void Update()
    {
        onInput();
        onRun(); 
        onRoll();
    }
    
    private void FixedUpdate()
    {
        onMove();   
    }
    #region Movement
        void onInput()
        {
            _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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
                _isRunning = true;
            }
            if(Input.GetKeyUp(KeyCode.LeftShift)) //Verificar se o "shift" esquerdo (botão para correr) parou de ser pressionado
            {
                speed = initialSpeed; //Para voltar para a velocidade inicial
                _isRunning = false;
            }
        }
        void onRoll()
        {
            if(Input.GetMouseButtonDown(1)) //Verifica se o botão direito do mouse (botão para esquiva) foi pressionado
            {
                speed = runSpeed;
                _isRolling = true;
            }
            if(Input.GetMouseButtonUp(1)) //Verifica se o botão direito do mouse (botão para esquiva) foi pressionado
            {
                speed = initialSpeed;
                _isRolling = false;
            }
        }
    #endregion
}
