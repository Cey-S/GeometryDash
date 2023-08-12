using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public delegate void OnPortalEnter();
    public static event OnPortalEnter portalEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Player"))
        {
            portalEntered?.Invoke();
        }
    }    
}
