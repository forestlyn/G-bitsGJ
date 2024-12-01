
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private InputManager inputManager;

    [SerializeField]
    private float runSpeed = 1.0f;
    public float RunSpeed
    {
        get { return runSpeed; } 
        set { runSpeed = value; }
    }

    [Header("PlatformManager")]
    [SerializeField]
    public float createLeftInterval = 1.0f;
    [SerializeField]
    public float createRightInterval = 1.5f;
    private void Init()
    {
        platformManager = new PlatformManager(createLeftInterval, createRightInterval);
        platformManager.Init();
        inputManager = new InputManager();
        PlayerManager.Instance.CreatePlayer(new Vector2(0, 2));
        UIManager.Instance.Init();
        InitGameState();
    }

    private void FixedUpdate()
    {
    }

    public void Update()
    {
        currentState.Update();

    }

    public void HandleInput(InputType inputType, Vector2 position, Collider2D collider2D)
    {
        //Debug.Log($"{inputType} {position}");
        switch (inputType)
        {
            case InputType.SwipeLeft:
                platformManager.CreatePlatform(position, PlatformType.ChangeDirectionLeft);
                break;
            case InputType.SwipeRight:
                platformManager.CreatePlatform(position, PlatformType.ChangeDirectionRight);
                break;
            case InputType.SwipeDown:
                platformManager.CreatePlatform(position, PlatformType.Breakable);
                break;
            case InputType.Delete:
                var platform = collider2D.GetComponent<BasePlatform>();
                Debug.Log("platform: " + collider2D.name);
                platform?.Reduce();
                break;
        }
    }

    private BaseGameState currentState;
    private Dictionary<GameStateType, BaseGameState> stateList ;
    private void InitGameState()
    {
        stateList = new Dictionary<GameStateType, BaseGameState>();
        stateList.Add(GameStateType.Start, new StartState());
        stateList.Add(GameStateType.Playing, new PlayingState());
        stateList.Add(GameStateType.Pause, new PauseState());
        stateList.Add(GameStateType.GameOver, new GameOverState());
        stateList.Add(GameStateType.Win, new WinState());
        ChangeGameState(GameStateType.Start);
    }
    public void ChangeGameState(GameStateType gameStateType)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }
        currentState = stateList[gameStateType];
        currentState.Enter();
    }

    private float score ;


    public class BaseGameState
    {
        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }

    public class StartState : BaseGameState
    {
        public override void Enter()
        {
            base.Enter();
            UIManager.Instance.ShowStartMenu(true);
            GameManager.Instance.score = 0;
        }

        public override void Exit()
        {
            base.Exit();
            UIManager.Instance.ShowStartMenu(false);
        }
    }

    public class PlayingState : BaseGameState
    {
        public override void Enter()
        {
            base.Enter();
            UIManager.Instance.ShowGameUI(true);
        }

        public override void Update()
        {
            base.Update();
            GameManager.Instance.score += Time.deltaTime; ;
            UIManager.Instance.SetScore((int)GameManager.Instance.score);

            
            GameManager.Instance.platformManager.MyUpdate(Time.deltaTime * GameManager.Instance.runSpeed);
            BGManager.Instance.MyUpdate(Time.deltaTime * GameManager.Instance.runSpeed);
            GameManager.Instance.inputManager.MyUpdate(Time.deltaTime * GameManager.Instance.runSpeed);
            PlayerManager.Instance.MyUpdate();
        }

        public override void Exit()
        {
            base.Exit();
            UIManager.Instance.ShowGameUI(false);
        }
    }

    public class PauseState : BaseGameState
    {
        public override void Enter()
        {
            base.Enter();
            UIManager.Instance.ShowPause(true);
        }

        public override void Exit()
        {
            base.Exit();
            UIManager.Instance.ShowPause(false);
        }
    }

    public class GameOverState : BaseGameState
    {
        public override void Enter()
        {
            base.Enter();
            UIManager.Instance.ShowGameOver(true);
            GameManager.Instance.score = 0;
        }

        public override void Exit()
        {
            base.Exit();
            UIManager.Instance.ShowGameOver(false);
        }
    }

    public class WinState : BaseGameState
    {
        public override void Enter()
        {
            base.Enter();
            UIManager.Instance.ShowWin(true);
            GameManager.Instance.score = 0;
        }

        public override void Exit()
        {
            base.Exit();
            UIManager.Instance.ShowWin(false);
        }
    }


}


public enum GameStateType
{
    Start,
    Playing,
    Pause,
    GameOver,
    Win
}