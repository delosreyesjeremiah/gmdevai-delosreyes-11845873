using UnityEngine;

public class Flee : NPCBaseFSM
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Flee from target
        Vector3 direction = NPC.transform.position - Opponent.transform.position;
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * RotationSpeed);
        NPC.transform.Translate(0.0f, 0.0f, Time.deltaTime * MovementSpeed);
    }
}
