using UnityEngine;
using Vuforia;

public class ProximityAttack : MonoBehaviour
{
    public Transform otherCharacter;
    public float attackDistance = 0.3f;

    private Animator animator;
    private const string ATTACK_PARAM = "IsAttacking";

    private ObserverBehaviour myTracker;
    private ObserverBehaviour otherTracker;

    private bool myVisible;
    private bool otherVisible;

    void Start()
    {
        animator = GetComponent<Animator>();
        myTracker = GetComponentInParent<ObserverBehaviour>();
        otherTracker = otherCharacter.GetComponentInParent<ObserverBehaviour>();

        myTracker.OnTargetStatusChanged += (_, s) =>
            myVisible = s.Status == Status.TRACKED || s.Status == Status.EXTENDED_TRACKED;

        otherTracker.OnTargetStatusChanged += (_, s) =>
            otherVisible = s.Status == Status.TRACKED || s.Status == Status.EXTENDED_TRACKED;
    }

    void Update()
    {
        if (!myVisible || !otherVisible)
        {
            animator.SetBool(ATTACK_PARAM, false);
            return;
        }

        float d = Vector3.Distance(transform.position, otherCharacter.position);
        bool shouldAttack = d < attackDistance;
        animator.SetBool(ATTACK_PARAM, shouldAttack);
    }
}