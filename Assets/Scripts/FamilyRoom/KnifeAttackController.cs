﻿using UnityEngine;

public class KnifeAttackController : MonoBehaviour
{
    [SerializeField]
    private KeyCode _attackKey;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(_attackKey))
        {
            if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Knife"))
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        if(Random.Range(0f, 2f) <= 1f)
        {
            _animator.SetTrigger("Attack1");
        }
        else
        {
            _animator.SetTrigger("Attack2");
        }

        //Raycast logic
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if(hit.distance > 0.8f)
            {
                return;
            }
            if (hit.transform.tag == "Shootable")
            {
                hit.transform.GetComponent<Shootable>().Shot("");
            }
        }
    }
}
