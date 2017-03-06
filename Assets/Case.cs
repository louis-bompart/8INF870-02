using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    private List<Case> neighbors;
    public int x;
    public int y;
    // Use this for initialization
    void Start()
    {
    }

    public Case SetCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
        return this;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void CreateOrGetNeighbors()
    {
        neighbors = new List<Case>();
        List<Case> toCompute = new List<Case>();
        for (int i = -1; i < 2; i += 2)
        {
            if (x + i > 0 && x + i < Map.xSize)
            {
                Case tmp = Map.map[x + i, y];
                if (tmp == null)
                {
                    tmp = Instantiate(x + i, y);
                    Map.map[x + i, y] = tmp;
                    toCompute.Add(tmp);
                }
                neighbors.Add(tmp);
            }
            if (y + i > 0 && y + i < Map.ySize)
            {
                Case tmp = Map.map[x, y + i];
                if (tmp == null)
                {
                    tmp = Instantiate(x, y + i);
                    Map.map[x, y + i] = tmp;
                    toCompute.Add(tmp);
                }
                neighbors.Add(tmp);
            }
        }
        foreach (Case item in toCompute)
        {
            item.CreateOrGetNeighbors();
        }
    }

    private Case Instantiate(int x, int y)
    {
        GameObject toInstantiate = Map.EmptyCase;
        if (Random.value > 1f - (float)Map.remainingBonus / (float)Map.remainingCase)
        {
            toInstantiate = Map.Bonus;
            Map.remainingBonus--;
        }
        Map.remainingCase--;
        Vector3 position = transform.position;
        position += (this.x - x) * Vector3.left;
        position += (this.y - y) * Vector3.up;
        return Instantiate(toInstantiate, position, this.transform.rotation).GetComponent<Case>().SetCoordinates(x, y);
    }
}
