using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> BodyParts = new List<Transform>();
    [SerializeField] float minDistance = 0.25f;
    [SerializeField] float speed = 1;
    [SerializeField] float rotationSpeed = 50;
    [SerializeField] GameObject bodyPrefab;

    [SerializeField] float dis;
     Transform curBodyPart;
    private Transform prevBodyPart;

    int beginSize = 3;
     string hor = "Horizontal";
    float rotDirection;
    float time;
    float randomCall;
    float maxSpeed = 5;
    public enum MobileInputMode
    {
        Touch,
        Accelerometer
    }
    [Header("Mobile Options")]
    [Tooltip("What input method to use on mobile devices")]
    public MobileInputMode inputMode = MobileInputMode.Accelerometer;
    [Range(1.0f, 10.0f)]
    public float accelerometerSensitivity = 4.0f;
    public static int InputSetup
    {
        get { return PlayerPrefs.GetInt("InputSetup", 0); }
        set { PlayerPrefs.SetInt("InputSetup", value); }
    }
    private float GetSteerInput()
    {
        if (!Application.isMobilePlatform)
        {
            // Get GamePad/Keyboard input.
            return Input.GetAxis("Horizontal");
        }
        else if (inputMode == MobileInputMode.Touch)
        {
            // Touch the left side of the screen to turn left, right side to turn right
            if (Input.touchCount > 0)
                return Input.GetTouch(0).position.x < (0.5f * Screen.width) ? -1.0f : 1.0f;
            else
                return 0.0f;
        }
        else
        {
            // Use the orientation of the device as the steering value
            return accelerometerSensitivity * Input.acceleration.x;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < beginSize-1; i++)
        {
            AddBodyPart();
        }

        StartCoroutine(RandomTime());
    }
    IEnumerator RandomTime()
    {
        randomCall = Random.Range(1f,5f);
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
    // Update is called once per frame
    void Update()
    {
        //UpdateRotDirection();
        if (Game.gameStatus == Game.GameStatus.isPlaying && Game.isMoving)
        {
            Move();
        }       
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddBodyPart();
        }
        if (Game.gameStatus == Game.GameStatus.isPlaying && speed < maxSpeed)
        {
            speed += 0.02f * Time.smoothDeltaTime;
        }
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
     void Move()
    {
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed *= speed*2;
        }
        BodyParts[0].Translate(BodyParts[0].forward * currentSpeed * Time.smoothDeltaTime,Space.World);

        if (GetSteerInput() != 0)
        {
            BodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * GetSteerInput());
        }

        //BodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * rotDirection);  

        for (int i = 1; i < BodyParts.Count; i++)
        {
            curBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i - 1];
            dis = Vector3.Distance(prevBodyPart.position, curBodyPart.position);

            Vector3 newPos = prevBodyPart.position;
            newPos.y = BodyParts[0].position.y;

            float T = Time.deltaTime * dis / minDistance * currentSpeed;

            if (T > 0.5F)
            {
                T = 0.5F;
            }
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newPos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, prevBodyPart.rotation, T);
        }
    }
    float size = 1;
    public void AddBodyPart()
    {
        size -= 0.03f;
        Transform newPart=(Instantiate(bodyPrefab,BodyParts[BodyParts.Count-1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;
        newPart.SetParent(transform);
        newPart.localScale = new Vector3(size, size, size);
        BodyParts.Add(newPart);
    }
  
}
