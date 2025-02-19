using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    GameManager gameManager;

    public GameObject gameChat;
    public TextMeshProUGUI gameChatText;
    public GameObject mainChat;
    public TextMeshProUGUI mainChatText;

    public TextMeshProUGUI timeText; // 시간 표시
    private float timer = 0f;
    public float Game1Maxtime;
    public float Game2Maxtime;

    public string maxGame1TimeTxt;
    public string maxGame2TimeTxt;


    
    public GameObject dungeon1Btn;
    public GameObject dungeon2Btn;

    private void Awake()
    {
        gameManager = GameManager.Instance;

        Instance = this;
        
    }

    private void Update()
    {
        if(!gameManager.isGameStart1 && !gameManager.isGameStart2)
        {
            return;
        }

        timer += Time.deltaTime;


        if (gameManager.isGameStart1)
        {
            if (timer > Game1Maxtime)
            {
                Game1Maxtime = timer;
            }
        }

        if (gameManager.isGameStart2)
        {
            if (timer > Game1Maxtime)
            {
                Game2Maxtime = timer;
            }
        }

        UpdateTimer();
    }

    private void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        if(gameManager.isDungeon1)
        {
            int maxMinutes = Mathf.FloorToInt(Game1Maxtime / 60);
            int maxseconds = Mathf.FloorToInt(Game1Maxtime % 60);
            maxGame1TimeTxt = string.Format("최고 기록 : {0:00} : {1:00}  현재 기록 : {2:00} : {3:00}", maxMinutes, maxseconds, minutes, seconds);
        }
       
        timeText.text = string.Format("{0:00} : {1:00} ", minutes, seconds);
    }

    public void Dungeon1Start()
    {
        gameManager.isGameStart1 = true;
        dungeon1Btn.SetActive(false);
        gameChat.SetActive(false);
        timer = 0f;
        PlayerController player = gameManager.Player.GetComponent<PlayerController>();
        player.isDead = false;
        AnimationHandler anim = gameManager.Player.GetComponent<AnimationHandler>();
        anim.InvincibilityEnd();

        Debug.Log(1);
    }

    public void Dungeon2Start()
    {
        gameManager.isGameStart2 = true;
        dungeon2Btn.SetActive(false);
        timer = 0f;
        PlayerController player = gameManager.Player.GetComponent<PlayerController>();
        player.isDead = false;
        AnimationHandler anim = gameManager.Player.GetComponent<AnimationHandler>();
        anim.InvincibilityEnd();
    }

    public void Dungeon1Exit()
    {
        gameManager.playerTransform.position = Vector3.zero;
        gameManager.isGameStart1 = false;
        dungeon1Btn.SetActive(false);
        timeText.gameObject.SetActive(false);
        timer = 0f;
        timeText.text = string.Format("{0:00} : {1:00}", timer, timer);
        gameChat.SetActive(false);

        PlayerController player = gameManager.Player.GetComponent<PlayerController>();
        player.isDead = false;
        AnimationHandler anim = gameManager.Player.GetComponent<AnimationHandler>();
        anim.InvincibilityEnd();
        gameManager.isCameraTransitioning1 = false;
        gameManager.isDungeon1 = false;
        Debug.Log(1);
    }
    public void Dungeon2Exit()
    {
        gameManager.playerTransform.position = Vector3.zero;
        gameManager.isGameStart2 = false;
        dungeon2Btn.SetActive(false);
        timeText.gameObject.SetActive(false);
        timer = 0f;
        timeText.text = string.Format("{0:00} : {1:00}", timer, timer);

        PlayerController player = gameManager.Player.GetComponent<PlayerController>();
        player.isDead = false;
        AnimationHandler anim = gameManager.Player.GetComponent<AnimationHandler>();
        anim.InvincibilityEnd();
        gameManager.isCameraTransitioning2 = false;
    }
}
