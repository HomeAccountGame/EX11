using Fusion;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] NumberField healthDisplay;

    [Networked(OnChanged = nameof(NetworkedHealthChanged))]
    public int NetworkedHealth { get; set; } = 100;

    private static void NetworkedHealthChanged(Changed<Health> changed)
    {
        Debug.Log($"Health changed to: {changed.Behaviour.NetworkedHealth}");
        changed.Behaviour.healthDisplay.SetNumber(changed.Behaviour.NetworkedHealth);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(int damage)
    {
        NetworkedHealth -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!HasStateAuthority)
            return;

        if (other.CompareTag("bullet"))
        {
            int damage = other.GetComponent<bullet>().Damage;
            DealDamageRpc(damage);
        }
    }
}