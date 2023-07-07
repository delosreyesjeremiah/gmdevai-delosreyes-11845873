using UnityEngine;

public class NPCBaseFSM : StateMachineBehaviour
{
    protected GameObject NPC
    {
        get => _NPC;
    }

    protected GameObject Opponent
    {
        get => _opponent;
    }

    protected float MovementSpeed
    {
        get => _movementSpeed;
    }

    protected float RotationSpeed
    {
        get => _rotationSpeed;
    }

    protected float Accuracy
    {
        get => _accuracy;
    }

    [SerializeField] private GameObject _NPC;
    [SerializeField] private GameObject _opponent;

    private float _movementSpeed = 2.0f;
    private float _rotationSpeed = 1.0f;
    private float _accuracy = 3.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _NPC = animator.gameObject;
        _opponent = _NPC.GetComponent<TankAI>().Player;
    }

    protected void MoveTowardsTarget(GameObject target)
    {
        Vector3 direction = target.transform.position - NPC.transform.position;
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * RotationSpeed);
        NPC.transform.Translate(0.0f, 0.0f, Time.deltaTime * MovementSpeed);
    }
}
