using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class ComboTower : MonoBehaviour
{
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
            Debug.Log("damageMin b4:"+gameObject.GetComponent<UnitTower>().statsList[0].damageMin);
            gameObject.GetComponent<UnitTower>().statsList[0].damageMin = 250;
            gameObject.GetComponent<UnitTower>().statsList[0].damageMax = 250;
            Debug.Log("damageMin after:"+gameObject.GetComponent<UnitTower>().statsList[0].damageMin);
        }
    }
}
