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


        run();
        jump();
        Attacks();
        Skills();
        Gestures();
        Emotes();


        return inputForm;
    }

   

    public void jump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputForm.jumpPress = true;
        }
        else inputForm.jumpPress = false;

    }
    public void run()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!inputForm.runPress)
            {
                inputForm.runPress = true;

            }
            else inputForm.runPress = false;
        }
    }
    public void Skills()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            inputForm.skills[0, 0] = true;
        }
        else inputForm.skills[0, 0] = false;

        if (Input.GetKeyDown(KeyCode.E))
        {
            inputForm.skills[1, 0] = true;
        }
        else inputForm.skills[1, 0] = false;

        if (Input.GetKeyDown(KeyCode.R))
        {
            inputForm.skills[2, 0] = true;
        }
        else inputForm.skills[2, 0] = false;

        if (Input.GetKeyDown(KeyCode.T))
        {
            inputForm.skills[3, 0] = true;
        }
        else inputForm.skills[3, 0] = false;

        if (Input.GetKeyDown(KeyCode.Y))
        {
            inputForm.skills[4, 0] = true;
        }
        else inputForm.skills[4, 0] = false;

    }
    public void Gestures()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inputForm.gesture[0] = true;

        }else inputForm.gesture[0] = false;
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inputForm.gesture[1] = true;

        }
        else inputForm.gesture[1] = false;
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inputForm.gesture[2] = true;

        }
        else inputForm.gesture[2] = false;

    }
    private void Emotes()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inputForm.emote[0] = true;
        }
        else inputForm.emote[0] = false;
        if (Input.GetKeyDown(KeyCode.O))
        {
            inputForm.emote[1] = true;
        }
        else inputForm.emote[1] = false;

        if (Input.GetKeyDown(KeyCode.P))
        {
            inputForm.emote[2] = true;
        }
        else inputForm.emote[2] = false;

    }

    public void Attacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inputForm.attacks[0, 0] = true;
        }
        else inputForm.attacks[0, 0] = false;


        if (Input.GetMouseButtonDown(1))
        {
            inputForm.attacks[1, 0] = true;
        }
        else inputForm.attacks[1, 0] = false;

        if (Input.GetMouseButtonDown(1) && Input.GetMouseButtonDown(0))
        {
            inputForm.attacks[0, 0] = false;
            inputForm.attacks[1, 0] = false;
            inputForm.attacks[2, 0] = true;

        }
        else inputForm.attacks[2, 0] = false;

    }
}
