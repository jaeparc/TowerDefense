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
                Connecteur(other);
                setDmgStat(dmgStart*coefCombo);
                statsUpdated = true;
                int x = 0;
                bool tower2combo = false;
                GameObject comboManagerExist = GameObject.Find("comboManager(Clone)");
                if(comboManagerExist != null){
                    for(int i = 0; i < comboManagerExist.GetComponent<comboManagerScript>().combos.GetLength(0); i++){
                        for(int n = 0; n < comboManagerExist.GetComponent<comboManagerScript>().combos.GetLength(1); n++){
                            if(comboManagerExist.GetComponent<comboManagerScript>().combos[i,n] == other.gameObject){
                                tower2combo = true;
                                x = i;
                            } else if(n == 0 && comboManagerExist.GetComponent<comboManagerScript>().combos[i,n] == null){
                                x = i;
                            }
                        }
                    }
                    if(tower2combo == false){
                        comboManagerExist.GetComponent<comboManagerScript>().combos[x,0] = gameObject;
                        comboManagerExist.GetComponent<comboManagerScript>().combos[x,1] = other.gameObject;
                    } else {
                        if(comboManagerExist.GetComponent<comboManagerScript>().combos[x,2] == null){
                            comboManagerExist.GetComponent<comboManagerScript>().combos[x,2] = gameObject;
                        } else {
                            Debug.Log("Limite atteinte!");
                        }
                    }
                } else {
                    Instantiate(comboManagerVar,new Vector3(0,0,0),Quaternion.Euler(0,0,0));
                }
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == gameObject.tag && built && other.GetComponent<ComboTower>().built && statsUpdated){
            destroyConnecteurs(gameObject,other.gameObject);
            setDmgStat(dmgStart);
            statsUpdated = false;
        }
    }

    void Connecteur(Collider other){
        if(gameObject.transform.position.x == other.transform.position.x || gameObject.transform.position.z == other.transform.position.z){
            Vector3 setupConnection = new Vector3(0,0,0);
            Quaternion rota = Quaternion.Euler(0,0,0);
            if(gameObject.transform.position.x > other.transform.position.x){//si la tour 2 est à gauche
                setupConnection = new Vector3(-0.5f,0,0);
                rota = Quaternion.Euler(0,0,90);
            } else if(gameObject.transform.position.x < other.transform.position.x){
                setupConnection = new Vector3(0.5f,0,0);
                rota = Quaternion.Euler(0,0,90);
            }
            if(gameObject.transform.position.z > other.transform.position.z){
                setupConnection = new Vector3(0,0,-0.5f);
                rota = Quaternion.Euler(90,0,0);
            } else if(gameObject.transform.position.z < other.transform.position.z){
                setupConnection = new Vector3(0,0,0.5f);
                rota = Quaternion.Euler(90,0,0);
            }
            setupConnection+=gameObject.transform.position;
            GameObject connector = Instantiate(connection,setupConnection,rota);
            connector.GetComponent<connectorController>().tour1 = gameObject;
            connector.GetComponent<connectorController>().tour2 = other.gameObject;
        }
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
