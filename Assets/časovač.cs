using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class časovač : MonoBehaviour
{
    float čas;
    TMP_Text text;
    controller hrac;
    Transform tutor;
    public GameObject tutorobj;
    public GameObject runner;
    string savetutor;
	void Start(){
        text=GetComponent<TMP_Text>();
        if(SceneManager.GetActiveScene().buildIndex-1>PlayerPrefs.GetInt("Levels"))Destroy(gameObject);
        čas=0;
        hrac=GameObject.FindGameObjectWithTag("Player").GetComponent<controller>();

        foreach(string i in PlayerPrefs.GetString("NewScores").Split(";")){
            string[] o=i.Split(":");
            if(o[0]==SceneManager.GetActiveScene().name){
                if(o.Length>=3){
                    showrunner sr=Instantiate(runner).GetComponent<showrunner>();
                    sr.showdata=o[2];
                    sr.StartShow();
                }
            }
        }
    }
    public void save(){
        savetutor+=hrac.lr;
        if(hrac.u)savetutor+="t";
        else savetutor+="f";
    }
    void FixedUpdate(){
        if(!hrac.paused)čas+=0.02f;
        text.text=SecToMS(čas);
    }
    public string Finish(){
        string save="";
        bool wereiin=false;
        foreach(string i in PlayerPrefs.GetString("NewScores").Split(";")){
            if(i.Length==0)break;
            string[] o=i.Split(":");
            if(o[0]!=(SceneManager.GetActiveScene().name).ToString()){
                if(save.Length>0)save+=";";
                save+=i;
            }else{
                if(o[2].Length>savetutor.Length){
                    if(save.Length>0)save+=";";
                    save+=o[0]+":"+Application.version+":"+savetutor;
                }else{
                    if(save.Length>0)save+=";";
                    save+=i;
                }
                wereiin=true;
            }
        }
        if(!wereiin){
            if(save.Length>0)save+=";";
            save+=(SceneManager.GetActiveScene().name)+":"+Application.version+":"+savetutor;
        }
        PlayerPrefs.SetString("NewScores",save);
        return SecToMS(čas);
    }
    string SecToMS(float sec){
        string ret="";
        if(Mathf.Floor(sec/3600)!=0)ret+=Mathf.Floor(sec/3600)+"hrs ";
        if(Mathf.Floor(sec/60)!=0)ret+=Mathf.Floor(sec/60)+"min ";
        if(sec!=0){
            string i=(Mathf.Floor((sec%60)*100)).ToString();
            try{
                i=i.Insert(i.Length-2,",");  
            }catch{}
            if(i.Length==3)i=0+i;
            ret+=i+"sec";
        }
        return ret;
    }
}
