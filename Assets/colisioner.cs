using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class colisioner : MonoBehaviour
{
    controller c;
    bool incol=false;
    float collamt=0;
    bool isrumon;
    int predcont=0;
    private void Start() {
        c=gameObject.GetComponentInParent<controller>();
        isrumon=PlayerPrefs.GetInt("Rumble")==1;
    }
    void FixedUpdate(){
        if(isrumon&&!c.somtutor){
            if(collamt>1){
                GamePad.SetVibration(0,1,1);
                collamt/=5;
                //Handheld.Vibrate();
            }else if(incol){
                GamePad.SetVibration(0,(Mathf.Abs(c.ja.angularVelocity/360f*11.18406f)/2f-Mathf.Abs(c.ja.velocity.magnitude))/10,0);
            }else{
                GamePad.SetVibration(0,0,0);
            }
        }
    }
    void OnCollisionExit2D(Collision2D col) {
        incol=false;
    }
    private void OnCollisionStay2D(Collision2D col) {
        if(col.gameObject.tag=="skok"&&col.contactCount>predcont){
            c.jnumber=1;
            collamt=Mathf.Pow(col.contacts[col.contactCount-1].normalImpulse,2);
        }
        predcont=col.contactCount;
    }
    void OnCollisionEnter2D(Collision2D col){
        incol=true;
        if(col.gameObject.tag=="skok"){
            c.jnumber=1;
        }
        for(int i = 0; i < col.contactCount; i++){
            var kontakt = col.GetContact(0);
            collamt=Mathf.Pow(kontakt.normalImpulse,2);
            if(c.u)if(c.velopred>10 || c.velopred<-10){
                if(kontakt.point.x>c.jatr.position.x+0.8 && c.lr<50){
                    c.ja.velocity=new Vector2(-15,20);
                }else if(kontakt.point.x<c.jatr.position.x-0.8 && c.lr>50){
                    c.ja.velocity=new Vector2(15,20);
                }
            }
        }
    }
}
