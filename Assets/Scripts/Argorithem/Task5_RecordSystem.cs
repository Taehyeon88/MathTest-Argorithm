using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Task5_RecordSystem : MonoBehaviour
{
    [SerializeField] private Button recordButton;
    [SerializeField] private Button playButton;
    [SerializeField] private TextMeshProUGUI queueText;
    [SerializeField] private Task5_EcoMove ecoMoveController;
    [SerializeField] private GameObject shadowPrf;

    public float speed = 5f;
    private bool isRecording;
    private bool isPlaying;
    private Renderer ecoRenderer;

    private Queue<(Vector3, bool)> recordPlayerMoveHistory = new Queue<(Vector3, bool)>();


    private void Start()
    {
        recordButton.onClick.AddListener(() => {
            if (!isPlaying)
                isRecording = true;
        });
        playButton.onClick.AddListener(() => {

            isPlaying = true;
            isRecording = false;
        });

        ecoRenderer = ecoMoveController.gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        if (isRecording)
        {
            Record();
        }

        if (isPlaying)
        {
            Play();
        }

        SetQueueCount();
    }


    private void Record()
    {
        if (ecoMoveController.returnMoveHistory.Count <= 0) return;

        Debug.Log(ecoMoveController.returnMoveHistory.Peek());
        recordPlayerMoveHistory.Enqueue((ecoMoveController.returnMoveHistory.Peek(), ecoMoveController.isReturnning));
    }

    private void Play()
    {
        if (recordPlayerMoveHistory.Count <= 0)
        {
            isPlaying = false;
            return;
        }

        //Debug.Log(recordPlayerMoveHistory.Peek());
        Vector3 targetPos = recordPlayerMoveHistory.Peek().Item1;
        ecoMoveController.gameObject.transform.position = targetPos;

        bool isReturning = recordPlayerMoveHistory.Peek().Item2;
        if (isReturning) ecoRenderer.material.color = Color.blue;
        else ecoRenderer.material.color = Color.white;

        recordPlayerMoveHistory.Dequeue();
    }

    private void SetQueueCount()
    {
        queueText.text = $"QueueCount: {recordPlayerMoveHistory.Count}";
    }
}
