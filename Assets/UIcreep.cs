using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class UIcreep : MonoBehaviour
{
    public GUISkin GameSkin;
    public bool DisplayNameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI(){
        GUI.skin = GameSkin;
        DisplayName();
    }

    void OnMouseEnter(){
        DisplayNameActive = true;
    }

    void OnMouseExit(){
        DisplayNameActive = false;
    }

    public void DisplayName(){
        if(DisplayNameActive)
            GUI.Box(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y - 50, 150, 25), gameObject.GetComponent<UnitCreep>().unitName);
    }
}
