﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Key : UdonSharpBehaviour
{
    KeyBoard keyboard;
    Animator animator;
    string note;
    bool initialized = false;

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        keyboard = GetComponentInParent<KeyBoard>();
        animator = GetComponent<Animator>();
        note = gameObject.name;
        initialized = true;
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (!initialized) Initialize();
        animator.SetTrigger("light");
        keyboard.PlayNote(note);        
    }

    public void Highlight()
    {
        animator.SetTrigger("light");
    }
}
