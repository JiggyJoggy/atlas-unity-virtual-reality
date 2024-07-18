using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARStartBNT : MonoBehaviour
{
    public GameObject startBNT;
    public GameObject ammo;
    public Camera cam;

    public GameObject enitreScreenUI;

    public void StartingGame()
    {
        startBNT.SetActive(false);

        enitreScreenUI.SetActive(true);

        Vector3 spawnPosition = cam.transform.position + cam.transform.forward * 2.0f;
        GameObject instantiatedAmmo = Instantiate(ammo, spawnPosition, Quaternion.identity);

        Rigidbody ammoRigidbody = instantiatedAmmo.GetComponent<Rigidbody>();
        if (ammoRigidbody != null)
        {
            ammoRigidbody.useGravity = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}