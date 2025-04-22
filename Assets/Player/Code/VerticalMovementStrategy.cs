using Assets.Player.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Player.Code
{
    internal class VerticalMovementStrategy : IMovementStrategy
    {
        public void Move(Rigidbody2D rb, Jugador jugador, float direccion)
        {
            Vector2 velocidadFisica = rb.velocity;
            velocidadFisica.y = direccion * jugador.Velocity.y;
            rb.velocity = velocidadFisica;
        }
    }
}
