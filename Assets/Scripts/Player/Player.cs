using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask soildObjectLayer;
    public LayerMask grassLayer;
    
    private bool isMoving;
    private Vector2 input;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isMoving)
        {
            
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            
            if (input.x != 0) input.y = 0;
            
            if (input != Vector2.zero)
            {
                anim.SetFloat("MoveX", input.x);
                anim.SetFloat("MoveY", input.y);
                
                var target = gameObject.transform.position;
                target.x += input.x;
                target.y += input.y;
                if (IsWalkable(target))
                {
                    StartCoroutine(Move(target));
                }
            }
        }
        anim.SetBool("isMoving", isMoving);
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
        
        CheckForEcounter();
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, soildObjectLayer) != null)
        {
            return false;
        }

        return true;
    }

    private void CheckForEcounter()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
        {
            if (Random.Range(0, 101) <= 10)
            {
               Debug.Log("pokemon!"); 
            }
        }
    }
}
