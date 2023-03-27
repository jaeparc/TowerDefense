using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class menuController : MonoBehaviour
{
    public int indexButton = 0;
    public TextMeshProUGUI[] boutons;
    public GameObject fade;
    public bool transitioningOut = false;
    private bool[] descendant = {false,false,false};

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "ecran_titre" || SceneManager.GetActiveScene().name == "credits"){
            if(Input.anyKeyDown && !transitioningOut){
                transitioningOut = true;
            }
            if(transitioningOut){
                if(fade.GetComponent<Image>().color.a < 1)
                    fade.GetComponent<Image>().color = new Color(0,0,0,fade.GetComponent<Image>().color.a+2*Time.deltaTime);
                else
                    SceneManager.LoadScene("menu_principal");
            }
        }
        if(SceneManager.GetActiveScene().name == "menu_principal")
            Select();

    }

    public void PlayBtn(){
        SceneManager.LoadScene("cinematique_debut");
    }

    public void CreditsBtn(){
        SceneManager.LoadScene("credits");
    }

    public void QuitBtn(){
        Application.Quit();
    }

    public void Select(){
        if(Input.GetKeyDown("return")){
            switch(indexButton){
                case 0:
                    PlayBtn();
                    break;
                case 1:
                    CreditsBtn();
                    break;
                case 2:
                    QuitBtn();
                    break;
            }
        }
        if(Input.GetKeyDown("down")){
            if(indexButton != 2){
                indexButton++;
            } else if(indexButton == 2){
                indexButton = 0;
            }
        } else if(Input.GetKeyDown("up")){
            if(indexButton != 0){
                indexButton--;
            } else if(indexButton == 0){
                indexButton = 2;
            }
        }
        switch(indexButton){
            case 0:
                if(descendant[0])
                    boutons[0].alpha -= 2*Time.deltaTime;
                else if(!descendant[0])
                    boutons[0].alpha += 2*Time.deltaTime;
                if(boutons[0].alpha <= 0 && descendant[0])
                    descendant[0] = false;
                if(boutons[0].alpha >= 1 && !descendant[0])
                    descendant[0] = true;
                boutons[1].alpha = 1;
                boutons[2].alpha = 1;
                break;
            case 1:
                if(descendant[1])
                    boutons[1].alpha -= 2*Time.deltaTime;
                else if(!descendant[1])
                    boutons[1].alpha += 2*Time.deltaTime;
                if(boutons[1].alpha <= 0 && descendant[1])
                    descendant[1] = false;
                if(boutons[1].alpha >= 1 && !descendant[1])
                    descendant[1] = true;
                boutons[0].alpha = 1;
                boutons[2].alpha = 1;
                break;
            case 2:
                if(descendant[2])
                    boutons[2].alpha -= 2*Time.deltaTime;
                else if(!descendant[2])
                    boutons[2].alpha += 2*Time.deltaTime;
                if(boutons[2].alpha <= 0 && descendant[2])
                    descendant[2] = false;
                if(boutons[2].alpha >= 1 && !descendant[2])
                    descendant[2] = true;
                boutons[0].alpha = 1;
                boutons[1].alpha = 1;
                break;
        }
    }
}
