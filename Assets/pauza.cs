using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauza : MonoBehaviour
{
    vlajka vlaj;
    controller hrac;
    public zapauzuj pauzuj;
    void Start(){
        GetComponent<Canvas>().worldCamera=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        vlaj=GameObject.FindGameObjectWithTag("vlajka").GetComponent<vlajka>();
        hrac=GameObject.FindGameObjectWithTag("Player").GetComponent<controller>();
    }
    public void Resume(){
        pauzuj.pauzuj();
    }
    public void Exit(){
        vlaj.load("MainMenu");
    }
    public void Reload(){
        vlaj.load(SceneManager.GetActiveScene().name);
    }
    public void Quit(){
        Application.Quit();
    }
}
