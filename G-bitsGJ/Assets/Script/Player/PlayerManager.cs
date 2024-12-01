using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerManager
{
    void CreatePlayer(Vector2 position);
    void MyUpdate();
}

public class PlayerManager : IPlayerManager
{
    private static PlayerManager instance;
    public static IPlayerManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PlayerManager();
                instance.Init();
            }
            return instance;
        }
    }
    GameObject prefab_player;
    Player player;
    private void Init()
    {
        prefab_player = Resources.Load<GameObject>("Prefabs/Player");
        player = null;
    }

    void IPlayerManager.CreatePlayer(Vector2 position)
    {
        GameObject playerGO = GameObject.Instantiate(prefab_player);
        player = playerGO.GetComponent<Player>();
        player.Init(position);
    }

    public void MyUpdate()
    {
        player.MyUpdate();
    }

}
