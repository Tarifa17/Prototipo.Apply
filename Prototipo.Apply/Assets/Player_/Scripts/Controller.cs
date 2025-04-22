using Assets.Player_.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private List<ICommand> commands;
    private Animator _animator;
    [SerializeField] private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    void Start()
    {
        commands = new List<ICommand>();
        _animator = GetComponent<Animator>();

    }

    void Update()
    {
        commands.Clear();
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Crear y añadir comandos
        commands.Add(new HorizontalCommand(horizontalInput, rb));
        commands.Add(new VerticalCommand(verticalInput, rb));

        foreach (var command in commands)
        {
            command.execute();
        }

        _animator.SetFloat("Horizontal", horizontalInput);
        _animator.SetFloat("Vertical", verticalInput);
        if (horizontalInput != 0 || verticalInput != 0) // Si hay movimiento
        {
            _animator.SetBool("isRunning", true); // Activa la animación de correr
        }
        else
        {
            _animator.SetBool("isRunning", false); // Desactiva la animación de correr
        }
    }
}
