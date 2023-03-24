using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationRoue : MonoBehaviour
{
    public Transform axe_avgauche, axe_avdroit, axe_argauche, axe_ardroit;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float realSpeed = speed*Time.deltaTime;
        for (int i = 0; i < transform.childCount; i++)
        {
            switch(transform.GetChild(i).name){
                case "roue_avgauche":
                    transform.GetChild(i).RotateAround(axe_avgauche.position,axe_avgauche.right,realSpeed);
                    break;
                case "roue_avdroite":
                    transform.GetChild(i).RotateAround(axe_avdroit.position,axe_avdroit.right,realSpeed);
                    break;
                case "roue_ardroite":
                    transform.GetChild(i).RotateAround(axe_ardroit.position,axe_ardroit.right,realSpeed);
                    break;
                case "roue_argauche":
                    transform.GetChild(i).RotateAround(axe_argauche.position,axe_argauche.right,realSpeed);
                    break;
            }
        }
    }
}
