﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody rg;
    private GameObject gfx;

    [SerializeField]
    private float speed = 5.0f;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    [SerializeField]
    private float speedRotation = 2.0f;
    [SerializeField]
    private float distanceInteract = 2.0f;

    [SerializeField]
    private Interactable_NPC_Manager NPC_Manager;
    [SerializeField]
    private Camera TPSCamera;
    private Vector3 localTransformSave;
    private Quaternion localRotationSave;

    [SerializeField]
    private Text interact;
    private Interactable_NPC PNJToInteract;

    public Vector3 velocityGlobal;

	void Start ()
    {
        PNJToInteract = null;
        rg = GetComponent<Rigidbody>();
        FindGfxNode();
        localTransformSave = TPSCamera.transform.localPosition;
        localRotationSave = TPSCamera.transform.localRotation;
    }

    void FindGfxNode()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        foreach(Transform t in transforms)
        {
            if(t.tag == "PlayerGFX")
            {
                gfx = t.gameObject;
                return;
            }
        }
    }
	
	void Update ()
    {
        if (!NPC_Manager.DiscussionInProcess())
        {
            Debug.Log("LOL");
            float x = Input.GetAxisRaw("Horizontal");
            float yRotation = Input.GetAxisRaw("Mouse X");
            float z = Input.GetAxisRaw("Vertical");

            Vector3 verticalMovement = transform.right * x;
            Vector3 horizontalMovement = transform.forward * z;

            Vector3 velocity = (verticalMovement + horizontalMovement).normalized * speed;
            Vector3 rotation = new Vector3(0f, yRotation, 0f) * speedRotation;
            velocityGlobal = velocity;

            PerformMovement(velocity);
            PerformRotation(rotation);

            if(IsNearPNJ())
            {
                Debug.Log("TA MERE");
                interact.gameObject.SetActive(true);
            }
            else
            {
                interact.gameObject.SetActive(false);
            }
        }

        if(interact.enabled && PNJToInteract != null && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("ICI");
            EnterInteractionWithNPC();
        }
    }

    public void EnterInteractionWithNPC()
    {
        interact.gameObject.SetActive(false);
        PNJToInteract.Interact();
        gfx.SetActive(false);
    }

    public void ExitInteractionWithNPC()
    {
        gfx.SetActive(true);
        TPSCamera.transform.localPosition = localTransformSave;
        TPSCamera.transform.localRotation = localRotationSave;
    }

    private bool IsNearPNJ()
    {
        int layerMask = LayerMask.GetMask("NPC");
        RaycastHit hit;
        Vector3 addingToCenter = new Vector3(0f, transform.localScale.y / 2, 0f);
        if (Physics.Raycast(transform.position + addingToCenter, transform.forward, out hit, distanceInteract, layerMask))
        {
            if(hit.transform.tag == "NPC")
            {
                PNJToInteract = hit.transform.gameObject.GetComponent<Interactable_NPC>();
                return true;
            }
        }
        PNJToInteract = null;
        return false;
    }

    void PerformMovement(Vector3 velocity)
    {
        rg.MovePosition(rg.position + velocity * Time.deltaTime);
    }

    void PerformRotation(Vector3 rotation)
    {
        rg.MoveRotation(rg.rotation * Quaternion.Euler(rotation));
    }
}
