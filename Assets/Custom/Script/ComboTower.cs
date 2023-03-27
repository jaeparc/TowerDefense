using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class ComboTower : MonoBehaviour
{
    public GameObject connection, comboManagerVar;
    public float dmgStart, coefCombo, dmg;
    public bool statsUpdated = false;
    public bool built = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.GetComponent<UnitTower>().prefabID);
        Debug.Log(gameObject.GetComponent<UnitTower>().GetCost()[0].ToString("f0"));
    }

    // Update is called once per frame
    void Update()
    {
        if(built == false){
            dmgStart = gameObject.GetComponent<UnitTower>().statsList[0].damageMin;
        }
        dmg = gameObject.GetComponent<UnitTower>().statsList[0].damageMin;
    }
    
    void OnTriggerStay(Collider other){
        if(other.GetComponent<UnitTower>() != null){
            if(other.GetComponent<UnitTower>().prefabID == gameObject.GetComponent<UnitTower>().prefabID && built && other.GetComponent<ComboTower>().built && statsUpdated == false){
                int x = -1;
                bool tower2combo = false;
                GameObject comboManagerExist = GameObject.Find("comboManager(Clone)");

                if(comboManagerExist != null){
                    comboManagerScript comboManager = comboManagerExist.GetComponent<comboManagerScript>();
                    for(int i = 0; i < comboManager.combos.GetLength(0); i++){
                        for(int n = 0; n < comboManager.combos.GetLength(1); n++){
                            if(comboManager.combos[i,n] == other.gameObject){
                                tower2combo = true;
                                x = i;
                                break;
                            } else if(n == 0 && comboManager.combos[i,n] == null){
                                x = i;
                                break;
                            }
                        }
                        if(x != -1)
                            break;
                    }
                    if(tower2combo == false){
                        comboManager.combos[x,0] = gameObject;
                        comboManager.combos[x,1] = other.gameObject;
                        Connecteur(other);
                        setDmgStat(dmgStart*coefCombo);
                        gameObject.GetComponent<EnergyTower>().energyolia = gameObject.GetComponent<EnergyTower>().energyoliaBase;
                        other.gameObject.GetComponent<EnergyTower>().energyolia = other.gameObject.GetComponent<EnergyTower>().energyoliaBase;
                        statsUpdated = true;
                    } else {
                        if(comboManager.combos[x,2] == null && comboManager.combos[x,1] != gameObject){
                            comboManager.combos[x,2] = gameObject;
                            Connecteur(other);
                            setDmgStat(dmgStart*coefCombo);
                            comboManager.combos[x,0].GetComponent<EnergyTower>().energyolia = comboManager.combos[x,0].GetComponent<EnergyTower>().energyoliaBase;
                            comboManager.combos[x,1].GetComponent<EnergyTower>().energyolia = comboManager.combos[x,1].GetComponent<EnergyTower>().energyoliaBase;
                            comboManager.combos[x,2].GetComponent<EnergyTower>().energyolia = comboManager.combos[x,2].GetComponent<EnergyTower>().energyoliaBase;
                            Debug.Log("Combo fini! X="+x);
                            Debug.Log("X="+x+";1="+comboManager.combos[x,0].GetComponent<UnitTower>().instanceID+";2="+comboManager.combos[x,1].GetComponent<UnitTower>().instanceID+";3="+comboManager.combos[x,2].GetComponent<UnitTower>().instanceID);
                            statsUpdated = true;
                        } else {
                            Debug.Log("Limite atteinte!");
                        }
                    }
                } else {
                    Instantiate(comboManagerVar,new Vector3(0,0,0),Quaternion.Euler(0,0,0));
                    Connecteur(other);
                    setDmgStat(dmgStart*coefCombo);
                    statsUpdated = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == gameObject.tag && built && other.GetComponent<ComboTower>() != null && statsUpdated){
            if(other.GetComponent<ComboTower>().built){
                destroyConnecteurs(gameObject,other.gameObject);
                setDmgStat(dmgStart);
                statsUpdated = false;
            }
        }
    }

    void Connecteur(Collider other){
        // if(gameObject.transform.position.x.ToString("F2") == other.transform.position.x.ToString("F2") || gameObject.transform.position.z.ToString("F2") == other.transform.position.z.ToString("F2")){
            Vector3 setupConnection = new Vector3(0,0,0);
            Quaternion rota = Quaternion.Euler(0,0,0);
            if(gameObject.transform.position.x - other.transform.position.x > 0.2f ){//si la tour 2 est à gauche
                setupConnection = new Vector3(-0.5f,0,0);
                rota = Quaternion.Euler(0,0,90);
            } else if(gameObject.transform.position.x - other.transform.position.x < -0.2f ){
                setupConnection = new Vector3(0.5f,0,0);
                rota = Quaternion.Euler(0,0,90);
            }
            if(gameObject.transform.position.z - other.transform.position.z > 0.2f ){
                setupConnection = new Vector3(0,0,-0.5f);
                rota = Quaternion.Euler(90,0,0);
            } else if(gameObject.transform.position.z - other.transform.position.z < -0.2f ){
                setupConnection = new Vector3(0,0,0.5f);
                rota = Quaternion.Euler(90,0,0);
            }
            setupConnection+=gameObject.transform.position;
            GameObject connector = Instantiate(connection,setupConnection,rota);
            connector.GetComponent<connectorController>().tour1 = gameObject;
            connector.GetComponent<connectorController>().tour2 = other.gameObject;
        // }
    }

    public void destroyConnecteurs(GameObject tower1, GameObject tower2){
        for(int i = 0; i < GameObject.FindGameObjectsWithTag("connector").Length; i++){
            GameObject connecteurTour1 = GameObject.FindGameObjectsWithTag("connector")[i].GetComponent<connectorController>().tour1;
            GameObject connecteurTour2 = GameObject.FindGameObjectsWithTag("connector")[i].GetComponent<connectorController>().tour2;
            if(connecteurTour1 == tower1 && connecteurTour2 == tower2){
                Destroy(GameObject.FindGameObjectsWithTag("connector")[i]);
            }
        }
    }

    public void setDmgStat(float dmg){
        gameObject.GetComponent<UnitTower>().statsList[0].damageMin = dmg;
        gameObject.GetComponent<UnitTower>().statsList[0].damageMax = dmg;
    }
}
