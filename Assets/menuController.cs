using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "ecran_titre"){
            if(Input.anyKeyDown){
                SceneManager.LoadScene("menu_principal");
            }
        }
    }

    public void PlayBtn(){
        SceneManager.LoadScene("cinematique_debut");
    }
}
