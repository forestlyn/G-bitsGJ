
using UnityEngine;

public class GameManager:MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameObject>("GameManager").GetComponent<GameManager>();
                instance.Init();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            Init();
        }
    }

    private PlatformManager platformManager;

    [SerializeField]
    private float runSpeed = 1.0f;
    public float RunSpeed
    {
        get { return runSpeed; } 
        set { runSpeed = value; }
    }

    private void Init()
    {
        platformManager = new PlatformManager();
        PlayerManager.Instance.CreatePlayer(new Vector2(0, 5));
    }

    private void FixedUpdate()
    {
    }

    public void Update()
    {
        platformManager.MyUpdate(Time.deltaTime * runSpeed);
        BGManager.Instance.MyUpdate(Time.deltaTime * runSpeed);
    }
}