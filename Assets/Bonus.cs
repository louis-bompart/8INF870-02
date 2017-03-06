using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : Case
{

    public Bonus bestNext;
    // Use this for initialization
    void Start()
    {
        bestNext = null;
    }

    // Update is called once per frame
    void Update()
    {

    }



    public int ReachableBonus(int scorePossible)
    {
        scorePossible++;
        int currentNextMax = scorePossible;
        for (int i = x; i < Map.xSize; i++)
        {
            for (int j = y; j < Map.ySize; j++)
            {
                if (Map.map[i, j] != this)
                {
                    if (Map.map[i, j] is Bonus)
                    {
                        int tmp = (Map.map[i, j] as Bonus).ReachableBonus(scorePossible);
                        if (tmp > currentNextMax)
                        {
                            currentNextMax = tmp;
                            bestNext = (Map.map[i, j] as Bonus);
                        }
                    }
                }
            }
        }
        return currentNextMax ;
    }
}
