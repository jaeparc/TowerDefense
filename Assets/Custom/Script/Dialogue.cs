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
    bool transitioning, transitioningIn, dialogueStarted, hasBeenPlayed, end;
    public AudioClip gosseQuiCourt, ambianceTemple;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        if(SceneManager.GetActiveScene().name == "cinematique_debut" || SceneManager.GetActiveScene().name == "cinematique_fin"){
            SoundManager.Instance.playMusic(ambianceTemple);
            if(SceneManager.GetActiveScene().name == "cinematique_debut"){
                SoundManager.Instance.playSound(gosseQuiCourt);
            }
        }
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
                if(SceneManager.GetActiveScene().name == "cinematique_debut")
                    SceneManager.LoadScene("Tutoriel");
                else if(SceneManager.GetActiveScene().name == "cinematique_fin")
                    SceneManager.LoadScene("menu_principal");
                else if(SceneManager.GetActiveScene().name == "cinematique_victoire")
                    SceneManager.LoadScene("credits");
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
                    if(SceneManager.GetActiveScene().name == "cinematique_debut")
                        SceneManager.LoadScene("Tutoriel");
                    else if(SceneManager.GetActiveScene().name == "cinematique_fin")
                        SceneManager.LoadScene("menu_principal");
                    else if(SceneManager.GetActiveScene().name == "cinematique_victoire")
                        SceneManager.LoadScene("credits");
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
        if(SceneManager.GetActiveScene().name == "cinematique_debut"){
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
        } else if(SceneManager.GetActiveScene().name == "cinematique_fin"){
            SpriteRenderer mictantecuhtli = GameObject.Find("mictlantecuhtli").GetComponent<SpriteRenderer>();
            SpriteRenderer tezcatlipoca = GameObject.Find("tezcatlipoca").GetComponent<SpriteRenderer>();
            SpriteRenderer kid = GameObject.Find("enfant").GetComponent<SpriteRenderer>();

            switch(index){
                case 1:
                    if(tezcatlipoca.color.a > 0){
                        tezcatlipoca.color = new Color(255,255,255,tezcatlipoca.color.a-2*Time.deltaTime);
                    }
                    if(kid.color.a < 1){
                        kid.color = new Color(255,255,255,kid.color.a+2*Time.deltaTime);
                    }
                    if(kid.color.a >= 1){
                        if(mictantecuhtli.color.a < 1){
                            mictantecuhtli.color = new Color(255,255,255,mictantecuhtli.color.a+0.5f*Time.deltaTime);
                        }
                        if(!SoundManager.Instance.isEffectPlaying() && !hasBeenPlayed){
                            SoundManager.Instance.playSound(gosseQuiCourt);
                            hasBeenPlayed = true;
                        }
                    }
                    break;
                case 2:
                    if(fade.GetComponent<Image>().color.a < 1){
                        fade.GetComponent<Image>().color = new Color(0,0,0,fade.GetComponent<Image>().color.a+2*Time.deltaTime);
                    }
                    if(fade.GetComponent<Image>().color.a >= 1 && fade.GetComponentInChildren<TextMeshProUGUI>().alpha < 1){
                        fade.GetComponentInChildren<TextMeshProUGUI>().text = "Game Over.";
                        fade.GetComponentInChildren<TextMeshProUGUI>().alpha += 2*Time.deltaTime;
                    }
                    break;
                case 3:
                    SceneManager.LoadScene("menu_principal");
                    break;
            }
        } else if(SceneManager.GetActiveScene().name == "cinematique_victoire"){
            SpriteRenderer xipetoteckid = GameObject.Find("xipetoteckid").GetComponent<SpriteRenderer>();
            SpriteRenderer tezcatlipoca = GameObject.Find("tezcatlipoca").GetComponent<SpriteRenderer>();

            switch(index){
                case 2:
                    if(xipetoteckid.color.a > 0)
                        xipetoteckid.color = new Color(255,255,255,xipetoteckid.color.a-2*Time.deltaTime);
                    if(tezcatlipoca.color.a < 1)
                        tezcatlipoca.color = new Color(255,255,255,tezcatlipoca.color.a+2*Time.deltaTime);
                    break;
                case 3:
                    if(fade.GetComponent<Image>().color.a < 1){
                        fade.GetComponent<Image>().color = new Color(0,0,0,fade.GetComponent<Image>().color.a+2*Time.deltaTime);
                    }
                    if(fade.GetComponent<Image>().color.a >= 1 && fade.GetComponentInChildren<TextMeshProUGUI>().alpha < 1){
                        fade.GetComponentInChildren<TextMeshProUGUI>().text = "After coming back to his village, the kid was given a special nickname. \r\n \r\n He was called \r\n the Sacred Child.";
                        fade.GetComponentInChildren<TextMeshProUGUI>().alpha += 2*Time.deltaTime;
                    }
                    break;
                case 4:
                    if(fade.GetComponentInChildren<TextMeshProUGUI>().alpha > 0)
                        fade.GetComponentInChildren<TextMeshProUGUI>().alpha -= 2*Time.deltaTime;
                    if(fade.GetComponentInChildren<TextMeshProUGUI>().alpha <= 0)
                        fade.GetComponentInChildren<TextMeshProUGUI>().text = "Thank you for playing. \r\n \r\n Click anywhere to to go to the credits.";
                        end = true;
                    if(end == true && fade.GetComponentInChildren<TextMeshProUGUI>().alpha < 1)
                        fade.GetComponentInChildren<TextMeshProUGUI>().alpha += 2*Time.deltaTime;
                    break;
            }
        }
    }
}