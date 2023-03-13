using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class ComboTower : MonoBehaviour
{
    public GameObject connection;
    private GameObject connectionExist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnTriggerEnter(Collider other){
        if(other.tag == gameObject.tag){
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
                    Debug.Log("OH");
                } else if(gameObject.transform.position.z < other.transform.position.z){
                    setupConnection = new Vector3(0,0,0.5f);
                    rota = Quaternion.Euler(90,0,0);
                    Debug.Log("OH");
                }
                setupConnection+=gameObject.transform.position;
                connectionExist = Instantiate(connection,setupConnection,rota);
            }
            Debug.Log("damageMin b4:"+gameObject.GetComponent<UnitTower>().statsList[0].damageMin);
            gameObject.GetComponent<UnitTower>().statsList[0].damageMin = 250;
            gameObject.GetComponent<UnitTower>().statsList[0].damageMax = 250;
            Debug.Log("damageMin after:"+gameObject.GetComponent<UnitTower>().statsList[0].damageMin);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == gameObject.tag){
            Destroy(GameObject.Find("connection"));
            Destroy(GameObject.Find("connection(Clone)"));
        }
    }
}
