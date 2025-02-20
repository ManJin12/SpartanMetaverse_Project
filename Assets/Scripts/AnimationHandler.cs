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
        animators = GetComponentsInChildren<Animator>(true); // true 옵션을 추가해서 비활성화된 오브젝트도 검색
    }


    public void Move(Vector2 obj)
    {
        bool isMoving = obj.magnitude > 0.5f;

        foreach (Animator anim in animators)
        {
            if (anim.gameObject.activeInHierarchy) // 활성화된 오브젝트만 적용
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
