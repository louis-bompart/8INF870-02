using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int x;
    public int y;
    public int score;
    bool inputLock;
    public GameObject trail;
    // Use this for initialization
    void Start()
    {
        inputLock = false;
        x = 0;
        y = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && inputLock)
            inputLock = false;
        if (Input.GetAxis("Horizontal") > 0 && !inputLock)
        {
            inputLock = true;
            MoveRight();
        }
        if (Input.GetAxis("Vertical") < 0 && !inputLock)
        {
            inputLock = true;
            MoveDown();
        }
        if (Map.map[x, y] is Finish)
        {
            Map.Complete(score);
        }
    }

    protected void MoveDown()
    {
        if (y + 1 < Map.ySize)
        {
            Instantiate(trail, transform.position, transform.rotation, transform.parent);
            if (Map.map[x, y + 1] is Bonus)
            {
                score++;
            }
            y++;
            transform.position = Map.map[x, y].transform.position;
        }

    }

    protected void MoveRight()
    {
        if (x + 1 < Map.xSize)
        {
            Instantiate(trail, transform.position, transform.rotation, transform.parent);
            if (Map.map[x + 1, y] is Bonus)
            {
                score++;
            }
            x++;
            transform.position = Map.map[x, y].transform.position;
        }
    }
}
