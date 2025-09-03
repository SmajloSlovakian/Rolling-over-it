using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runshow : MonoBehaviour
{
    public string rundata;
    public string lvl;
    public GameObject showrunner;
    public void ShowRun(){
        GameObject i=Instantiate(showrunner);
        DontDestroyOnLoad(i);
        i.name="RunShowData";
        i.GetComponent<showrunner>().showdata=rundata;
        i.GetComponent<showrunner>().cammed=true;
        vlajka g=GameObject.FindGameObjectWithTag("vlajka").GetComponent<vlajka>();
        g.load(lvl);
    }
}
