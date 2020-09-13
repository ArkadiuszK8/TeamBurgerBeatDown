using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewButton : Button
{
    public PlayerController playerController;

    public ButtonType type;

    private void Update()
    {
        if (IsPressed() == true)
        {
            switch (type)
            {
                case ButtonType.left:
                    playerController.MoveLeft();
                    break;
                case ButtonType.right:
                    playerController.MoveRight();
                    break;
                case ButtonType.fire:
                    playerController.Fire();
                    break;
            }
        }
    }

    public enum ButtonType
    {
        left,
        right,
        fire
    }
}
