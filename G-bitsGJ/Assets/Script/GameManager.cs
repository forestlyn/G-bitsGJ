
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
    }

    private void FixedUpdate()
    {
        platformManager.FixedUpdate(Time.fixedDeltaTime * runSpeed);
    }

    public void Update()
    {
        platformManager.Update(Time.deltaTime * runSpeed);
    }
}