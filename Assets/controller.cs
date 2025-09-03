using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controller : MonoBehaviour
{
    public Rigidbody2D ja;
    public Transform jatr;
    public Transform checkpoints;
    public float add;
    public float jump;
    public int jnumber=1;
    public float velopred;
    public SpriteRenderer sprite;
    public Sprite litevlaj;
    public bool paused=false;
    public float lr=50;
    public bool up=false, u=false;
    public bool uptl=false;
    public bool upping;
    public float ch=0,chp=0;
    public bool somtutor=false;
    časovač casovac;
    private void Start() {
        if(!somtutor){
            Pause(false);
            casovac=GameObject.Find("Timer").GetComponent<časovač>();
        }
    }

    void Update(){
        if(!somtutor){
            if(uptl||Input.GetAxis("Jump")>0.5)u=uptl||Input.GetAxis("Jump")>0.5;
            if(Input.GetAxis("Horizontal")!=0){
                lr=Mathf.Round(Input.GetAxis("Horizontal")*40+50);
            }else if(Input.touchCount==0 && !Input.GetMouseButton(0)){
                lr=50;
            }
            if(PlayerPrefs.GetInt("Checkpoint")==1){
                chp=ch;
                ch=Input.GetAxis("Checkpoint");
                if(ch>0.5&&chp<0.5)checkp();
                if(ch>-0.5&&chp<-0.5)revcheckp();
            }
        }
    }
    public void checkp(){
        foreach(var i in GameObject.FindGameObjectsWithTag("checkpoint")){
            Destroy(i);
        }
        
        Transform a=Instantiate(jatr,checkpoints,true);
        a.gameObject.tag="checkpoint";
        a.GetComponent<Rigidbody2D>().velocity=ja.velocity;
        a.GetComponent<Rigidbody2D>().angularVelocity=ja.angularVelocity;
        a.GetComponent<Rigidbody2D>().simulated=false;
        a.GetComponent<SpriteRenderer>().sprite=litevlaj;
        a.GetComponent<Collider2D>().enabled=false;
    }
    public void revcheckp(){
        GameObject a=GameObject.FindGameObjectWithTag("checkpoint");
        jatr.position=a.transform.position;
        ja.velocity=a.GetComponent<Rigidbody2D>().velocity;
        ja.angularVelocity=a.GetComponent<Rigidbody2D>().angularVelocity;
    }
    public void Pause(bool a){
        paused=a;
        ja.simulated=!a;
        sprite.gameObject.GetComponent<Animator>().enabled=!a;
    }
    void FixedUpdate(){
        if(ja.simulated&&!somtutor)ControlsUpdate();
        if(!paused){
            if(lr>50){
                if(sprite.flipX){
                    sprite.gameObject.GetComponent<Animator>().SetTrigger("otoč");
                    sprite.flipX=false;
                }
            }else if(lr<50){
                if(!sprite.flipX){
                    sprite.gameObject.GetComponent<Animator>().SetTrigger("otoč");
                    sprite.flipX=true;
                }
            }
        }
    }
    public void ControlsUpdate(){
        velopred=ja.velocity.x;
        ja.AddTorque(-add*(lr-50)/40,ForceMode2D.Impulse);
        if(u&&!up&&jnumber>0){
            ja.AddForce(new Vector2(0,jump),ForceMode2D.Impulse);
            jnumber--;
        }
        up=u;
        if(!somtutor&&!(uptl||Input.GetAxis("Jump")>0.5))u=false;
        if(!somtutor&&casovac!=null)casovac.save();
    }
}
