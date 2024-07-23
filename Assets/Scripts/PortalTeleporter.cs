using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    private bool overlapping = false;

    void Update()
    {
        if (overlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dot = Vector3.Dot(transform.up, portalToPlayer);

            if (dot < 0)
            {
                float rotDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotDiff += 180;
                player.Rotate(Vector3.up, rotDiff);

                Vector3 posOffset = Quaternion.Euler(0, rotDiff, 0) * portalToPlayer;
                player.position = receiver.position + posOffset;

                overlapping = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController)
        {
            overlapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController)
        {
            overlapping = false;
        }
    }
}
