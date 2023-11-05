using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{   
    [Header("Amounts")]
    [SerializeField] private int woodAmount;
    [SerializeField] private float timeAmount;

    [Header("Components")]
    [SerializeField] private GameObject houseCollider;
    [SerializeField] private SpriteRenderer houseSprite;
    [SerializeField] private Transform point; //move o player para uma posição determinada ao iniciar a construção p/ evitar que a casa seja construída em cima do player
    [SerializeField] private Color startColor; 
    [SerializeField] private Color endColor;
    

    private bool detectingPlayer;
    private Player player;
    private PlayerAnim playerAnim;
    private PlayerItems playerItems;

    private float timeCount;
    private bool isBegining;

    void Start()
    {
        player = FindObjectOfType<Player>();
        playerAnim = player.GetComponent<PlayerAnim>();
        playerItems = player.GetComponent<PlayerItems>();
    }

    void Update()
    {
        if(detectingPlayer && Input.GetKeyDown(KeyCode.E) && playerItems.currentWood >= woodAmount)
        {
            //inicia a construção da casa
            player.transform.eulerAngles = new Vector2(0, 0);
            isBegining = true;
            playerAnim.OnHammeringStarted();
            houseSprite.color = startColor;
            player.transform.position = point.position;
            player.isPaused = true;
            playerItems.currentWood -= woodAmount;

        }

        if(isBegining){
            timeCount += Time.deltaTime;

            if(timeCount >= timeAmount){
                //finaliza a construção da casa
                playerAnim.OnHammeringEnded();
                houseSprite.color = endColor;
                player.isPaused = false;
                isBegining = false;
                houseCollider.SetActive(true);
            }
        }

    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
