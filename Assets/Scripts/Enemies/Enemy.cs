using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
