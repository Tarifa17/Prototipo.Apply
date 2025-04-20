using Assets.Player_.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private List<ICommand> commands;
    [SerializeField] private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;

    private void Start()
    {
        commands = new List<ICommand>();
    }

    private void Update()
    {
        commands.Clear();
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        commands.Add(new HorizontalCommand(horizontalInput, rb));
        commands.Add(new VerticalCommand(verticalInput, rb));

        foreach (var command in commands)
        {
            command.execute();
        }
    }
}
