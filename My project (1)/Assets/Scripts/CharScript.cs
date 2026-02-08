using UnityEngine;

public class CharScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Animate()
    {
        animator.SetBool("Bounce", true);
    }
    public void Stop()
    {
        
        animator.SetBool("Bounce", false);
    }
}
