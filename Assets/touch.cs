using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touch : MonoBehaviour
{
    controller pl;
    public Slider sl;
    public GameObject upbutt;
    public GameObject chuse,chmake;
    bool u0=false, u1=false;
    void Start(){
        pl=GameObject.FindGameObjectWithTag("Player").GetComponent<controller>();
        if(PlayerPrefs.GetInt("TouchControls")==0){
            upbutt.SetActive(false);
            sl.gameObject.SetActive(false);
        }
        if(PlayerPrefs.GetInt("Checkpoint")==0){
            chuse.SetActive(false);
            chmake.SetActive(false);
        }
    }
    void Update(){
        if(!u1&&u0)pl.upping=true;
        u1=u0;
        pl.uptl=u0;
    }
    public void Pressu(bool a){
        u0=a;
    }
    public void Presssl(bool a){
        if(!a)sl.value=50;
    }
    public void Slide(float a){
        pl.lr=a;
    }
    public void Pressmak(bool a){
        pl.checkp();
    }
    public void Pressuse(bool a){
        pl.revcheckp();
    }
}
