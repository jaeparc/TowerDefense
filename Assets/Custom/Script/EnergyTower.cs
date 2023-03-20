using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class EnergyTower : MonoBehaviour
{
    public float energyolia, energyoliaBase, intensity = 1, cost;
    public Color colorBase;
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
        if(GameObject.Find("comboManager(Clone)") != null){
            GameObject[] combo = GameObject.Find("comboManager(Clone)").GetComponent<comboManagerScript>().getCombo(gameObject);
            if(combo != null){
                for(int i = 0; i < combo.Length; i++){
                    if(combo[i] != null)
                        combo[i].GetComponent<EnergyTower>().energyolia = combo[i].GetComponent<EnergyTower>().energyoliaBase;
                }
            } else {
                energyolia = energyoliaBase;
            }
        } else {
            energyolia = energyoliaBase;
        }
    }


    //AFFICHAGE STAT ENERGIE QUAND LA SOURIS PASSE SUR LA TOUR
    void OnGUI(){
        GUI.skin = GameSkin;
        DisplayStats();
    }

    void OnMouseEnter(){
        if(gameObject.GetComponent<ComboTower>().built)
            displayNameActive = true;
    }

    void OnMouseExit(){
        //GetComponent<Renderer>().material.color = startColor;
        displayNameActive = false;
    }

    public void DisplayStats(){
        if(displayNameActive == true){
            Vector3 mousePos = Input.mousePosition;
            Debug.Log(mousePos);
            GUI.Box(new Rect(Input.mousePosition.x,Screen.height-Input.mousePosition.y-50,150,50),gameObject.GetComponent<UnitTower>().unitName+"\r\n Energie : "+gameObject.GetComponent<EnergyTower>().energyolia);
        }
    }
}
