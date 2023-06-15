using Fusion;
using UnityEngine;

public class scoreadd : NetworkBehaviour
{
    [SerializeField] NumberField scoreDisplay;

    [Networked(OnChanged = nameof(NetworkedScoreChanged))]
    public int NetworkedScore { get; set; } = 0;
    private static void NetworkedScoreChanged(Changed<scoreadd> changed) {
        // Here you would add code to update the player's healthbar.
        Debug.Log($"Score changed to: {changed.Behaviour.NetworkedScore}");
        changed.Behaviour.scoreDisplay.SetNumber(changed.Behaviour.NetworkedScore);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    // All players can call this function; only the StateAuthority receives the call.
    public void AddScoreRpc(int score) {
        // The code inside here will run on the client which owns this object (has state and input authority).
        Debug.Log("Received AddScoreRpc on StateAuthority, modifying Networked variable");
        NetworkedScore += score;
    }
}
