using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadSpriteSwap : MonoBehaviour
{
    public GameObject ForKeyBoard;
    public GameObject ForMouse;
    public GameObject ForGamePad;
    float pgpc=0,pmc=0,pkbc=0;
    void Start(){
        if(ForGamePad!=null)ForGamePad.SetActive(false);
        if(ForKeyBoard!=null)ForKeyBoard.SetActive(false);
            if(ForMouse!=null)ForMouse.SetActive(true);
    }
    void FixedUpdate(){
        if(Input.GetAxis("Gamepadcheck")!=pgpc){
            if(ForKeyBoard!=null)ForKeyBoard.SetActive(false);
            if(ForMouse!=null)ForMouse.SetActive(false);
            if(ForGamePad!=null)ForGamePad.SetActive(true);
        }
        if(Input.GetAxis("Mousecheck")!=pmc){
            if(ForGamePad!=null)ForGamePad.SetActive(false);
            if(ForKeyBoard!=null)ForKeyBoard.SetActive(false);
            if(ForMouse!=null)ForMouse.SetActive(true);
        }
        if(Input.GetAxis("Keyboardcheck")!=pkbc){
            if(ForGamePad!=null)ForGamePad.SetActive(false);
            if(ForMouse!=null)ForMouse.SetActive(false);
            if(ForKeyBoard!=null)ForKeyBoard.SetActive(true);
        }
        pgpc=Input.GetAxis("Gamepadcheck");
        pmc=Input.GetAxis("Mousecheck");
        pkbc=Input.GetAxis("Keyboardcheck");
    }
}
