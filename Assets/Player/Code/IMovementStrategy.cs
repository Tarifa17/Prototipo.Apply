using Assets.Player.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Player.Code
{
    internal interface IMovementStrategy
    {
        public void Move(Rigidbody2D rb, Jugador player, float direction);
    }
}
