using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skrecok : MonoBehaviour
{
    public Transform parent;
    Rigidbody2D rigpar;
    Animator ani;
    private void Start(){
        rigpar=parent.GetComponent<Rigidbody2D>();
        ani=GetComponent<Animator>();
    }
    void Update(){
        gameObject.transform.position=new Vector3(parent.transform.position.x,parent.transform.position.y);
        gameObject.transform.eulerAngles=new Vector3(0,0,rigpar.angularVelocity/-100);
        ani.SetFloat("rýchlosť",Mathf.Abs(rigpar.angularVelocity));
    }
}
