using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class skeč : MonoBehaviour
{
    int text=0;
    float nowsec=0;
    bool returnnow=false;
    TMP_Text comp;
    float timeToTransition=1;
    int skip=3;
    bool pskip=true;
    public string returnto;
    public GameObject cutscened;
    public List<string> titles;
    public List<float> sec;
    void Start(){
        var p=Instantiate(cutscened);
        DontDestroyOnLoad(p);
        p.name="Cutscened";
        comp=GetComponent<TMP_Text>();
    }
    void Update(){
        nowsec+=Time.deltaTime;
        comp.text=titles[text];
    
        if(nowsec<timeToTransition){
            comp.alpha=nowsec/timeToTransition;
        }else if(nowsec-sec[text]-timeToTransition>0){
            comp.alpha=(nowsec-sec[text]-timeToTransition)/-timeToTransition+timeToTransition;
        }

        if(Input.GetAxis("Submit")>0.5&&!pskip)skip-=1;
        pskip=Input.GetAxis("Submit")>0.5; //pre telefóny toto spraviť tiež pls
        
        if(nowsec-timeToTransition*2>sec[text]){
            text+=1;
            nowsec=0;
            if(sec.Count<=text)returnnow=true;
            else if(sec[text]<0.5){
                timeToTransition=0.01f;
                sec[text]*=10;
            }
            else timeToTransition=1f;
        }
        if(returnnow||skip<=0){
            SceneManager.LoadScene(returnto);
            Destroy(GetComponentInParent<Canvas>().gameObject);
        }
    }
}
