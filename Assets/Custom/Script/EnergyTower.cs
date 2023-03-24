using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDTK;

public class EnergyTower : MonoBehaviour
{
    public float energyolia, energyoliaBase, intensity = 1, cost;
    public Color colorBase, BasecolorBase;
    public GameObject dalleBase, VFXdisparition;
    public bool timerStarted = false, rembourse = false;
    public string type;
    public AnimationCurve courbe;

    //VAR AFFICHAGE STATS
    public GUISkin GameSkin;
    private Color startColor;
    private bool displayNameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        energyolia = energyoliaBase;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Indicator"){
                colorBase = transform.GetChild(i).GetComponent<Renderer>().material.GetColor("_EmissionColor");
                BasecolorBase = transform.GetChild(i).GetComponent<Renderer>().material.GetColor("_BaseColor");
            }
        }
        //colorBase = transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<ComboTower>().built && timerStarted == false)
        {
            StartCoroutine(BaisseEnergy());
            timerStarted = true;
        }

        if (Input.GetKeyDown("space"))
        {
            restartEnergy();
        }

        if (energyolia <= 0)
        {
            Instantiate(dalleBase, transform.position, Quaternion.Euler(-90, 0, 0)).GetComponent<socleScript>().type = type;
            if (VFXdisparition != null)
                Instantiate(VFXdisparition, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    IEnumerator BaisseEnergy()
    {
        while (enabled)
        {
            while (energyolia > 0)
            {
                energyolia--;
                intensity = energyolia / energyoliaBase;
                for (int i = 0; i < transform.childCount; i++)
                {
                    Debug.Log(courbe.Evaluate(intensity));
                    if (transform.GetChild(i).tag == "Indicator"){
                        transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.black,colorBase, courbe.Evaluate(intensity)));
                        transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_BaseColor", Color.Lerp(Color.black,BasecolorBase, intensity));
                    }
                }
                yield return new WaitForSeconds(1);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "socle(Clone)" && gameObject.GetComponent<ComboTower>().built && other.GetComponent<socleScript>().type == type && !rembourse)
        {
            GameObject.Find("GameControl").GetComponent<RscManager>()._GainRsc(new List<float> { gameObject.GetComponent<UnitTower>().GetCost()[0] * 0.25f });
            rembourse = true;
        }
    }

    public void restartEnergy()
    {
        if (GameObject.Find("comboManager(Clone)") != null)
        {
            GameObject[] combo = GameObject.Find("comboManager(Clone)").GetComponent<comboManagerScript>().getCombo(gameObject);
            if (combo != null)
            {
                for (int i = 0; i < combo.Length; i++)
                {
                    if (combo[i] != null && GameObject.Find("GameControl").GetComponent<RscManager>()._SpendRsc(new List<float> { cost }))
                        combo[i].GetComponent<EnergyTower>().energyolia = combo[i].GetComponent<EnergyTower>().energyoliaBase;
                }
            }
            else
            {
                if (GameObject.Find("GameControl").GetComponent<RscManager>()._SpendRsc(new List<float> { cost }))
                    energyolia = energyoliaBase;
            }
        }
        else
        {
            energyolia = energyoliaBase;
            GameObject.Find("GameControl").GetComponent<RscManager>()._SpendRsc(new List<float> { cost });
        }
    }


    //AFFICHAGE STAT ENERGIE QUAND LA SOURIS PASSE SUR LA TOUR
    void OnGUI()
    {
        GUI.skin = GameSkin;
        DisplayStats();
    }

    void OnMouseEnter()
    {
        if (gameObject.GetComponent<ComboTower>().built)
            displayNameActive = true;
    }

    void OnMouseExit()
    {
        //GetComponent<Renderer>().material.color = startColor;
        displayNameActive = false;
    }

    public void DisplayStats()
    {
        if (displayNameActive == true)
        {
            GUI.Box(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y - 50, 150, 50), gameObject.GetComponent<UnitTower>().unitName + "\r\n Energie : " + gameObject.GetComponent<EnergyTower>().energyolia);
        }
    }
}
