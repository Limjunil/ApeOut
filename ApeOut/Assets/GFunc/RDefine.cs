using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RDefine
{
    
    public const string ENEMY_TAG = "Enemy";
    public const string PLAYER_TAG = "Player";

    public const string INIT_SCENE = "00.InitScene";
    public const string TITLE_SCENE = "01.TitleScene";
    public const string PLAY_SCENE = "02.PlayScene";



    public enum TileStatusColor
    {
        DEFAULT, SELECTED, SEARCH, INACTIVE
    }
}
