using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showrunner : MonoBehaviour
{
    public string showdata;
    public bool running;
    public GameObject tutor;
    public Transform tutortransform;
    public controller tutorcontrol;
    public bool cammed=false;
    controller hrac;
    int showstep;
    private void Awake(){
        tutor.SetActive(false);
    }
    public void StartShow(){
        running=true;
        hrac=GameObject.FindGameObjectWithTag("Player").GetComponent<controller>();
        showstep=0;
        tutor.SetActive(true);
        if(cammed){
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<kamera>().follow=tutortransform;
        }
    }
    public void StopShow(){
        Destroy(gameObject);
    }
    void StepShow(){
        tutorcontrol.lr=float.Parse(showdata[showstep].ToString()+showdata[showstep+1].ToString());
        tutorcontrol.u=showdata[showstep+2].ToString()=="t";
        tutorcontrol.ControlsUpdate();
        showstep+=3;
    }
    void FixedUpdate(){
        if(showstep>showdata.Length-1)running=false;
        if(running){
            tutor.GetComponentInParent<controller>().Pause(hrac.paused);
            if(hrac==null)StopShow();
            if(!hrac.paused){
                StepShow();
            }
        }
    }
}
