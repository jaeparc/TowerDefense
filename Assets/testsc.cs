using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testsc : MonoBehaviour
{
    private bool displayNameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGUI(){
        DisplayStats();
    }

    void OnMouseEnter(){
        displayNameActive = true;
    }

    void OnMouseExit(){
        //GetComponent<Renderer>().material.color = startColor;
        displayNameActive = false;
    }

    public void DisplayStats(){
        if(displayNameActive == true){
            Vector3 mousePos = Input.mousePosition;
            Debug.Log(mousePos);
            GUI.Box(new Rect(Input.mousePosition.x,Screen.height-Input.mousePosition.y-50,150,50),"HAAAAAAAAAAAAAAAAAA");
        }
    }
}
