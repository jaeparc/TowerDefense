using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class connectorController : MonoBehaviour
{
    public GameObject tour1, tour2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(tour1 == null || tour2 == null){
            if(tour1 == null){
                tour2.GetComponent<ComboTower>().setDmgStat(tour2.GetComponent<ComboTower>().dmgStart);
                tour2.GetComponent<ComboTower>().statsUpdated = false;
            } else if(tour2 == null){
                tour1.GetComponent<ComboTower>().setDmgStat(tour1.GetComponent<ComboTower>().dmgStart);
                tour1.GetComponent<ComboTower>().statsUpdated = false;
            }
            Destroy(gameObject);
        }
    }
}
