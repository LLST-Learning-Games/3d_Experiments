using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    [SerializeField] private GameObject _inAreaHighlight;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter" + other.gameObject.name);
        if (other.tag == "Player")
        {
            _inAreaHighlight.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit" + other.gameObject.name);
        if (other.tag == "Player")
        {
            _inAreaHighlight.SetActive(false);
        }
    }
}
