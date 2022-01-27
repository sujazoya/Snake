using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[System.Serializable]
public class SnakeItems
{
    public GameObject[] items;
    //public Image levelFill;
    //public Text currentLevel;
    //public Text nexttLevel;

}
public class GameController_Snake : MonoBehaviour
{
   
    float zPose;
    float xPose;
    [SerializeField] SnakeItems items;
    int itemIndex;  
    public static GameController_Snake Instance;
    
    GameMaster gameMaster;
    [SerializeField] GameObject StartPlay;
    [SerializeField] Text number;
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        Game.gameStatus = Game.GameStatus.isInMenu;
    }
    void Start()
    {
        gameMaster = FindObjectOfType<GameMaster>();
        //items.levelFill.fillAmount = 0;
        //for (int i = 0; i < levelItemCount; i++)
        //{
        //    CreateNewItem();
        //}
        SaveGameMode(Game.GameMode);
    }
    public void SaveGameMode(int mode)
    {
        if (mode == 1)
        {
            Game.GameMode = 1;
            gameMaster.gameMode = GameMaster.GameMode.Easy;
        }else

        if (mode == 2)
        {
            Game.GameMode = 2;
            gameMaster.gameMode = GameMaster.GameMode.Medium;
        }
        else if(mode == 3)
        {
            Game.GameMode = 3;
            gameMaster.gameMode = GameMaster.GameMode.Hard;
        }
        else if(mode == 4)
        {
            Game.GameMode = 4;
            gameMaster.gameMode = GameMaster.GameMode.Expert;
        }
        else
        Game.GameMode = 1;
        gameMaster.gameMode = GameMaster.GameMode.Easy;
        gameMaster.CloseLevelPanel();
        
    }   
    
    public void Play()
    {
        if (gameMaster.gameMode == GameMaster.GameMode.Easy)
        {
            Game.levelMinTime = 250;
            Game.levelMaxTime = 350;
            Game.currentLevelTarget = 20;
            for (int i = 0; i < 10; i++)
                CreateNewItem();
        } else
            if (gameMaster.gameMode == GameMaster.GameMode.Medium)
        {
            Game.levelMinTime = 300;
            Game.levelMaxTime = 400;
            Game.currentLevelTarget = 30;
            for (int i = 0; i < 15; i++)
                CreateNewItem();
        }
        else if(gameMaster.gameMode == GameMaster.GameMode.Hard)
        {
            Game.levelMinTime = 350;
            Game.levelMaxTime = 450;
            Game.currentLevelTarget = 40;
            for (int i = 0; i < 15; i++)
                CreateNewItem();
        }
        else if(gameMaster.gameMode == GameMaster.GameMode.Expert)
        {
            Game.levelMinTime = 450;
            Game.levelMaxTime = 550;
            Game.currentLevelTarget = 70;
            for (int i = 0; i < 20; i++)
                CreateNewItem();
        }
        gameMaster.ShowUI(Game.HUD);
        StartCoroutine(LatePlay());
    }
    IEnumerator CheckForItems()
    {
        yield return new WaitForSeconds(5);
        if (Game.achivedLevelTarget < Game.currentLevelTarget)
        {
            for (int i = 0; i < 3; i++)
            {
                CreateNewItem();
            }
        }
        StartCoroutine(CheckForItems());
    }
    IEnumerator LatePlay()
    {            
             
        yield return new WaitForSeconds(2.5f);
        StartPlay.SetActive(true);
        number.text = "1";
        yield return new WaitForSeconds(1f);
        number.text = "2";
        yield return new WaitForSeconds(1f);
        number.text = "3";
        yield return new WaitForSeconds(3f);
        StartPlay.SetActive(false);
        Game.gameStatus = Game.GameStatus.isPlaying;
        MusicManager.PlayMusic("music");
        Game.gameStatus = Game.GameStatus.isPlaying;
        StartCoroutine(CheckForItems());
    }
    // Start is called before the first frame update
  
    void CreateNewItem()
    {
        itemIndex = Random.Range(0, items.items.Length);
        zPose = Random.Range(-8, 13);
        xPose = Random.Range(-6, 6);
        Vector3 newPos= new Vector3(xPose, 0.65f, zPose);
        GameObject item_ = Instantiate(items.items[itemIndex]);
        item_.transform.position = newPos;

    }
    // Update is called once per frame
    void Update()        
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CreateNewItem();
        }
        
    }
   
}
