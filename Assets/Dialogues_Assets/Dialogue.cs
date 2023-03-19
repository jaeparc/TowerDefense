using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string [] lines;
    public float textSpeed;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        gestionLines();
        gestionPersos();
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
                    SceneManager.LoadScene("Juan");
        }
    }

    void gestionLines(){
        if(Input.GetMouseButtonDown(0)){
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
        switch(index){
            case 0:
                xipetotec.color = new Color(0,0,0,0);
                break;
            case 1:
                xipetotec.color = new Color(0,0,0,255);
                break;
            case 4:
                xipetotec.color = new Color(255,255,255,255);
                break;
        }
    }
}