using ECS;
using Game.Input;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class ControllerMoveSystem : SystemBase<MoveDirection, FaceDirection, PlayerController>
    {
        private Vector3 inputValue;

        protected override void OnCreate()
        {
            base.OnCreate();

            if (Application.isMobilePlatform)
            {
                GameInput.Controller.Default.VirtualPadLeft.performed += OnMove;
                GameInput.Controller.Default.VirtualPadLeft.canceled += OnMove;
            }
            else
            {
                GameInput.Controller.Default.Move.performed += OnMove;
                GameInput.Controller.Default.Move.canceled += OnMove;
            }
        }


        void OnMove(InputAction.CallbackContext ctx)
        {
            var v2 = ctx.ReadValue<Vector2>();
            inputValue = new Vector3(v2.x, 0, v2.y);
        }

        protected override void OnUpdate(int index, Entity entity, MoveDirection move, FaceDirection face, PlayerController player)
        {
            Vector3 toward = Camera.main.transform.rotation * inputValue;
            move.Value = Vector3.ProjectOnPlane(toward, Vector3.up).normalized;
            if (!move.Value.Equals(float3.zero))
                face.Value = move.Value;

        }
    }
}
