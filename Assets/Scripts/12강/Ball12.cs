using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball12 : MonoBehaviour
{
    private bool canGetScoll = false;

    public bool isOneTime = false;

    private void Start()
    {
        if (gameObject.tag == "CommonBall")
        {
            canGetScoll = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        string playerTag = GameManager12.Instance.playerName;

        Debug.Log(isOneTime);

        if (!canGetScoll) return;

        if (collision.gameObject.CompareTag(playerTag) && !isOneTime)
        {
            GameManager12.Instance.SetScoll();
            GameManager12.Instance.ReCheckEndWaitting();

            isOneTime = true;
        }
    }
}
