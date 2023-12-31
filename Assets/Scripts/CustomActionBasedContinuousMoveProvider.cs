﻿using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace UnityEngine.XR.Interaction.Toolkit
{
    /// <summary>
    /// Locomotion provider that allows the user to smoothly move their rig continuously over time
    /// using a specified input action.
    /// </summary>
    /// <seealso cref="LocomotionProvider"/>
    [AddComponentMenu("XR/Locomotion/Custom Continuous Move Provider (Action-based)", 11)]
    public class CustomActionBasedContinuousMoveProvider : CustomContinuousMoveProviderBase
    {
        [SerializeField]
        [Tooltip("The Input System Action that will be used to read Move data from the left hand controller. Must be a Value Vector2 Control.")]
        InputActionProperty m_LeftHandMoveAction;
        /// <summary>
        /// The Input System Action that Unity uses to read Move data from the left hand controller. Must be a <see cref="InputActionType.Value"/> <see cref="Vector2Control"/> Control.
        /// </summary>
        public InputActionProperty leftHandMoveAction
        {
            get => m_LeftHandMoveAction;
            set => SetInputActionProperty(ref m_LeftHandMoveAction, value);
        }

        [SerializeField]
        [Tooltip("The Input System Action that will be used to read Move data from the right hand controller. Must be a Value Vector2 Control.")]
        InputActionProperty m_RightHandMoveAction;
        /// <summary>
        /// The Input System Action that Unity uses to read Move data from the right hand controller. Must be a <see cref="InputActionType.Value"/> <see cref="Vector2Control"/> Control.
        /// </summary>
        public InputActionProperty rightHandMoveAction
        {
            get => m_RightHandMoveAction;
            set => SetInputActionProperty(ref m_RightHandMoveAction, value);
        }

        [SerializeField]
        [Tooltip("The Input System Action that will be used to read a button from the left hand controller. Must be a Button Control.")]
        InputActionProperty m_RightHandJumpAction;
        /// <summary>
        /// The Input System Action that Unity uses to read Move data from the left hand controller. Must be a <see cref="InputActionType.Value"/> <see cref="Vector2Control"/> Control.
        /// </summary>
        public InputActionProperty rightHandjumpAction
        {
            get => m_RightHandJumpAction;
            set => SetInputActionProperty(ref m_RightHandJumpAction, value);
        }

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        protected void OnEnable()
        {
            m_LeftHandMoveAction.EnableDirectAction();
            m_RightHandMoveAction.EnableDirectAction();
            m_RightHandJumpAction.EnableDirectAction();
        }

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        protected void OnDisable()
        {
            m_LeftHandMoveAction.DisableDirectAction();
            m_RightHandMoveAction.DisableDirectAction();
            m_RightHandJumpAction.DisableDirectAction();
        }

        /// <inheritdoc />
        protected override Vector2 ReadInput()
        {
            var leftHandValue = m_LeftHandMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
            var rightHandValue = m_RightHandMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;

            return leftHandValue + rightHandValue;
        }

        protected override float ReadButton()
        {
            float jumpValue = m_RightHandJumpAction.action?.ReadValue<float>() ?? 0f;
            return jumpValue;
        }

        void SetInputActionProperty(ref InputActionProperty property, InputActionProperty value)
        {
            if (Application.isPlaying)
                property.DisableDirectAction();

            property = value;

            if (Application.isPlaying && isActiveAndEnabled)
                property.EnableDirectAction();
        }
    }
}
