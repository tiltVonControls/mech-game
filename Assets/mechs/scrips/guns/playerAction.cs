using UnityEngine;
using UnityEngine.InputSystem;

public class playerAction : MonoBehaviour
{
    [SerializeField]
    private gunSelect gunSelect;

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed && gunSelect.gunActiveL != null)

        {
            gunSelect.gunActiveL.DoShoot();
            Debug.Log("shoot left");
        }
        if (Mouse.current.rightButton.isPressed && gunSelect.gunActiveR != null)

        {
            gunSelect.gunActiveR.DoShoot();
            Debug.Log("shoot right");
        }
    }
}

