using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCouleurManager : MonoBehaviour
{
    GameObject[] player;

   
    void Start()
    {
    	player = InitTabController.initTabController.player;
        ChangeSpriteRendererColorAfterDelay(player, ChooseNewColor("bleu"), 10f);
    }



    void Update()
    {
 
        
    }

    
    void ChangeSpriteRendererColorAfterDelay(GameObject[] player, Color newColor, float delayInSeconds)
    {
        StartCoroutine(ChangeColorAfterDelay(player, newColor, delayInSeconds));
    }

    private IEnumerator ChangeColorAfterDelay(GameObject[] player, Color newColor, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject ball in player)
        {
            SpriteRenderer spriteRenderer = ball.GetComponent<SpriteRenderer>();
    
            if (spriteRenderer != null)
            {
                spriteRenderer.color = newColor;
            }
            else
            {
                Debug.LogError("Le GameObject ne possède pas de composant SpriteRenderer.");
            }
        }

        yield return new WaitForSeconds(5f);

     
        DesactiverRBV(player, 0f);
        
        InitTabController.initTabController.CouvreTex.SetActive(true);
        InitTabController.initTabController.ouvrirTemps = true;
        InitTabController.initTabController.CouvreTexDebut.SetActive(false);
      
     
    }

    void DesactiverRBV(GameObject[] player, float delay)
    {
        StartCoroutine(DesactiverRB(player, delay));
    }

    private IEnumerator DesactiverRB(GameObject[] player, float delay)
    {
        yield return new WaitForSeconds(delay);

        DesactiverRigidbodyGameObjects(player);
     
    }

    private void DesactiverRigidbodyGameObjects(GameObject[] gameObjectPlacement)
    {
        foreach(GameObject ball in gameObjectPlacement)
        {
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
         
             if(ball && rb != null)
             {
                rb.Sleep();
                //rb.simulated = false;
                rb.isKinematic = true;
            }
        }
    }

    Color ChooseNewColor(string chosenColor)
    {
        if (chosenColor.ToLower() == "bleu")
        {
            return new Color(0, 0, 1); // Bleu
        }
        else if (chosenColor.ToLower() == "rouge")
        {
            return new Color(1, 0, 0); // Rouge
        }
        else
        {
            return Color.white; // Valeur par défaut si la couleur n'est ni bleue ni rouge
        }
    }

}
