using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TDTK;

public class start_end : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SpawnManager.GetTimeToNextWave() == -1){
            if(gameObject.GetComponent<TextMeshPro>().alpha > 0)
                gameObject.GetComponent<TextMeshPro>().alpha -= 2*Time.deltaTime;
            else
                gameObject.SetActive(false);
        }
    }
}
