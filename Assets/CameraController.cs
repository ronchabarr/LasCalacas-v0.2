using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public Vector3 Offset;
    public float Speed = 10f;
    [SerializeField]
    private Space offsetPositionSpace = Space.Self;
    [SerializeField]
    private bool lookAt = true;
    // Update is called once per frame


    private void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (Player == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = Player.TransformPoint(Offset);
        }
        else
        {
            transform.position = Player.position + Offset;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(Player);
        }
        else
        {
            transform.rotation = Player.rotation;
        }

        float XMove = Input.GetAxis("Horizontal");
        float YMove = Input.GetAxis("Vertical");

        





    }

}
