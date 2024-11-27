using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerManager
{
    void CreatePlayer(Vector2 position);
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
    private void Init()
    {
        prefab_player = Resources.Load<GameObject>("Prefabs/Player");
    }

    void IPlayerManager.CreatePlayer(Vector2 position)
    {
        GameObject playerGO = GameObject.Instantiate(prefab_player);
        playerGO.GetComponent<Player>().Init(position);
    }

}
