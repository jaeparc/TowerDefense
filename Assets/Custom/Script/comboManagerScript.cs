using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comboManagerScript : MonoBehaviour
{
    public GameObject[,] combos = new GameObject[100,3];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] getCombo(GameObject tour){
        for(int i = 0; i < combos.GetLength(0); i++){
            for(int n = 0; n < combos.GetLength(1); n++){
                if(combos[i,n] == tour){
                    GameObject[] rowToReturn = new GameObject[combos.GetLength(1)];
                    for(int m = 0; m < combos.GetLength(1); m++){
                        rowToReturn[m] = combos[i,m];
                    }
                    return rowToReturn;
                }
            }
        }
        return null;
    }
}
