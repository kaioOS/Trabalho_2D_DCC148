using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole; 
    [SerializeField] private Sprite carrot; 

    [SerializeField] private int digAmount; //O quanto deve cavar até gerar o buraco
    [SerializeField] private bool detecting;
    [SerializeField] private bool dugHole; //Para verificar se já existe um buraco antes da cenoura
    [SerializeField] private float waterAmount; //Total de água para nascer a cenoura
    private int inicialDigAmount;
    private float currentWater;
    PlayerItems playerItems;
    private void Start()
    {
        inicialDigAmount =  digAmount;
        playerItems = FindObjectOfType<PlayerItems>(); 
    }
    private void Update()
    {
        if(dugHole)
        {
            if(detecting)
            {
                currentWater += 0.01f;
            }
            if(currentWater>=waterAmount) //Colocou a quantidade de água necessária
            {
                spriteRenderer.sprite = carrot;
                if(Input.GetKeyDown(KeyCode.E))
                {
                    spriteRenderer.sprite = hole;
                    playerItems.carrots++;
                    currentWater = 0;
                }
            }
        }
    }
    public void onHit()
    {
        digAmount--;
 
        if(digAmount <= inicialDigAmount/2) //Gera o buraco
        {
            spriteRenderer.sprite = hole;
            dugHole = true;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Shovel"))
        {
            onHit();
        }
        if(collision.CompareTag("Water"))
        {
            detecting = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Water"))
        {
            detecting = false;
        }
    }
}
