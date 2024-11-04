// GENERATED AUTOMATICALLY FROM 'Assets/Assets/PlayerController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerController"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""c5c2d747-86e5-436e-beda-87de5475b966"",
            ""actions"": [
                {
                    ""name"": ""Thrust"",
                    ""type"": ""PassThrough"",
                    ""id"": ""288b4cfb-fe3c-4d7e-9807-d97ddecd251a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Strafe"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bb9cf557-ba1b-42af-b5e9-68e04f2d77d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up-Down"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e284bdb1-2ff8-4f74-80f3-6a88bcbcd4d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""be519aae-7c29-4c10-b651-ab7be039d030"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5a878f59-b60d-4bd5-988a-a9212430df71"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""2f5c2291-1bf2-4ff8-b01f-ee34141f6667"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""0d051d0c-c9b7-4f11-b18e-0c874d16e3be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""ThrustKeyboard"",
                    ""id"": ""3dd2daa7-7cdc-443a-87bd-3ce73c4605f3"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e579e3e3-da28-4f67-99cf-a6bfc0fa4e38"",
                    ""path"": ""<Keyboard>/#(S)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""75bd855b-3cac-421a-b2a1-9d71b6bdb523"",
                    ""path"": ""<Keyboard>/#(W)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ThrustController"",
                    ""id"": ""d68996e6-a252-4f35-aee0-3e0f14dd2d8a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a644c869-81bf-478a-a695-b3fe61bb3eaa"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0c951fa9-2729-4960-a18f-4a5a2809605e"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""StrafeKeyboard"",
                    ""id"": ""51a6a490-bf26-4ae6-9cc1-08418b52a0f1"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2eee53c0-46e8-4721-b648-da92bf846dab"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""13c3177e-1b62-45fc-9ff1-33be72bc0d9a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""StrafeController"",
                    ""id"": ""d6cdab2a-df17-4996-a1f2-8ece2f66799f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1df33851-7c00-4bf8-aa52-2a6f83262373"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ad965728-7671-4728-8253-a12a8c5fc7eb"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Up-DownKeyBoard"",
                    ""id"": ""9bd2d980-4005-445e-a4c3-4ce2ee411da8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up-Down"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""aadaede2-d849-43e6-a887-e398adba782e"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up-Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""24cdde2a-5376-442c-a1f8-528f5710b384"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up-Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Up-DownController"",
                    ""id"": ""0c63ce83-2a15-483b-acf4-1beae9b95cf0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up-Down"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b48b383e-56c6-4b7d-a7d3-dda15f898a75"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up-Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b4ecdce9-e561-4f1b-8a25-1cf1bc40a8b9"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up-Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""RollKeyboard"",
                    ""id"": ""18106df7-7a0f-49be-9cbe-9eb532357754"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""acd0ff15-da08-4e70-a67e-ad6abb113585"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2b69cfcf-1c26-4741-af40-06a4a482d93a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""RollController"",
                    ""id"": ""1fe74874-7dbc-4453-84bb-896ad5aa0c97"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""89e857db-1e0f-43fd-a974-a6b37f873b3b"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f0942402-c295-41f7-b214-d5dee7592a3c"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cb6a5f09-43b0-4d1d-9166-7cef33e8b455"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eced053d-190e-46d0-b2da-cc2e2fcdf6e9"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e29cae7-0556-4613-846d-0e8949a6ef43"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f214837e-cd75-4b5f-b348-0f5ebacd461b"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Dialogue"",
            ""id"": ""d4b50151-7453-441c-9af5-a52cbafcc96b"",
            ""actions"": [
                {
                    ""name"": ""ContinueDialogue"",
                    ""type"": ""Button"",
                    ""id"": ""3e01d445-5e95-48de-b355-6b2149cba8a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""01a5b945-c1db-4875-81c1-f93be5e82957"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ContinueDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9119be31-6649-4ff7-a8d6-eb728be1ffc5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ContinueDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Tutorial"",
            ""id"": ""fac7e0e6-6b04-405b-ab1d-1aa980992ee1"",
            ""actions"": [
                {
                    ""name"": ""Continue"",
                    ""type"": ""Button"",
                    ""id"": ""b1f93c54-c188-453f-a962-c46a379aae1e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PanCamera"",
                    ""type"": ""Value"",
                    ""id"": ""b8c8588d-2228-4d45-8742-8154b3e8a32b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveForward"",
                    ""type"": ""Button"",
                    ""id"": ""2eba8315-cbca-4c7b-8d5f-2add2398c9bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""a7908b73-e717-45d1-9025-c284d43856e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""06324eb7-6a48-43bd-b369-2e8588649924"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3e8860e-34e5-4c7a-a6a9-8555245822c2"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PanCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6831904-a0c4-4254-b59d-40a6fb5c4390"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c447531-e6b9-4f0a-ada1-5e3333063c30"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_Thrust = m_PlayerControls.FindAction("Thrust", throwIfNotFound: true);
        m_PlayerControls_Strafe = m_PlayerControls.FindAction("Strafe", throwIfNotFound: true);
        m_PlayerControls_UpDown = m_PlayerControls.FindAction("Up-Down", throwIfNotFound: true);
        m_PlayerControls_Roll = m_PlayerControls.FindAction("Roll", throwIfNotFound: true);
        m_PlayerControls_LookMovement = m_PlayerControls.FindAction("LookMovement", throwIfNotFound: true);
        m_PlayerControls_Grab = m_PlayerControls.FindAction("Grab", throwIfNotFound: true);
        m_PlayerControls_Interact = m_PlayerControls.FindAction("Interact", throwIfNotFound: true);
        // Dialogue
        m_Dialogue = asset.FindActionMap("Dialogue", throwIfNotFound: true);
        m_Dialogue_ContinueDialogue = m_Dialogue.FindAction("ContinueDialogue", throwIfNotFound: true);
        // Tutorial
        m_Tutorial = asset.FindActionMap("Tutorial", throwIfNotFound: true);
        m_Tutorial_Continue = m_Tutorial.FindAction("Continue", throwIfNotFound: true);
        m_Tutorial_PanCamera = m_Tutorial.FindAction("PanCamera", throwIfNotFound: true);
        m_Tutorial_MoveForward = m_Tutorial.FindAction("MoveForward", throwIfNotFound: true);
        m_Tutorial_Interact = m_Tutorial.FindAction("Interact", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Thrust;
    private readonly InputAction m_PlayerControls_Strafe;
    private readonly InputAction m_PlayerControls_UpDown;
    private readonly InputAction m_PlayerControls_Roll;
    private readonly InputAction m_PlayerControls_LookMovement;
    private readonly InputAction m_PlayerControls_Grab;
    private readonly InputAction m_PlayerControls_Interact;
    public struct PlayerControlsActions
    {
        private @PlayerController m_Wrapper;
        public PlayerControlsActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Thrust => m_Wrapper.m_PlayerControls_Thrust;
        public InputAction @Strafe => m_Wrapper.m_PlayerControls_Strafe;
        public InputAction @UpDown => m_Wrapper.m_PlayerControls_UpDown;
        public InputAction @Roll => m_Wrapper.m_PlayerControls_Roll;
        public InputAction @LookMovement => m_Wrapper.m_PlayerControls_LookMovement;
        public InputAction @Grab => m_Wrapper.m_PlayerControls_Grab;
        public InputAction @Interact => m_Wrapper.m_PlayerControls_Interact;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Thrust.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnThrust;
                @Thrust.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnThrust;
                @Thrust.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnThrust;
                @Strafe.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnStrafe;
                @Strafe.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnStrafe;
                @Strafe.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnStrafe;
                @UpDown.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnUpDown;
                @UpDown.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnUpDown;
                @UpDown.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnUpDown;
                @Roll.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRoll;
                @LookMovement.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookMovement;
                @LookMovement.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookMovement;
                @LookMovement.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookMovement;
                @Grab.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGrab;
                @Interact.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Thrust.started += instance.OnThrust;
                @Thrust.performed += instance.OnThrust;
                @Thrust.canceled += instance.OnThrust;
                @Strafe.started += instance.OnStrafe;
                @Strafe.performed += instance.OnStrafe;
                @Strafe.canceled += instance.OnStrafe;
                @UpDown.started += instance.OnUpDown;
                @UpDown.performed += instance.OnUpDown;
                @UpDown.canceled += instance.OnUpDown;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @LookMovement.started += instance.OnLookMovement;
                @LookMovement.performed += instance.OnLookMovement;
                @LookMovement.canceled += instance.OnLookMovement;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);

    // Dialogue
    private readonly InputActionMap m_Dialogue;
    private IDialogueActions m_DialogueActionsCallbackInterface;
    private readonly InputAction m_Dialogue_ContinueDialogue;
    public struct DialogueActions
    {
        private @PlayerController m_Wrapper;
        public DialogueActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @ContinueDialogue => m_Wrapper.m_Dialogue_ContinueDialogue;
        public InputActionMap Get() { return m_Wrapper.m_Dialogue; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogueActions set) { return set.Get(); }
        public void SetCallbacks(IDialogueActions instance)
        {
            if (m_Wrapper.m_DialogueActionsCallbackInterface != null)
            {
                @ContinueDialogue.started -= m_Wrapper.m_DialogueActionsCallbackInterface.OnContinueDialogue;
                @ContinueDialogue.performed -= m_Wrapper.m_DialogueActionsCallbackInterface.OnContinueDialogue;
                @ContinueDialogue.canceled -= m_Wrapper.m_DialogueActionsCallbackInterface.OnContinueDialogue;
            }
            m_Wrapper.m_DialogueActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ContinueDialogue.started += instance.OnContinueDialogue;
                @ContinueDialogue.performed += instance.OnContinueDialogue;
                @ContinueDialogue.canceled += instance.OnContinueDialogue;
            }
        }
    }
    public DialogueActions @Dialogue => new DialogueActions(this);

    // Tutorial
    private readonly InputActionMap m_Tutorial;
    private ITutorialActions m_TutorialActionsCallbackInterface;
    private readonly InputAction m_Tutorial_Continue;
    private readonly InputAction m_Tutorial_PanCamera;
    private readonly InputAction m_Tutorial_MoveForward;
    private readonly InputAction m_Tutorial_Interact;
    public struct TutorialActions
    {
        private @PlayerController m_Wrapper;
        public TutorialActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Continue => m_Wrapper.m_Tutorial_Continue;
        public InputAction @PanCamera => m_Wrapper.m_Tutorial_PanCamera;
        public InputAction @MoveForward => m_Wrapper.m_Tutorial_MoveForward;
        public InputAction @Interact => m_Wrapper.m_Tutorial_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Tutorial; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TutorialActions set) { return set.Get(); }
        public void SetCallbacks(ITutorialActions instance)
        {
            if (m_Wrapper.m_TutorialActionsCallbackInterface != null)
            {
                @Continue.started -= m_Wrapper.m_TutorialActionsCallbackInterface.OnContinue;
                @Continue.performed -= m_Wrapper.m_TutorialActionsCallbackInterface.OnContinue;
                @Continue.canceled -= m_Wrapper.m_TutorialActionsCallbackInterface.OnContinue;
                @PanCamera.started -= m_Wrapper.m_TutorialActionsCallbackInterface.OnPanCamera;
                @PanCamera.performed -= m_Wrapper.m_TutorialActionsCallbackInterface.OnPanCamera;
                @PanCamera.canceled -= m_Wrapper.m_TutorialActionsCallbackInterface.OnPanCamera;
                @MoveForward.started -= m_Wrapper.m_TutorialActionsCallbackInterface.OnMoveForward;
                @MoveForward.performed -= m_Wrapper.m_TutorialActionsCallbackInterface.OnMoveForward;
                @MoveForward.canceled -= m_Wrapper.m_TutorialActionsCallbackInterface.OnMoveForward;
                @Interact.started -= m_Wrapper.m_TutorialActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_TutorialActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_TutorialActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_TutorialActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Continue.started += instance.OnContinue;
                @Continue.performed += instance.OnContinue;
                @Continue.canceled += instance.OnContinue;
                @PanCamera.started += instance.OnPanCamera;
                @PanCamera.performed += instance.OnPanCamera;
                @PanCamera.canceled += instance.OnPanCamera;
                @MoveForward.started += instance.OnMoveForward;
                @MoveForward.performed += instance.OnMoveForward;
                @MoveForward.canceled += instance.OnMoveForward;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public TutorialActions @Tutorial => new TutorialActions(this);
    public interface IPlayerControlsActions
    {
        void OnThrust(InputAction.CallbackContext context);
        void OnStrafe(InputAction.CallbackContext context);
        void OnUpDown(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnLookMovement(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IDialogueActions
    {
        void OnContinueDialogue(InputAction.CallbackContext context);
    }
    public interface ITutorialActions
    {
        void OnContinue(InputAction.CallbackContext context);
        void OnPanCamera(InputAction.CallbackContext context);
        void OnMoveForward(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
