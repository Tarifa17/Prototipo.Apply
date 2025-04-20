using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Player_.Models
{
    public class Jugador
    {
        private Vector2 position;
        private Vector2 velocity;

        public Jugador(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        public Vector2 Velocity { get => velocity; set => velocity = value; }
    }
}

