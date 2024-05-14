using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource buttonSFX;
    bool isPressed;

    void Start()
    {
        buttonSFX = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0.26f, 0.415f, 0.7f);
            presser = other.gameObject;
            onPress.Invoke();
            buttonSFX.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0.26f, 0.43f, 0.7f);
            onRelease.Invoke();
            isPressed = false;
        }
    }
}
