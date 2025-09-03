using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Threading.Tasks;

public class pozgen : MonoBehaviour
{
    public int amount;
    public float maxx;
    public float maxy;
    public float minhl;
    public float maxhl;
    public int mina;
    public int maxa;
    public float minsc;
    public float maxsc;
    public List<GameObject> ostrovy;
    void Start(){
        generate();
    }
    async void generate(){
        for (int i = 0; i < amount; i++){
            try{
                GameObject nový=Instantiate(ostrovy[Random.Range(0,ostrovy.Count)],gameObject.GetComponentsInParent<Transform>()[1]);
                pozadie n=nový.GetComponent<pozadie>();
                float hĺbka=Random.Range(minhl,maxhl);
                n.multi=hĺbka;
                n.x=Random.Range(-maxx,maxx);
                n.y=Random.Range(-maxy,maxy);
                n.z=20-hĺbka;
                nový.GetComponent<Tilemap>().color=new Color(255,255,255,((maxa-mina)*((hĺbka-minhl)/(maxhl-minhl))+mina)/255);
                nový.transform.localScale=new Vector3((maxsc-minsc)*((hĺbka-minhl)/(maxhl-minhl))+minsc,(maxsc-minsc)*((hĺbka-minhl)/(maxhl-minhl))+minsc,(maxsc-minsc)*((hĺbka-minhl)/(maxhl-minhl))+minsc);
                await Task.Yield();
            }catch{}
        }

    }
}
