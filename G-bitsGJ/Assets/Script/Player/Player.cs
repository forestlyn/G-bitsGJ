using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour , IPlayer
{
    private BaseState currentState;
    private Dictionary<PlayerState, BaseState> stateDic = new Dictionary<PlayerState, BaseState>();
    private Animator animator;
    private Rigidbody2D rigidbody2D;
    public void ChangeState(PlayerState stateName)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = stateDic[stateName];
        currentState.Enter();
    }

    public void MyUpdate()
    {
        currentState.Update();
    }

    public void Init(Vector2 beginPosition)
    {
        transform.position = beginPosition;
        Direction = PlayerDirection.Right;
        LastAttackedTime = 0;
        HP = 20;
        InitComponent();
        InitState();
    }

    private void InitState()
    {
        stateDic.Add(PlayerState.Drop, new DropState(this));
        stateDic.Add(PlayerState.Walk, new WalkState(this));

        ChangeState(PlayerState.Drop);
    }
    private void InitComponent()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private PlayerDirection direction;
    public PlayerDirection Direction
    {
        get => direction;
        set
        {
            if(direction == value)
            {
                return;
            }
            direction = value;
            Vector3 midScale = transform.localScale;
            if(direction == PlayerDirection.Left)
            {
                midScale.x = -Mathf.Abs(midScale.x);
            }
            else
            {
                midScale.x = Mathf.Abs(midScale.x);
            }
            transform.localScale = midScale;            
        }
    }

    public Vector2 Direction_vector2
    {
        get
        {
            if(Direction == PlayerDirection.Left)
            {
                return Vector2.left;
            }
            else
            {
                return Vector2.right;
            }
        }
    }

    private float speed;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    float LastAttackedTime;
    private int hp;
    public int HP
    {
        get => hp;
        set 
        {
            if(value != hp && Time.time - LastAttackedTime > 0.5f)
            {
                UIManager.Instance.SetHP(value);
                LastAttackedTime = Time.time;
            }
            if(value <= 0)
            {
                // Game Over
                GameManager.Instance.ChangeGameState(GameStateType.GameOver);
            }
            
        }
    }


    private class BaseState
    {
        protected Player player;
        public BaseState(Player player)
        {
            this.player = player;
        }
        public virtual void Enter() { }
        public virtual void Update() 
        {
            Vector2 currentVelocity = player.Direction_vector2 * player.Speed;
            currentVelocity.y = player.rigidbody2D.velocity.y;
            player.rigidbody2D.velocity = currentVelocity;
        }
        public virtual void Exit() { }
    }

    private class DropState : BaseState
    {
        public DropState(Player player) : base(player)
        {
        }
        public override void Enter()
        {
            base.Enter();
            player.animator.SetBool("Drop", true);
        }

        public override void Exit()
        {
            base.Exit();
            player.animator.SetBool("Drop", false);
        }
    }

    private class WalkState : BaseState
    {
        public WalkState(Player player) : base(player)
        {
        }
        public override void Enter()
        {
            base.Enter();
            player.animator.SetBool("Walk", true);
        }
        public override void Update()
        {
            base.Update();
            // Physics2D.Raycast(player.transform.position , )
            Vector2 center = (Vector2)player.transform.position + player.Direction_vector2 * 0.3f;
            Vector2 size = new Vector2(0.2f, 0.1f);
            var collider2D = Physics2D.OverlapBox(center, size , 0 , LayerMask.GetMask("Platform"));
            //Gizmos.DrawWireCube(center, size);
            if (collider2D != null)
            {
                if(player.Direction == PlayerDirection.Left)
                {
                    player.Direction = PlayerDirection.Right;
                }
                else
                {
                    player.Direction = PlayerDirection.Left;
                }
            }
        }
        public override void Exit()
        {
            base.Exit();
            player.animator.SetBool("Walk", false);
        }

    }

    private void OnDrawGizmos()
    {
        if (currentState is WalkState)
        {
            Gizmos.color = Color.red;
            Vector2 center = (Vector2)transform.position + Direction_vector2 * 0.3f;
            Vector2 size = new Vector2(0.2f, 0.1f);
            Gizmos.DrawWireCube(center, size);
        }
    }
}

public interface IPlayer 
{
    PlayerDirection Direction{ get; set; }
    float Speed { get; set; }
    int HP { get; set; }
    void ChangeState(PlayerState stateName);
}

public enum PlayerState
{
    Drop,
    Walk
}

public enum PlayerDirection
{
    Left,
    Right
}