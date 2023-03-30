using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class animationMenus : MonoBehaviour
{
    public TextMeshProUGUI pressAnyBtn;
    public bool descendant;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(descendant)
            pressAnyBtn.alpha -= Time.deltaTime;
        else if(!descendant)
            pressAnyBtn.alpha += Time.deltaTime;
        if(pressAnyBtn.alpha <= 0 && descendant)
            descendant = false;
        if(pressAnyBtn.alpha >= 1 && !descendant)
            descendant = true;
    }       
}
