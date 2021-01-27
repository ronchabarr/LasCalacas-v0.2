using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericController : MonoBehaviour
{
    InputForm inputForm;
    bool attackState;
    
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inputForm = new InputForm();

       


    }

    public InputForm GetInputForm()
    {
        
        inputForm.moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputForm.rotVector = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));


        Dash();
        //Jump Currently Disabled due to gamedesign 
        //jump();
        Attacks();
        Skills();
        Gestures();
        Emotes();
        Shield();


        return inputForm;
    }

   

    public void jump()
    {
        inputForm.jumpPress = Input.GetKeyDown(KeyCode.Space);
    }
    public void Dash()
    {

        inputForm.Dash=Input.GetKeyDown(KeyCode.LeftShift);
   
    }
    public void Skills()
    {
        //currently there are 2 (posibility for 5 in code)

        inputForm.skills[0, 0] = Input.GetKeyDown(KeyCode.Q);
        inputForm.skills[1, 0] = Input.GetKeyDown(KeyCode.E);
        inputForm.skills[2, 0] = Input.GetKeyDown(KeyCode.R);
        inputForm.skills[3, 0] = Input.GetKeyDown(KeyCode.T);
        //inputForm.skills[4, 0] = Input.GetKeyDown(KeyCode.Y);
    }
    public void Gestures()
    {
        inputForm.gesture[0] = Input.GetKeyDown(KeyCode.Alpha1);
        inputForm.gesture[1] = Input.GetKeyDown(KeyCode.Alpha2);
        inputForm.gesture[2] = Input.GetKeyDown(KeyCode.Alpha3);
    }
    private void Emotes()
    {
        inputForm.emote[0] = Input.GetKeyDown(KeyCode.I);
        inputForm.emote[1] = Input.GetKeyDown(KeyCode.O);
        inputForm.emote[2] = Input.GetKeyDown(KeyCode.P);
    }

    public void Attacks()
    {
        inputForm.attacks[0, 0] = Input.GetMouseButtonDown(0);
        inputForm.attacks[1, 0] = Input.GetMouseButtonDown(1);

        //3rd input attack Down(not compatable with game design)
        //
        //inputForm.attacks[2, 0] = Input.GetMouseButtonDown(1) && Input.GetMouseButtonDown(0);
    }
    public void Shield()
    {
        inputForm.shieldPress = Input.GetKeyDown(KeyCode.V); 
    }
}
