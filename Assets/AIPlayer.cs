using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    public List<Case> path;
    int maxScore;
    Bonus stubBonus;
    Bonus nextBonus;
    // Use this for initialization
    void Start()
    {
        maxScore = 0;
        path = new List<Case>();
        stubBonus = gameObject.GetComponent<Bonus>();
        stubBonus.SetCoordinates(0, 0);
        maxScore = stubBonus.ReachableBonus(0);
        nextBonus = stubBonus.bestNext;
        ComputePath();

    }

    void ComputePath()
    {
        while (nextBonus != null)
        {
            path.Add(nextBonus);
            nextBonus.ReachableBonus(path.Count);
            nextBonus = nextBonus.bestNext;
        }
        maxScore = path.Count;
        path.Add(Map.map[Map.xSize-1, Map.ySize-1]);
        Debug.Log("Path complete");
        Debug.Log("AI Score :"+maxScore);
        TravelPath();
    }

    void TravelPath()
    {
        for (int i = 0; i < path.Count; i++)
        {
            while (x < path[i].x)
            {
                MoveRight();
            }
            while (y < path[i].y)
            {
                MoveDown();
            }
        }
        Instantiate(trail, transform.position, transform.rotation, transform.parent);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
