using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver4 : MonoBehaviour
{
    public GameObject gameOverUI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameOverUI.SetActive(true);
            GameObject.FindWithTag("Player").gameObject.SetActive(false);
        }
    }
}
