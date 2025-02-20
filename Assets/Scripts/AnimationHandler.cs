using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");

    protected Animator[] animators;

    protected virtual void Awake()
    {
        animators = GetComponentsInChildren<Animator>(true); // true �ɼ��� �߰��ؼ� ��Ȱ��ȭ�� ������Ʈ�� �˻�
    }


    public void Move(Vector2 obj)
    {
        bool isMoving = obj.magnitude > 0.5f;

        foreach (Animator anim in animators)
        {
            if (anim.gameObject.activeInHierarchy) // Ȱ��ȭ�� ������Ʈ�� ����
                anim.SetBool(IsMoving, isMoving);
        }
    }

    public void Damage()
    {
        foreach (Animator anim in animators)
        {
            if (anim.gameObject.activeInHierarchy)
                anim.SetBool(IsDamage, true);
        }
    }

    public void InvincibilityEnd()
    {
        foreach (Animator anim in animators)
        {
            if (anim.gameObject.activeInHierarchy)
                anim.SetBool(IsDamage, false);
        }
    }
}
