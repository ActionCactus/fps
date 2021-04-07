using UnityEngine;
using FPS.Player.Data;
using FPS.Player.MovementSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementData))]
[RequireComponent(typeof(Crouch))] // Hack for now
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Transform))]
public class CrouchSlide : MonoBehaviour
{
    private MovementData movementData;
    private InputAction crouchButton;
    private Collider playerCollider;
    private Transform playerTransform;
    private PhysicMaterial playerMaterial;

    public void Start()
    {
        this.crouchButton = GetComponent<Crouch>().InputAction;
        this.playerCollider = this.GetComponent<Collider>();
        this.playerTransform = this.GetComponent<Transform>();
        this.movementData = GetComponent<MovementData>();
        this.playerMaterial = this.playerCollider.material;
    }

    public void Update()
    {
        if (
            ((!this.crouchButton.IsPressed() && this.movementData.IsActive(PlayerMovementStates.CrouchSliding)) ||
            (this.movementData.IsActive(PlayerMovementStates.CrouchSliding) && !this.movementData.IsActive(PlayerMovementStates.Sprinting)))
        )
        {
            // Stop slide
            this.movementData.SetInactive(PlayerMovementStates.CrouchSliding);
            this.playerCollider.transform.localScale = CrouchUtil.unshrink(this.playerCollider.transform.localScale);
            this.playerTransform.transform.localScale = CrouchUtil.unshrink(this.playerTransform.transform.localScale);
            this.playerMaterial.dynamicFriction *= 2;
            this.playerMaterial.staticFriction *= 2;
        }
        else if (
            this.crouchButton.IsPressed() &&
            !this.movementData.IsActive(PlayerMovementStates.CrouchSliding) &&
            this.movementData.IsActive(PlayerMovementStates.Sprinting))
        {
            // Start slide
            this.movementData.SetActive(PlayerMovementStates.CrouchSliding);
            this.playerCollider.transform.localScale = CrouchUtil.shrink(this.playerCollider.transform.localScale);
            this.playerTransform.transform.localScale = CrouchUtil.shrink(this.playerTransform.transform.localScale);
            this.playerMaterial.dynamicFriction /= 2;
            this.playerMaterial.staticFriction /= 2;
        }

        // Maintain slide
    }
}
