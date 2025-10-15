using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task6_TurnSystem : MonoBehaviour
{
    private List<Player> players = new List<Player>();
    private Test6_PriorityQueue<string> attackQueue;

    private int turnCount = 1;
    void Start()
    {
        players.Add(new Player("전사", 5, 0));
        players.Add(new Player("마법사", 7, 0));
        players.Add(new Player("궁수", 10, 0));
        players.Add(new Player("도적", 12, 0));

        attackQueue = new Test6_PriorityQueue<string>();
        foreach (var player in players)
            attackQueue.Enqueue(player.playerName, GetPriority(player.attackCount, player.attackSpeed));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayOneTurn();
        }
    }

    private void PlayOneTurn()
    {
        string targetPlayer = attackQueue.Dequeue();
        Debug.Log($"{turnCount}턴/{targetPlayer}의 턴입니다");

        Player player = players.Find(p => p.playerName == targetPlayer);
        player.AddAttackCount();
        attackQueue.Enqueue(player.playerName, AddedPriority(player.attackCount, player.attackSpeed));
        turnCount++;
    }

    private float GetPriority(int count, int speed )
    {
        Debug.Log(Mathf.Pow((count + 1) * 1f, 2) / speed);
        return Mathf.Pow((count + 1) * 1f, 2) / speed;
    }

    private float AddedPriority(int count, int speed)
    {
        Debug.Log(Mathf.Pow((count + 1) * 2f, 2) / speed);
        return Mathf.Pow((count + 1) * 2f, 2) / speed;
    }
}

public class Player
{
    public string playerName { get; private set; }
    public int attackSpeed { get; private set; }
    public int attackCount {  get; private set; }

    public Player(string playerName, int attackSpeed, int attackCount)
    {
        this.playerName = playerName;
        this.attackSpeed = attackSpeed;
        this.attackCount = attackCount;
    }

    public void AddAttackCount()
    {
        attackCount++;
    }
}