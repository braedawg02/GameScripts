using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEditor.SearchService;
using System.Runtime.InteropServices;
using UnityEngine.UIElements;

public class GameConsole : MonoBehaviour
{
    public bool consoleActive = false;
    string input;

    public static DebugCommand Toggle_View;
    public Camera playerCamera;
    public PlayerController playerController;
        public static List<object> CommandList;

    private void Awake()
    {

        Toggle_View = new DebugCommand("Toggle_View", "Toggle between first person and free camera", "Toggle_View", () =>
        {
        PlayerController.instance.Toggle_View();
        });

        CommandList = new List<object>(){
            Toggle_View
        };
    }

    public void OnReturn(InputValue Value)
    {
        {
            if (!consoleActive)
            {
                return;
            }
            if (Value.isPressed)
            {
                HandleInput();
                input = "";
            }
        }
    }

    public void OnToggleDebug(InputValue value)
    {
        consoleActive = !consoleActive;

    }
    private void OnGUI()
    {
        if (!consoleActive)
        {
            return;
        }
        float y = 0f;
        GUI.Box(new Rect(0, y, Screen.width, Screen.height), "Console");
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }
    private void HandleInput()
    {
        for (int i = 0; i < CommandList.Count; i++)
        {
            DebugCommandBase commandBase = CommandList[i] as DebugCommandBase;
            if (input.Contains(commandBase.commandID))
            {
                if (CommandList[i] as DebugCommand != null)
                {
                    (CommandList[i] as DebugCommand).Invoke();


                }
            }

        }
    }
}

