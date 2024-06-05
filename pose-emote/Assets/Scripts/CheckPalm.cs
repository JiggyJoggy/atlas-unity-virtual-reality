using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPalm : MonoBehaviour
{
    public Image gameObjectImage;
    public Sprite facePalmImage;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Palm"))
        {
            gameObjectImage.sprite = facePalmImage;
        }
    }
}
