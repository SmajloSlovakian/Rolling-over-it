using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class zapauzuj : MonoBehaviour
{
    controller hrac;
    public GameObject pausmenu;
    GameObject inst;
    bool predp;
    void Start(){
        hrac=GameObject.FindGameObjectWithTag("Player").GetComponent<controller>();
    }
    void Update(){
        if(Input.GetAxis("Cancel")>0.5&&!predp){
            pauzuj();
        }
        predp=Input.GetAxis("Cancel")>0.5;
    }
    public void pauzuj(bool iswin=false){
        hrac.Pause(!hrac.paused);
        if(hrac.paused){
            inst=Instantiate(pausmenu,GetComponentsInParent<Transform>()[2]);
            inst.GetComponent<pauza>().pauzuj=this;
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(GameObject.Find("ResumeButt"));
            if(!iswin)GameObject.Find("Win submenu").SetActive(false);
            else GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(GameObject.Find("RestartButt"));
        }else{
            Destroy(inst);
        }
    }
}
