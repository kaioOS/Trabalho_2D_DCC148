using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float tree_Health;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private int totalWood;
    [SerializeField] private ParticleSystem leafs;
    public void onHit()
    {
        tree_Health--;
 
        anim.SetTrigger("hit");
        leafs.Play();
        
        if(tree_Health == 0) //Árvore destruída
        {
            totalWood = Random.Range(1,5);
            for (int i = 0; i < totalWood; i++)
            {
                //Gera o coletável de madeira
                Instantiate(
                woodPrefab, 
                transform.position + new Vector3(Random.Range(-0.5f,0.5f),
                Random.Range(-0.5f,0.5f),0f), 
                transform.rotation); 
            }
            

            anim.SetTrigger("cut");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Axe"))
        {
            onHit();
        }
    }
}
