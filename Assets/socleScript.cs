using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class socleScript : MonoBehaviour
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
        if(other.GetComponent<EnergyTower>() != null && other.GetComponent<EnergyTower>().destroying == false){
            Destroy(gameObject);
        }
    }
}
