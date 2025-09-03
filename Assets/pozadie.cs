using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pozadie : MonoBehaviour
{
    GameObject kam;
    public float multi;
    public float x;
    public float y;
    public float z;
    void Start(){
        kam=GameObject.FindGameObjectWithTag("MainCamera");
        Update();
    }

    void Update(){
        gameObject.transform.position=new Vector3((kam.transform.position.x+x)/multi,(kam.transform.position.y+y)/multi,z);
    }
}
