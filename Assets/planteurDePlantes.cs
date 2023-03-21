using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planteurDePlantes : MonoBehaviour
{
    public GameObject[] plantes;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(plantes[Random.Range(0,plantes.Length)],transform.position,Quaternion.Euler(0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
