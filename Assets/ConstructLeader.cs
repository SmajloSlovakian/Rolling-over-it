using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Linq;

public class ConstructLeader : MonoBehaviour
{
    UnityWebRequest w;
    string lvltshw="";
    public GameObject shower;
    List<GameObject> elements=new List<GameObject>();
    bool downpending=false;
    public GameObject tutoranim;
    public GameObject tutorsprite;
    void Start(){
        shower.SetActive(false);
        tutorsprite.SetActive(false);
        tutoranim.SetActive(false);
    }
    void Update(){
        if(downpending){
            if(w.result==UnityWebRequest.Result.InProgress){
                tutoranim.transform.localEulerAngles-=Vector3.forward*5;
            }
            if(w.result==UnityWebRequest.Result.Success){
                downpending=false;
                ConstructWithData(Detabulate(w.downloadHandler.text));
                tutorsprite.SetActive(false);
                tutoranim.SetActive(false);
            }
            if(w.result!=UnityWebRequest.Result.Success&&w.result!=UnityWebRequest.Result.InProgress){
                GetComponentInChildren<TMP_Text>().text="Connection failed";
                downpending=false;
                tutorsprite.SetActive(false);
                tutoranim.SetActive(false);
            }
        }
    }
    public void Construct(string lvl){
        Deconstruct();
        tutorsprite.SetActive(true);
        tutoranim.SetActive(true);
        downpending=true;
        w=UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/e/2PACX-1vTLmV3izOqPiNprutlVzP7TL08kMOkiEG0RnfSsAHbNCqqQMeh69ZPjFw1WxWS8iN5o2z1vIAj1ll0L/pub?output=tsv"); //"https://docs.google.com/spreadsheets/d/1RZCHbi6KwUsKF05mb4BclbmPXjv5bcbpXCEqx9vXIW8/export?format=tsv"
        w.SendWebRequest();
        lvltshw=lvl;
    }
    public void ConstructWithData(string data){
        int it=-1;
        List<string> str=tolist(data.Split(";"));
        if(lvltshw!=""){
            GetComponentInChildren<TMP_Text>().text="Scores for level "+lvltshw+":";
        }
        str.Reverse(); //chcem podľa času
        foreach(string i in str){
            string[] o=i.Split(":");
            if(i!="")if(o[0]==lvltshw||lvltshw==""){
                it++;
                GameObject noveau=Instantiate(shower,transform);
                elements.Add(noveau);
                noveau.transform.localPosition=Vector3.down*it*50+Vector3.down*10;
                //noveau.transform.localPosition+=Vector3.down*10;
                noveau.SetActive(true);
                if(lvltshw!="")noveau.GetComponentInChildren<TMP_Text>().text="By: "+o[3]+"\nTime: "+SecToMS(o[2].Length/3f/50f);
                else noveau.GetComponentInChildren<TMP_Text>().text="Level: "+o[0]+"\nTime: "+SecToMS(o[2].Length/3f/50f);
                noveau.GetComponentInChildren<Runshow>().rundata=o[2];
                noveau.GetComponentInChildren<Runshow>().lvl=o[0];
                GetComponentsInParent<RectTransform>()[1].sizeDelta+=50*Vector2.up;
            }
        }
    }
    public void Deconstruct(){
        if(lvltshw!="")GetComponentInChildren<TMP_Text>().text="Downloading data...";
        GetComponentsInParent<RectTransform>()[1].sizeDelta=Vector2.up*25;
        foreach(GameObject i in elements){
            Destroy(i);
        }
        elements=new List<GameObject>();
    }
    string SecToMS(float sec){
        string ret="";
        if(Mathf.Floor(sec/360)!=0)ret+=Mathf.Floor(sec/360)+"hrs ";
        if(Mathf.Floor(sec/60)!=0)ret+=Mathf.Floor(sec/60)+"min ";
        if(sec!=0)ret+=Mathf.Floor((sec%60)*100)/100+"sec";
        return ret;
    }
    string Detabulate(string s){
        string ret="";
        foreach(string p in s.Split("\n")){
            if(p!="")if(p[0].ToString()!="Č"){
                if(ret.Length>0)ret+=";";
                string o=p.Split("\t")[1];
                if(o.Split(":").Length==4)ret+=o;
            }
        }
        return ret;
    }
    List<string> tolist(string[] s){
        List<string> ret=new List<string>();
        foreach(string i in s){
            ret.Add(i);
        }
        return(ret);
    }
    /*List<string> sortit(List<string> s){
        
        return(new List<string>());
    }*/
}
