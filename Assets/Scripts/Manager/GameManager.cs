using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Dungeon1_Object;
    public Transform Dungeon1_Point;

    public GameObject Dungeon2_Object;
    public Transform Dungeon2_Point;
    public float offsetX;
    public bool isDungeon2 = false;


    public bool isCameraTransitioning1 = false;
    public bool isCameraTransitioning2 = false;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
