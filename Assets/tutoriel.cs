using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class tutoriel : MonoBehaviour
{
    public Sprite[] tuto;
    public int index;
    public TextMeshProUGUI subtitle, instructions;
    public GameObject fade;
    public bool transitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.stopMusic();
        index = -1;
        subtitle.text = "";
        instructions.text = "Avant de commencer à jouer, les principes du jeu vont vous être expliqués afin que vous ne soyez pas perdus. \r\n \r\n Utilisez les flêches afin d'avancer.";
    }

    // Update is called once per frame
    void Update()
    {
        if(index >= 0 && index <= 4){
            instructions.text = "";
            subtitle.text = "Les tours";
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,255);
        }
        else if(index >= 5 && index <= 11){
            instructions.text = "";
            subtitle.text = "HUD";
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,255);
        }
        if(Input.GetKeyDown("left") && index > 0){
            index--;
            gameObject.GetComponent<SpriteRenderer>().sprite = tuto[index];
        } else if(Input.GetKeyDown("right")){
            if(index < tuto.Length){
                index++;
                gameObject.GetComponent<SpriteRenderer>().sprite = tuto[index];
            }
            else if(index == tuto.Length){
                subtitle.text = "";
                instructions.text = "Vous voilà arrivé à la fin du tutoriel. \r\n \r\n Si vous êtes une piètre divinité, utilisez les flêches pour naviguer dans le tutoriel sinon appuyez sur Espace pour commencer à jouer";
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            }
        } else if(Input.GetKeyDown("space") && index == tuto.Length){
            transitioning = true;
        } else if(transitioning){
            if(fade.GetComponent<Image>().color.a < 1)
                fade.GetComponent<Image>().color = new Color(0,0,0,fade.GetComponent<Image>().color.a+2*Time.deltaTime);
            else
                SceneManager.LoadScene("Scene 1");
        }
    }
}
