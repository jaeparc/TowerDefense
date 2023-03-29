using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string [] lines;
    public float textSpeed;
    public GameObject fade;
    bool transitioning, transitioningIn, dialogueStarted;
    public AudioClip gosseQuiCourt, ambianceTemple;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        SoundManager.Instance.playSound(gosseQuiCourt);
        SoundManager.Instance.playMusic(ambianceTemple);
    }

    // Update is called once per frame
    void Update()
    {
        if(!dialogueStarted && Input.anyKeyDown && !SoundManager.Instance.isEffectPlaying()){
            transitioningIn = true;
            startDialogue();
            dialogueStarted = true;
        }
        if(transitioningIn){
            if(fade.GetComponent<Image>().color.a > 0){
                fade.GetComponent<Image>().color = new Color(0,0,0,fade.GetComponent<Image>().color.a-2*Time.deltaTime);
                fade.GetComponentInChildren<TextMeshProUGUI>().alpha -= 2*Time.deltaTime;
            }
            else{
                transitioningIn = false;
            }
        }
        if(dialogueStarted){
            gestionLines();
            gestionPersos();
            skipCinematique();
        }
    }

    void skipCinematique(){
        if(Input.GetKey("space")){
            if(!transitioning)
                transitioning = true;
        }
        if(transitioning){
            if(fade.GetComponent<Image>().color.a < 1)
                fade.GetComponent<Image>().color = new Color(0,0,0,fade.GetComponent<Image>().color.a+2*Time.deltaTime);
            else
                SceneManager.LoadScene("Tutoriel");
        }
    }

    void startDialogue(){
        index = 0;
        StartCoroutine(typeLine());
    }

    IEnumerator typeLine(){
        foreach(char c in lines[index].ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void nextLine(){
        if(index < lines.Length - 1){
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(typeLine());
        } else if(index == lines.Length-1){
            if(!transitioning)
                transitioning = true;
            if(transitioning){
                if(fade.GetComponent<Image>().color.a < 1)
                    fade.GetComponent<Image>().color = new Color(0,0,0,fade.GetComponent<Image>().color.a+2*Time.deltaTime);
                else
                    SceneManager.LoadScene("Tutoriel");
            }
        }
        
    }

    void gestionLines(){
        if(Input.GetMouseButtonDown(0) || Input.GetKey("return")){
            if(textComponent.text == lines[index]){
                nextLine();
            } else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void gestionPersos(){
        SpriteRenderer xipetotec = GameObject.Find("xipetotec").GetComponent<SpriteRenderer>();
        SpriteRenderer tezcatlipoca = GameObject.Find("tezcatlipoca").GetComponent<SpriteRenderer>();
        SpriteRenderer kid = GameObject.Find("enfant").GetComponent<SpriteRenderer>();
        switch(index){
            case 0:
                xipetotec.color = new Color(0,0,0,0);
                break;
            case 1:
                if(xipetotec.color.a < 1)
                    xipetotec.color = new Color(0,0,0,xipetotec.color.a+2*Time.deltaTime);
                break;
            case 4:
                if(xipetotec.color.r < 1)
                    xipetotec.color = new Color(xipetotec.color.r+2*Time.deltaTime,xipetotec.color.g+2*Time.deltaTime,xipetotec.color.b+2*Time.deltaTime,255);
                break;
            case 13:
                if(tezcatlipoca.color.a < 1)
                    tezcatlipoca.color = new Color(255,255,255,tezcatlipoca.color.a+2*Time.deltaTime);
                if(kid.color.r > 0.5)
                    kid.color = new Color(kid.color.r-2*Time.deltaTime,kid.color.g-2*Time.deltaTime,kid.color.b-2*Time.deltaTime,255);
                if(xipetotec.color.r > 0.5)
                    xipetotec.color = new Color(xipetotec.color.r-2*Time.deltaTime,xipetotec.color.g-2*Time.deltaTime,xipetotec.color.b-2*Time.deltaTime,255);
                break;
            case 15:
                if(tezcatlipoca.color.a > 0)
                    tezcatlipoca.color = new Color(255,255,255,tezcatlipoca.color.a-2*Time.deltaTime);
                if(kid.color.r < 1)
                    kid.color = new Color(kid.color.r+2*Time.deltaTime,kid.color.g+2*Time.deltaTime,kid.color.b+2*Time.deltaTime,255);
                if(xipetotec.color.r < 1)
                    xipetotec.color = new Color(xipetotec.color.r+2*Time.deltaTime,xipetotec.color.g+2*Time.deltaTime,xipetotec.color.b+2*Time.deltaTime,255);
                break;
        }
    }
}