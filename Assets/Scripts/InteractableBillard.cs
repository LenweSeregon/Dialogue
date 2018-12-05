﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBillard : MonoBehaviour, Interactable {
    public Pool.Main poolMain;

    public string GetInteractableText()
    {
        return "Interagir : " + GetKeyInteract().ToString();
    }

    public KeyCode GetKeyInteract()
    {
        return KeyCode.F;
    }

    public void Interact()
    {
        poolMain.loadGame();
    }

    public bool IsInteractable()
    {
        return true;
    }

    public bool IsNPC()
    {
        return false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}