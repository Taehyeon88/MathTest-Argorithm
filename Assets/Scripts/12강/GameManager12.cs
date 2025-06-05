using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager12 : MonoBehaviour
{
    public static GameManager12 Instance;

    public List<GameObject> players = new List<GameObject>();

    public string playerName { get; private set; }
    public bool isWaiting {  get; private set; }

    private int playerId = 1;
    private UiManager12 uiManager;
    private GameObject turnPlayer;

    private int[] playerScolls;

    public Rigidbody[] ballsRb;
    private int checkingCount;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        turnPlayer = players[0];
        playerScolls = new int[players.Count];
        uiManager = FindObjectOfType<UiManager12>();

        StartTurn();
    }

    public void SetScoll()
    {
        playerScolls[playerId - 1] += 1;
        uiManager.SetPlayerScoll(playerId, playerScolls[playerId - 1]);
    }

    public void StartWaiting()
    {
        StartCoroutine(WaitingTurn());
    }

    public void ReCheckEndWaitting()
    {
        checkingCount = ballsRb.Length;
    }

    void StartTurn()
    {
        uiManager.SetPlayerName(playerId);
        playerName = $"Player{playerId}";
    }

    void ChangeTurn()
    {
        playerId ++;

        if(playerId > players.Count) playerId = 1;

        turnPlayer = players[players.Count - 1];

        StartTurn();
    }

    void ResetBalls()
    {
        foreach (var ball in ballsRb)
        {
            ball.gameObject.GetComponent<Ball12>().isOneTime = false;
        }
    }
    IEnumerator WaitingTurn()
    {
        isWaiting = true;
        uiManager.SetWaitting();

        checkingCount = ballsRb.Length;

        yield return new WaitForSeconds(3f);
        while (isWaiting)
        {
            if (ballsRb[checkingCount - 1].velocity.magnitude < 0.05f)
            {
                checkingCount--;
                if (checkingCount <= 0)
                {
                    isWaiting = false;
                }
            }
            yield return null;
        }
        ChangeTurn();
        ResetBalls();
    }
}
