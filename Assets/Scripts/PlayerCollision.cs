using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private Collider2D _playerCollider;
    private void Start()
    {
        _playerCollider = GetComponent<Collider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out JumpPad jumpPad))
        {
            playerController.Jump(jumpPad.GetJumpPadForce(), jumpPad.GetAdditionalSleepJumpTime());
            jumpPad.TriggerJumpPad();
        }
        else if (col.TryGetComponent(out Collectibles collectible))
        {
            var collectibleType = collectible.GetCollectibleInfoOnContact();

            switch (collectibleType)
            {
                case CollectibleType.DoubleJump:
                    playerController.EnableDoubleJump();
                    break;
                case CollectibleType.RefillHealth:
                case CollectibleType.RefillEnergy:
                case CollectibleType.RefillDash:
                default:
                    break;
            }
            
            Debug.Log(collectibleType);
        }

        if (_playerCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            playerController.TakeDamage();
        }
    }
}
