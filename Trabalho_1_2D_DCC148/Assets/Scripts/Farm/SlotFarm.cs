using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip carrotSFX;
    
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole; 
    [SerializeField] private Sprite carrot; 

    [Header("Settings")]
    [SerializeField] private int digAmount; //O quanto deve cavar até gerar o buraco
    [SerializeField] private bool detecting;
    [SerializeField] private float waterAmount; //Total de água para nascer a cenoura

    private int inicialDigAmount;
    private float currentWater;
    private bool dugHole; //Para verificar se já existe um buraco antes da cenoura
    private bool plantedCarrot; //Para verificar quando tocar o SFX da cenoura
    private bool isTouching; //verdadeiro quando o player está encostando na cenoura
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
            if(currentWater>=waterAmount && !plantedCarrot) //Colocou a quantidade de água necessária
            {
                audioSource.PlayOneShot(holeSFX);
                spriteRenderer.sprite = carrot;

                plantedCarrot = true;                
            }

            if(Input.GetKeyDown(KeyCode.E) && plantedCarrot && isTouching)
            {
                audioSource.PlayOneShot(carrotSFX);
                spriteRenderer.sprite = hole;
                playerItems.carrots++;
                currentWater = 0;
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
        if(collision.CompareTag("Player"))
        {
            isTouching = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Water"))
        {
            detecting = false;
        }
        if(collision.CompareTag("Player"))
        {
            isTouching = false;
        }
    }
}
