using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaveKoala.Characters
{
    public static class Tags
    {
        public const string Ground = @"Ground";
        public const string Edge = @"Edge";
        public const string Enemy = @"Enemy";
        public const string Player = @"Player";
        public const string Projectile = @"Projectile";
    }

    public enum CharacterType
    {
        Enemy = 0,
        Player = 1
    }
}