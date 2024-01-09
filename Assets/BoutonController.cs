using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonController : MonoBehaviour
{
    public static BoutonController boutonController;
    GameObject gameAct;
    GameObject[] ObjectTouche;
    int nombreCouleurOuvert = 0;

    // nombre de touche
    int Nbr_touches;

    // test- ouverture PopUp
    bool tester_OuPopUp = false;


    string[] NomCouleurGAme;

    void Awake()
    {
        if(boutonController != null)
        {
            return;
        }
        boutonController = this;
     }
    
    // Start is called before the first frame update
    void Start()
    {
        Nbr_touches = 0;

        gameAct = this.gameObject;

        ObjectTouche = InitTabController.initTabController.FindMes;

        NomCouleurGAme = new string[ObjectTouche.Length];

        nombreCouleurOuvert = ObjectTouche.Length;

        int i = 0;
        foreach (GameObject item in ObjectTouche)
        {
            if(item)
            {
                NomCouleurGAme[i] = item.name;
                i++;
            }         
        }

        //init nmbre de touché
        Nbr_touches = ObjectTouche.Length;

    }

    /*
        Arreter le jeu lorsque tous les FindMes sont identifier
    */
    void OnMouseDown()
    {
      
        if(gameAct.tag == "Pyr")
        {
            // et dimunuer le nombre de FindMes sur le bar
            Debug.Log(" le nombre de touché " + Nbr_touches);
            Nbr_touches--;
            InitTabController.initTabController.Nbr_coul--;
            Debug.Log(" le nombre de touché " + Nbr_touches);
            if(Nbr_touches <= 0 || InitTabController.initTabController.Nbr_coul <= 0)
            {
                tester_OuPopUp = true;
            }
        }

        foreach (string item in NomCouleurGAme)
        {
            if(gameAct.name == item && InitTabController.initTabController.Temp_Prlongation > 0)
            {
                ToucherFinMe(ObjectTouche, gameAct, tester_OuPopUp);
            }
            else
            {
                if(Nbr_touches <= 0)
                {
                    ToucherFinMe2(gameAct, tester_OuPopUp);
                }

            }
            
        }

     
 
      
    }


    public void ToucherFinMe(GameObject[] game, GameObject objectTouche, bool val)
    {
        int NobreFindMes = game.Length;

        foreach(GameObject ball in game)
        {
            if(NobreFindMes >= 0)
            {
                // changer la couleur de l'object à green
                InitTabController.initTabController.changerCouleurDunGameObject(objectTouche, InitTabController.initTabController.ChooseNewColor("green"));
               
                // Si les couleur sont fini
                if(val)
                {
                    InitTabController.initTabController.Winnner(true);
                    Debug.Log("Vous avez Gagné hein !!!! : ");
                    Debug.Log("Nbr_touches : " + Nbr_touches);
                    InitTabController.initTabController.Final = true;
                    InitTabController.initTabController.OpenPop();
                    break;
                }
                else
                {
                    Debug.Log("Nbr_touches : " + Nbr_touches);
                    break;
                }
             
            }
        }
    }


    public void ToucherFinMe2(GameObject objectTouche, bool val)
    {
        // changer la couleur de l'object à green
        InitTabController.initTabController.changerCouleurDunGameObject(objectTouche, InitTabController.initTabController.ChooseNewColor("rouge"));
               
        // Si les couleur sont fini
        //if(val)
        //{
            InitTabController.initTabController.Winnner(false);
            Debug.Log("Vous avez Echoué !!!! : ");
            Debug.Log("Nbr_touches : " + Nbr_touches);
            InitTabController.initTabController.OpenPop();
        //}
             
        
    }




}
