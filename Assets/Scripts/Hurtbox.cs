using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private Fighter fighter;
    private Height hitboxHeight;

    private void Awake()
    {
        hitboxHeight = gameObject.layer == 6 ? Height.High : Height.Low;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        fighter.HitCheck(hitboxHeight);
    }
    
}
