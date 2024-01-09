using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InitTabController : MonoBehaviour
{
    public static InitTabController initTabController;

    // Arreter le jeu pour ouvrir le popup
    private bool gamePaused = false;
    public GameObject[] player;

    public GameObject[] FindMes;

    GameObject GameChoisis;
    
    public static int Level = 1; 
    public int Nbr_coul = 1;
    public int Temp_debut = 1;
    public int Temp_Prlongation = 0;
   

    // text jeu
    public Text Level_Count;
    public Text Nbr_coul_Count;
    public Text Temp_debut_Count;
    public Text Temp_Prlongation_Count;

    public GameObject CouvreTex;
    public GameObject CouvreTexDebut;

    // object popup pour Bombe
    public GameObject PopUp;
    public GameObject[] DeuxBoutonPop;
    
    public Text Level_Count_Popup;
    public Text Text_Show_Level_Popup;
    public Text Text_Show_Level_Popup_2;

    public GameObject FineMeNow;
   

    public bool ouvrirTemps = false;

    // Arreter le lorsque le temps de prolongation fini
    public bool Arreter_jeu = false;

    // Varriable finale
    public bool Final = false;

    // nombre couleur pour le prochain FindMe
    public int Nbr_couleur_prochain_FindMe = 1;
  

   
   
    
    
    void Awake()
    {
        if(initTabController != null)
        {
            return;
        }
        initTabController = this;
     }
    
    // Start is called before the first frame update
    void Start()
    {
     	TournerLeJeu();
        TempsDebut();
        FineMeNow.SetActive(false);

        AfficherText(FineMeNow, 8);
        
        Debug.Log(" Level : " + Level + " Nbr_coul : " + Nbr_coul + " Temp_debut : " + Temp_debut + " Temp_Prlongation : " + Temp_Prlongation + " ");
    }

    // Update is called once per frame
    void Update()
    {
        Level_Count.text = Level.ToString();
        Nbr_coul_Count.text = Nbr_coul.ToString();
        Temp_debut_Count.text = Temp_debut.ToString();
        Temp_Prlongation_Count.text = Temp_Prlongation.ToString();

        //FineMeNow.text = " Trouve moi maintenant! ".ToString();


        if(ouvrirTemps)
        {
            InitTabController.initTabController.Temp_Prlongation = 0;
            InitTabController.initTabController.TempsProlongation();
            ouvrirTemps = false;
        }

        /*
            Arreter le jeu lorsque le temps de 
            prolongation mit pour le jeu finissent
        */
        
        if(Temp_Prlongation >= 15)
        {
            Debug.Log(" Temps de jeu st efini !!!!!" + Temp_Prlongation);
            Arreter_jeu = true;
        }
        if(Arreter_jeu)
        {
            Winnner(false);
            Debug.Log(" Vous avez Echoué, FindMe ");
            OpenPop();
            Arreter_jeu = false;
        }   

    }

    void AfficherText(GameObject player, float delayInSeconds)
    {
        StartCoroutine(ChangeColorAfterDelayt(player, delayInSeconds));
    }

    private IEnumerator ChangeColorAfterDelayt(GameObject layer, float delay)
    {
        yield return new WaitForSeconds(delay);
        layer.SetActive(true);
    }

    public bool Winnner(bool val)
    {
        return val;
    }

    public void OpenPop()
    {
        PopUp.SetActive(true);
        
        Level_Count_Popup.text = " Level : " + Level + " ".ToString();

        if(Final)
        {
            Text_Show_Level_Popup.text = " Bravooo! vous avez Reussit! level : " + Level + " ".ToString();
            Text_Show_Level_Popup_2.text = " Prochain FindMe nombre : " + Nbr_couleur_prochain_FindMe + " ".ToString();
            DeuxBoutonPop[0].SetActive(true);
            DeuxBoutonPop[1].SetActive(false);
        }
        else
        {
            Text_Show_Level_Popup.text = " Echec! vous avez Echoué! level : " + Level + " ".ToString();
            Text_Show_Level_Popup_2.text = " FindMe nombre : " + Nbr_couleur_prochain_FindMe + " ".ToString();
            DeuxBoutonPop[0].SetActive(false);
            //DeuxBoutonPop[1].SetActive(true);
        }

        gamePaused = true;
        Time.timeScale = 0f;
    }

    public void ClosePop()
    {
        PopUp.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1f;
    }

    
    
    public void TournerLeJeu()
    {
    	int[] GameAchangerCouleur = RetournTabCouleur();
    	
    	FindMes = new GameObject[GameAchangerCouleur.Length];

    	for(int i = 0; i < GameAchangerCouleur.Length; i++)
    	{
    	    changerCouleurDunGameObject(player[GameAchangerCouleur[i]], ChooseNewColor("rouge"));
            FindMes[i] = player[GameAchangerCouleur[i]];
            Nbr_coul++;
    	}
    	
    }
    
    
    public void changerCouleurDunGameObject(GameObject game, Color newColor)
    {
    	  SpriteRenderer spriteRenderer = game.GetComponent<SpriteRenderer>();
    
          if (spriteRenderer != null)
          {
                spriteRenderer.color = newColor;
          }
    }
    
    public Color ChooseNewColor(string chosenColor)
    {
        if (chosenColor.ToLower() == "bleu")
        {
            return new Color(0, 0, 1); // Bleu
        }
        else if (chosenColor.ToLower() == "rouge")
        {
            return new Color(1, 0, 0); // Rouge
        }
        else if (chosenColor.ToLower() == "green")
        {
            return new Color(0, 1, 0); // vert
        }
        else
        {
            return Color.white; // Valeur par défaut si la couleur n'est ni bleue ni rouge
        }
    }
    
    public int[] RetournTabCouleur()
    {
    	int nbr_couleur = NormalisationLevelNbrCouleur(Level);
    	
    	int[] Tabs_Val = new int[nbr_couleur];
    	
    	for(int i = 0; i < nbr_couleur; i++)
    	{
    	    Tabs_Val[i] = Random.Range(0, player.Length);
    	}
    	
    	return Tabs_Val;
    }
    
    
    public int NormalisationLevelNbrCouleur(int val)
    {  
    	if(val < 3)
        {
            Nbr_couleur_prochain_FindMe = 1;
            return 1;
        }
        else
        if(val > 2 && val < 10)
        {
            Nbr_couleur_prochain_FindMe = 2;
            return 2;
        }
        else
        if(val > 10 && val < 25)
        {
            Nbr_couleur_prochain_FindMe = 3;
            return 3;
        }
        else
        if(val > 25 && val < 55)
        {
            Nbr_couleur_prochain_FindMe = 3;
            return 3;
        }
        else
        {
            Nbr_couleur_prochain_FindMe = 4;
            return 4;
        }
        
    }


    public void OuvrirPopUpFinJeu()
    {

    }

    public void OuvrirPopUpFinJeuGameOver()
    {
        
    }

    public void TempsDebut()
    {
        StartCoroutine(ReduireTempDebut());
    }

    public void TempsProlongation()
    {
        StartCoroutine(ReduireTempProlongation());
    }

    private IEnumerator ReduireTempDebut()
    {      
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(1f);
            Temp_debut++;
        }        
    }

    private IEnumerator ReduireTempProlongation()
    {    
        if(ouvrirTemps)
        {
            for (int i = 0; i < 15; i++)
            {
                yield return new WaitForSeconds(1f);
                Temp_Prlongation++;
            }  
        }
    }

    public void SceneSuivant()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(nextSceneIndex);
        Level++;
    }

    public void SceneResume()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void OuvrirSceneParNomSuivant()
    {
        // Charger la scène spécifiée par son nom
        SceneManager.LoadScene("SampleScene");
        Level++;
    }

    public void OuvrirSceneParNomResume()
    {
        // Charger la scène spécifiée par son nom
        SceneManager.LoadScene("SampleScene");
    }

    
}
