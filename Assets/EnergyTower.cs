using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class EnergyTower : MonoBehaviour
{
    public float energyoliaBase;
    public float energyolia;
    public Color colorBase;
    public float intensity = 1;
    public GameObject dalleBase;
    public bool timerStarted = false;

    //VAR AFFICHAGE STATS
    public GUISkin GameSkin;
    private Color startColor;
    private bool displayNameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        energyolia = energyoliaBase;
        colorBase = transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<ComboTower>().built && timerStarted == false){
            StartCoroutine(BaisseEnergy());
            timerStarted = true;
        }

        if(Input.GetKeyDown("space")){
            restartEnergy();
        }

        if(energyolia <= 0){
            Instantiate(dalleBase,transform.position,Quaternion.Euler(90,0,0));
            Destroy(gameObject);
        }
    }

    IEnumerator BaisseEnergy(){
        while(enabled){
            while(energyolia > 0){
                energyolia -= 1;
                intensity = energyolia/energyoliaBase;
                transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor",Color.Lerp(Color.black,colorBase,intensity));
                yield return new WaitForSeconds(1);
            }
        }
    }

    public void restartEnergy(){
        energyolia = energyoliaBase;
    }


    //AFFICHAGE STAT ENERGIE QUAND LA SOURIS PASSE SUR LA TOUR
    void OnGUI(){
        GUI.skin = GameSkin;
        DisplayStats();
    }

    void OnMouseEnter(){
        //startColor = GetComponent<Renderer>().material.color;
        //GetComponent<Renderer>().material.color = Color.blue;
        displayNameActive = true;
    }

    void OnMouseExit(){
        //GetComponent<Renderer>().material.color = startColor;
        displayNameActive = false;
    }

    public void DisplayStats(){
        if(displayNameActive == true){
            Vector3 mousePos = Input.mousePosition;
            GUI.Box(new Rect(mousePos.x,mousePos.y,150,25),"Energie : "+gameObject.GetComponent<EnergyTower>().energyolia);
        }
    }
}
