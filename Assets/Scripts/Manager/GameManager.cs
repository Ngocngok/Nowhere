using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private CharacterBehavior _player;

    public CharacterBehavior Player
    {
        get
        {
            if (_player == null)
            {
                _player = FindObjectOfType<PlayerBehavior>();
            }
            return _player;
        }
    }
}
