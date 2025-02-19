using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public Transform playerTransform;
    public GameObject Player;
    public GameObject Dungeon1_Object;
    public Transform Dungeon1_Point;

    public GameObject Dungeon2_Object;
    public Transform Dungeon2_Point;
    public float offsetX;
    public bool isDungeon1 = false;
    public bool isDungeon2 = false;


    public bool isCameraTransitioning1 = false;
    public bool isCameraTransitioning2 = false;
    public bool isDungeon2PositionSet = false;

    public bool isGameStart1 = false;
    public bool isGameStart2 = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
