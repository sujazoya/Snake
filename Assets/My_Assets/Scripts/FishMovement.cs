using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public enum FrontSide
    {
        None,LeftHanded, RightHanded,Back,Front
    }
  
    [SerializeField] float minDistance = 0.25f;
    [SerializeField] float speed = 1;
    [SerializeField] float rotationSpeed = 50;   
    float rotDirection;
    float time;
    float randomCall;
    float baseSpeed;   
    public FrontSide frontSide;
    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = speed;
        //for (int i = 0; i < beginSize-1; i++)
        //{
        //    AddBodyPart();
        //}

        StartCoroutine(RandomTime());
    }
    IEnumerator RandomTime()
    {
        randomCall = Random.Range(3f,9f);
        time = Random.Range(1,8);
        yield return new WaitForSeconds(randomCall);
        if (time <4)
        {
            max = true;
        }
        else
        {
            max = false;
        }
        StartCoroutine(RandomTime());
    }
    private float GetSteerInput()
    {       
       return Input.GetAxis("Horizontal");       
        
    }
    // Update is called once per frame
    void Update()
    {
        UpdateRotDirection();
         Move();              
       
    }
    bool max;
    void UpdateRotDirection()
    {
        if (max && rotDirection<1f)
        {
            rotDirection += 0.3f * Time.deltaTime;
        }
        else if (!max && rotDirection > -1f)
        {
            rotDirection -= 0.3f * Time.deltaTime;
        }
    }
    float currentSpeed;
     void Move()
    {
         currentSpeed = speed;
        //if (Input.GetKey(KeyCode.W))
        //{
        //    currentSpeed *= speed*2;
        //}
        if (frontSide==FrontSide.LeftHanded)
        {
            transform.Translate(-transform.right * currentSpeed * Time.smoothDeltaTime, Space.World);
        }
        else if (frontSide == FrontSide.RightHanded)
        {
            transform.Translate(transform.right * currentSpeed * Time.smoothDeltaTime, Space.World);
        }
        else if (frontSide == FrontSide.Back)
        {
            transform.Translate(transform.forward * currentSpeed * Time.smoothDeltaTime, Space.World);
        }
        else if (frontSide == FrontSide.Front)
        {
            transform.Translate(-transform.forward * currentSpeed * Time.smoothDeltaTime, Space.World);
        }
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * rotDirection);  

        //for (int i = 1; i < BodyParts.Count; i++)
        //{
        //    curBodyPart = BodyParts[i];
        //    prevBodyPart = BodyParts[i - 1];
        //    dis = Vector3.Distance(prevBodyPart.position, curBodyPart.position);

        //    Vector3 newPos = prevBodyPart.position;
        //    newPos.y = BodyParts[0].position.y;

        //    float T = Time.deltaTime * dis / minDistance * currentSpeed;

        //    if (T > 0.5F)
        //    {
        //        T = 0.5F;
        //    }
        //    curBodyPart.position = Vector3.Slerp(curBodyPart.position, newPos, T);
        //    curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, prevBodyPart.rotation, T);
        //}
    }
    void SpeedNormal()
    {
        currentSpeed = baseSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Game.SnakeBody)
        {
            currentSpeed *= speed * 2;
            Invoke("SpeedNormal", 1);
        }
    }


}
