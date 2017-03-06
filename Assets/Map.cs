using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    public static Case[,] map;
    public static int xSize;
    public static int ySize;
    public static int remainingBonus;
    public static int remainingCase;
    public int n;
    public int x;
    static int score;
    static bool complete;

    public GameObject emptyCase;
    public GameObject player;
    public GameObject aiPlayer;
    public GameObject bonus;
    public GameObject finish;

    public static GameObject EmptyCase;
    public static GameObject Player;
    public static GameObject Bonus;
    public static GameObject Finish;
    public static GameObject AIPlayer;
    // Use this for initialization
    void Start()
    {
        complete = false;
        AIPlayer = aiPlayer;
        EmptyCase = emptyCase;
        Player = player;
        Bonus = bonus;
        Finish = finish;
        xSize = n;
        ySize = n;
        remainingBonus = x;
        remainingCase = n * n - 2;
        map = new Case[xSize, ySize];
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                map[i, j] = null;
            }
        }
        //Create Player in memory
        GameObject startpos = Instantiate(emptyCase);
        Case startInstance = startpos.GetComponent<Case>();
        startInstance.SetCoordinates(0, 0);
        map[0, 0] = startInstance;
        //Create Finish and instantiate it
        Case finishInstance = Instantiate(finish, new Vector3(xSize - 1, -(ySize - 1)), transform.rotation, transform.parent).GetComponent<Case>();
        finishInstance.SetCoordinates(xSize - 1, ySize - 1);
        map[xSize - 1, ySize - 1] = finishInstance;
        //Create map recursively
        startInstance.CreateOrGetNeighbors();
        //Set camera
        Camera.main.transform.position = (startInstance.transform.position + finishInstance.transform.position) / 2 + Vector3.back;
        Camera.main.orthographicSize = n / 2;
        //Instantiate player
        Instantiate(player, startpos.transform.position, startpos.transform.rotation, this.transform.parent);

    }

    public static void Complete(int input)
    {
        if (!complete)
        {
            score = input;
            Instantiate(AIPlayer);
            complete = true;
            Debug.Log("Complete");
            Debug.Log("Your score is :" + score);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
