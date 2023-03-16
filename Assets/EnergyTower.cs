using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class EnergyTower : MonoBehaviour
{
    public float energyoliaBase;
    public float energyolia;
    public float intensity = 255;
    public GameObject dalleBase;
    public bool timerStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        energyolia = energyoliaBase;
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
        while(energyolia > 0){
            energyolia -= 1;
            intensity -= intensity/energyoliaBase;
            transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor",new Color(intensity,intensity,intensity));
            yield return new WaitForSeconds(1);
        }
    }

    public void restartEnergy(){
        energyolia = energyoliaBase;
    }
}
