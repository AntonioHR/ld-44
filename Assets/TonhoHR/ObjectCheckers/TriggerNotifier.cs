using UnityEngine;
using System.Collections;
using System;

public class TriggerNotifier : MonoBehaviour
{
    public LayerMask layerMask;


    public event Action<GameObject> ObjectEntered;
    public event Action<GameObject> ObjectLeft;

    private void OnTriggerEnter(Collider other)
    {
        FireObjectEntered(other.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FireObjectEntered(collision.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        FireObjectLeft(other.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FireObjectLeft(collision.gameObject);
    }

    private void FireObjectLeft(GameObject gameObject)
    {
        if (ObjectLeft != null)
            ObjectLeft(gameObject);
    }

    private void FireObjectEntered(GameObject gameObject)
    {
        if (ObjectEntered != null)
            ObjectEntered(gameObject);
    }
}
