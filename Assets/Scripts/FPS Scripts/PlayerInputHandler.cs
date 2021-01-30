using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Tooltip("Sensitivity multiplier for moving the camera around")]
    public float lookSensitivity = 1f;
    [Tooltip("Additional sensitivity multiplier for WebGL")]
    public float webglLookSensitivityMultiplier = 0.25f;
    [Tooltip("Limit to consider an input when using a trigger on a controller")]
    public float triggerAxisThreshold = 0.4f;
    [Tooltip("Used to flip the vertical input axis")]
    public bool invertYAxis = false;
    [Tooltip("Used to flip the horizontal input axis")]
    public bool invertXAxis = false;

    GameFlowManager m_GameFlowManager;
    // PlayerCharacterController m_PlayerCharacterController;
    bool m_FireInputWasHeld;

    PlayerInput input;

    Vector2 moveInput = new Vector2();
    Vector2 lookInput;
    private void Start()
    {
        // m_PlayerCharacterController = GetComponent<PlayerCharacterController>();
        // DebugUtility.HandleErrorIfNullGetComponent<PlayerCharacterController, PlayerInputHandler>(m_PlayerCharacterController, this, gameObject);
        // m_GameFlowManager = FindObjectOfType<GameFlowManager>();
        // DebugUtility.HandleErrorIfNullFindObject<GameFlowManager, PlayerInputHandler>(m_GameFlowManager, this);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        m_FireInputWasHeld = GetFireInputHeld();
    }

    public bool CanProcessInput()
    {
        return Cursor.lockState == CursorLockMode.Locked /* && !m_GameFlowManager.gameIsEnding */ ;
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector2>();
    }
    public Vector3 GetMoveInput()
    {
        if (CanProcessInput())
        {
            Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

            // constrain move input to a maximum magnitude of 1, otherwise diagonal movement might exceed the max move speed defined
            move = Vector3.ClampMagnitude(move, 1);

            return move;
        }
        return Vector3.zero;
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        lookInput = value.ReadValue<Vector2>();
    }
    public float GetLookInputsHorizontal()
    {
        return GetMouseOrStickLookAxis(Mouse.current.delta.ReadValue().x, -lookInput.x);
    }

    public float GetLookInputsVertical()
    {
        return GetMouseOrStickLookAxis(Mouse.current.delta.ReadValue().y, lookInput.y);
    }

    public bool GetJumpInputDown()
    {
        if (CanProcessInput())
        {
            return Gamepad.current.bButton.wasPressedThisFrame;
        }

        return false;
    }

    public bool GetJumpInputHeld()
    {
        if (CanProcessInput())
        {
            return Gamepad.current.bButton.isPressed;
        }

        return false;
    }

    public bool GetFireInputDown()
    {
        return GetFireInputHeld() && !m_FireInputWasHeld;
    }

    public bool GetFireInputReleased()
    {
        return !GetFireInputHeld() && m_FireInputWasHeld;
    }

    public bool GetFireInputHeld()
    {
        if (CanProcessInput())
        {
            float value = Gamepad.current.rightTrigger.ReadValue();
            bool isGamepad = value != 0f;
            if (isGamepad)
            {
                return value >= triggerAxisThreshold;
            }
            else
            {
                return Mouse.current.leftButton.isPressed;
            }
        }

        return false;
    }

    public bool GetAimInputHeld()
    {
        if (CanProcessInput())
        {
            // bool isGamepad = Input.GetAxis(GameConstants.k_ButtonNameGamepadAim) != 0f;
            bool i = false; // isGamepad ? (Input.GetAxis(GameConstants.k_ButtonNameGamepadAim) > 0f) : Input.GetButton(GameConstants.k_ButtonNameAim);
            return i;
        }

        return false;
    }

    public bool GetSprintInputHeld()
    {
        if (CanProcessInput())
        {
            return Keyboard.current.shiftKey.isPressed;
        }

        return false;
    }

    public bool GetCrouchInputDown()
    {
        if (CanProcessInput())
        {
            return Keyboard.current.ctrlKey.wasPressedThisFrame;
        }

        return false;
    }

    public bool GetCrouchInputReleased()
    {
        if (CanProcessInput())
        {
            return Keyboard.current.ctrlKey.wasReleasedThisFrame;
        }

        return false;
    }

    public int GetSwitchWeaponInput()
    {
        if (CanProcessInput())
        {

            bool isGamepad = false; //Input.GetAxis(GameConstants.k_ButtonNameGamepadSwitchWeapon) != 0f;
            string axisName = isGamepad ? GameConstants.k_ButtonNameGamepadSwitchWeapon : GameConstants.k_ButtonNameSwitchWeapon;

            if (false)
                return -1;
            else if (false)
                return 1;
            /* else if (Input.GetAxis(GameConstants.k_ButtonNameNextWeapon) > 0f)
                return -1;
            else if (Input.GetAxis(GameConstants.k_ButtonNameNextWeapon) < 0f)
                return 1;
            */
        }

        return 0;
    }

    public int GetSelectWeaponInput()
    {
        if (CanProcessInput())
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
                return 1;
            else if (Keyboard.current.digit2Key.wasPressedThisFrame)
                return 2;
            else if (Keyboard.current.digit3Key.wasPressedThisFrame)
                return 3;
            else if (Keyboard.current.digit4Key.wasPressedThisFrame)
                return 4;
            else if (Keyboard.current.digit5Key.wasPressedThisFrame)
                return 5;
            else if (Keyboard.current.digit6Key.wasPressedThisFrame)
                return 6;
            else
                return 0;
        }

        return 0;
    }

    float GetMouseOrStickLookAxis(float mouseInput, float stickInput)
    {
        if (CanProcessInput())
        {
            // Check if this look input is coming from the mouse
            bool isGamepad = stickInput != 0f;
            float i = isGamepad ? stickInput : mouseInput;

            // handle inverting vertical input
            if (invertYAxis)
                i *= -1f;

            // apply sensitivity multiplier
            i *= lookSensitivity;

            if (isGamepad)
            {
                // since mouse input is already deltaTime-dependant, only scale input with frame time if it's coming from sticks
                i *= Time.deltaTime;
            }
            else
            {
                // reduce mouse input amount to be equivalent to stick movement
                i *= (0.75f * Time.deltaTime);
#if UNITY_WEBGL
                // Mouse tends to be even more sensitive in WebGL due to mouse acceleration, so reduce it even more
                i *= webglLookSensitivityMultiplier;
#endif
            }

            return i;
        }

        return 0f;
    }
}
