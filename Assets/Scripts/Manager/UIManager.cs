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
    public BgLooper bgLooper;

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

    public GameObject CharacterBtn;

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
            if (timer > Game2Maxtime)
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
        if (gameManager.isDungeon2)
        {
            int maxMinutes = Mathf.FloorToInt(Game2Maxtime / 60);
            int maxseconds = Mathf.FloorToInt(Game2Maxtime % 60);
            maxGame2TimeTxt = string.Format("최고 기록 : {0:00} : {1:00}  현재 기록 : {2:00} : {3:00}", maxMinutes, maxseconds, minutes, seconds);
        }

        timeText.text = string.Format("{0:00} : {1:00} ", minutes, seconds);
    }

    public void Dungeon1Start()
    {
        gameManager.playerTransform.position = gameManager.Dungeon1_Point.position;
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
        gameManager.playerTransform.position = gameManager.Dungeon2_Point.position;
        gameManager.playerTransform.rotation = Quaternion.identity;
        bgLooper.ResetPositions();
        bgLooper.StartPositions();
        gameManager.isDungeon2PositionSet = false;
        gameManager.isGameStart2 = true;
        dungeon2Btn.SetActive(false);
        gameChat.SetActive(false);
        timer = 0f;
        PlayerController player = gameManager.Player.GetComponent<PlayerController>();
        player.isDead = false;
        player.rigid.gravityScale = 1f;
        AnimationHandler anim = gameManager.Player.GetComponent<AnimationHandler>();
        anim.InvincibilityEnd();
        Debug.Log(1);
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
        gameManager.Dungeon1_Object.SetActive(false);
        Debug.Log(1);
    }
    public void Dungeon2Exit()
    {
        bgLooper.ResetPositions();
        gameManager.playerTransform.position = Vector3.zero;
        gameManager.playerTransform.rotation = Quaternion.identity;
        gameManager.isDungeon2PositionSet = false;
        gameManager.isGameStart2 = false;
        dungeon2Btn.SetActive(false);
        timeText.gameObject.SetActive(false);
        timer = 0f;
        timeText.text = string.Format("{0:00} : {1:00}", timer, timer);
        gameChat.SetActive(false);

        PlayerController player = gameManager.Player.GetComponent<PlayerController>();
        player.isDead = false;
        player.rigid.gravityScale = 0f;
        AnimationHandler anim = gameManager.Player.GetComponent<AnimationHandler>();
        anim.InvincibilityEnd();
        gameManager.isCameraTransitioning2 = false;
        gameManager.isDungeon2 = false;
        gameManager.Dungeon2_Object.SetActive(false);
        Debug.Log(1);
    }

    public void CharacterChange(CharacterType newCharacter)
    {
        BaseController character = gameManager.Player.GetComponent<BaseController>();

        GameObject[] characters = {character.KnightRenderer.gameObject, character.ElfRenderer.gameObject};

        foreach (GameObject obj in characters)
        {
            obj.SetActive(false);
        }

        characters[(int)newCharacter].SetActive(true);
        gameManager.currentCharacter = newCharacter;
    }

    public void SelectElf()
    {
        CharacterChange(CharacterType.Elf);
        gameManager.isElf = true;
        gameManager.isKnight = false;
    }

    public void SelectKnight()
    {
        CharacterChange(CharacterType.Knight);
        gameManager.isElf = false;
        gameManager.isKnight = true;
    }
}
