using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastAttack : NetworkBehaviour
{
    [SerializeField] int damage;

    [SerializeField] InputAction attack;
    [SerializeField] InputAction attackLocation;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float shootForce = 10f;

    private void OnEnable()
    {
        attack.Enable();
        attackLocation.Enable();
    }

    private void OnDisable()
    {
        attack.Disable();
        attackLocation.Disable();
    }

    private void OnValidate()
    {
        if (attack == null)
            attack = new InputAction(type: InputActionType.Button);
        if (attack.bindings.Count == 0)
            attack.AddBinding("<Mouse>/leftButton");

        if (attackLocation == null)
            attackLocation = new InputAction(type: InputActionType.Value, expectedControlType: "Vector2");
        if (attackLocation.bindings.Count == 0)
            attackLocation.AddBinding("<Mouse>/position");
    }

    void Update()
    {
        if (!HasStateAuthority)
            return;

        if (attack.WasPerformedThisFrame())
        {
            Vector2 attackLocationInScreenCoordinates = attackLocation.ReadValue<Vector2>();

            // Spawn bullet prefab and shoot it
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = bulletSpawnPoint.forward * shootForce;
        }
    }
}