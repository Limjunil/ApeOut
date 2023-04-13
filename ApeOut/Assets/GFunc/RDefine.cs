using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RDefine
{
    public const string TERRAIN_PREF_OCEAN = "Terrain_Ocean";
    public const string TERRAIN_PREF_PLAIN = "Terrain_Plain";

    public const string OBSTACLE_PREF_PLAIN_CASTLE = "Obstacle_PlainCastle";

    public const string ENEMY_TAG = "Enemy";
    public const string PLAYER_TAG = "Player";



    public enum TileStatusColor
    {
        DEFAULT, SELECTED, SEARCH, INACTIVE
    }
}
