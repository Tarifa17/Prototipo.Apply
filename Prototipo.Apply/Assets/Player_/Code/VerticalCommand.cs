using Assets.Player_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Player_.Code
{
    public class VerticalCommand : ICommand
    {
        private readonly float _input;
        private Rigidbody2D _rb;
        private Jugador _jugador;
        private IMovementStrategy _movementStrategy;

        public VerticalCommand(float input, Rigidbody2D rb)
        {
            _input = input;
            _rb = rb;
            _jugador = new Jugador(new Vector2(0, 3));
            _movementStrategy = new VerticalMovementStrategy();
        }
        public void execute()
        {
            _movementStrategy.Move(_rb, _jugador, _input);
        }
    }
}
