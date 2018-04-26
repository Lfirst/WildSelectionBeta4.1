﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveS : MonoBehaviour {
    //Appel aux Objets du jeu
    public CharacterController CharControl;
    public Transform Body;
    public Animator anim;

    //Basics
    public float WalkSpeed = 4 ;
    public float RunSpeed = 6;
    
    //Crouch
    private float Slide = 0;
    public float SlideTime = 15;
    bool crouched = false;
    private float WaitingTime = 10;
    public float CrouchSpeed = 2;

    //Jump
    public float jumpforce = 10f;
    private float jumpVelocity;
    public float gravity = 30f;
    bool jump = false;
    bool jumping = false;

    // Use this for initialization
    void Awake()
    {
        CharControl = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Crouch();
        MovePlayer();
        Jump();
        
	}
    void MovePlayer()
    {
        //Recupération des touches 
        float horiz = Input.GetAxis("Horizontal");
        float run = Input.GetAxis("Run");
        float vert = Input.GetAxis("Vertical");
        //Déclaration des Vectors3
        Vector3 moveDirSide, moveDireForward;
        
        //Switch entre crouch et normal + décalage du Slide
        if (CharControl.isGrounded && crouched && (Slide == SlideTime || run <= 0))
        {
            moveDirSide = transform.right * horiz * CrouchSpeed;
            moveDireForward = transform.forward * vert * CrouchSpeed;
        }
        else
        {
            moveDirSide = transform.right * horiz * (WalkSpeed + run * RunSpeed);
            moveDireForward = transform.forward * vert * (WalkSpeed + run * RunSpeed);
        }
        //Conditions de Jump et création des forces
        // Au sol
        if (CharControl.isGrounded)
        {
            jumpVelocity = -gravity * Time.deltaTime;
            //si appuyer sur space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpVelocity = jumpforce;
            }
        }
        else
        {
            jumpVelocity -= gravity * Time.deltaTime;
        }

        // Slide time
        if (Slide < SlideTime && crouched)
        {
            Slide += 0.5f;
            
        }
        else if (Slide == SlideTime && !crouched)
        {
            Slide = 0;
        }

        Vector3 moveDirUp = Vector3.zero ;

        //Affect les forces au joueurs
        moveDirUp.x = moveDirSide.x + moveDireForward.x;
        moveDirUp.y = jumpVelocity + moveDirSide.y + moveDireForward.y;
        moveDirUp.z = moveDireForward.z + moveDirSide.z;
        CharControl.Move(moveDirUp *Time.deltaTime);

    }
    //Paramètres Animation Jump
    void Jump()
    {
        anim.SetBool("Grounded", CharControl.isGrounded);
        anim.SetFloat("AirVelocity", jumpVelocity);
    }


    //Move Crouch
    void Crouch()
    {
        //Modification de la taille du personnage et reposition du corp graphic

        if (Input.GetKey(KeyCode.A) && WaitingTime >= SlideTime+5)
        {
            if (!crouched)
            {
                CharControl.height = 1;
                crouched = true;
                WaitingTime = 0;
                Body.transform.position += new Vector3(0, 0.32f, 0);
            }
            else
            {
                crouched = false;
                WaitingTime = 0;
                Body.transform.position += new Vector3(0, -0.32f, 0);
            }
        }

        // Temps d'attente entre deux ctrl 
        // Slide
        if (WaitingTime < SlideTime + 5)
        {
            WaitingTime += 0.5f;

                
        }
        //Non Slide
        float run = Input.GetAxis("Run");
        float vert = Input.GetAxis("Vertical");
        if (run <= 0 || vert == 0)
        {
            WaitingTime += 0.5f;
        }
        //Transition de Crouch vers Normal
        if (!crouched && CharControl.height < 2f)
        {
            CharControl.height += 0.1f;
        }
        if(CharControl.height == 2)
        {
            crouched = false;
        }
        
        
        //Paramètre d'animation Crouch

        crouched = CharControl.height == 1;
        anim.SetBool("Crouched", crouched);
    }
    }
