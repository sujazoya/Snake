using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using suja;

public class SnakeHealth : MonoBehaviour
{
    [SerializeField] List< MeshRenderer> bodyRenderers;
     float lerpTime=2;
    Color feedColor = Color.red;
   [SerializeField] SnakeMovement snakeMovement;
    //public LayerMask foodMask;
    // Start is called before the first frame update
    
    [SerializeField] GameObject body_blast;
    GameMaster gameMaster;
    void Start()
    {
        //snakeMovement = GetComponent<SnakeMovement>();
        Invoke("GetbodyRenderers", 2);
        gameMaster = FindObjectOfType<GameMaster>();       
        MusicManager.PlayMusic("music");
    }
   void GetbodyRenderers()
    {
        for (int i = 1; i < snakeMovement.BodyParts.Count; i++)
        {
            MeshRenderer mr = snakeMovement.BodyParts[i].GetComponent<MeshRenderer>();
            bodyRenderers.Add(mr);
        }
       
    }
    void ChangeColor()
    {
        changeColor = true;
    }
    [HideInInspector] public bool changeColor = false;
    [SerializeField] float time;
    // Update is called once per frame
    void Update()
    {
        if (bodyRenderers.Count > 0)
        {
            if (changeColor)
            {
                for (int i = 0; i < bodyRenderers.Count; i++)
                {
                    bodyRenderers[i].material.color = Color.Lerp(bodyRenderers[i].material.color, feedColor, lerpTime*Time.deltaTime);
                    //bodyRenderers[i].transform.localScale = Vector3.Lerp(bodyRenderers[i].transform.localScale, bodyRenderers[i].transform.localScale * 1.7f, 2f);
                }
                
            }            
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeColor();
        }
    }
    void StarMovingSnake()
    {
        Game.isMoving = true;
    }
    IEnumerator Die(GameObject other)
    {
        yield return new WaitForSeconds(2);
        //Instantiate(body_blast, transform.position, transform.rotation, transform);
        if (other.GetComponent<Animator>())
        {
            other.GetComponent<Animator>().SetTrigger("end");
        }
        for (int i = 1; i < snakeMovement.BodyParts.Count; i++)
        {
            snakeMovement.BodyParts[i].GetComponent<BodyPart>().ShowBlast();
            Game.gameStatus = Game.GameStatus.isGameover;
            Destroy(snakeMovement.gameObject, 2.2f);
        }
        if (Game.achivedLevelTarget >= Game.currentLevelTarget)
        {
            gameMaster.OnGameWon();
        }
        else
        {
            gameMaster.OnGameover();
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Game.foodTag)
        {
            MusicManager.PlaySfx("eating");
            Game.isMoving = false;
            Invoke("StarMovingSnake", 1);
            if (other.GetComponent<Animator>())
            {
                other.GetComponent<Animator>().SetTrigger("end");
            }
            other.transform.position = transform.position;
            snakeMovement.AddBodyPart();
            Game.achivedLevelTarget++;
            gameMaster.UpdateLevelStatus();
            if (Game.achivedLevelTarget >= Game.currentLevelTarget)
            {
                gameMaster.OnGameWon();
            }
            //Debug.Log("hit" + transform.name);
            //animator.SetTrigger("end");
            Destroy(other.gameObject, 0.5f);
        }
        else if (other.tag == Game.poisonTag)
        {
            Game.isMoving = false;
            MusicManager.Vibrate();
            MusicManager.PlaySfx("die");
            StartCoroutine(Die(other.gameObject));          
            GetbodyRenderers();
            changeColor = true;
            other.transform.position = transform.position;  
        }
        
    }
}
