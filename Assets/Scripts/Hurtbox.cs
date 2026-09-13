using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private Fighter fighter;

    private Height hurtboxHeight;
    private GameObject storedGameObjectOnTrigger;

    private void Awake()
    {
        hurtboxHeight = gameObject.layer == 6 ? Height.High : Height.Low;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null || other.gameObject == null) return;
            
        storedGameObjectOnTrigger = other.gameObject;

        if (storedGameObjectOnTrigger.layer == (int)hurtboxHeight)
        {
            Debug.Log("Hitbox collided with hurtbox " + hurtboxHeight);
            //false means the attacker got blocked
            if (fighter.BlockCheck(hurtboxHeight))
            {
                storedGameObjectOnTrigger.GetComponent<FighterAttack>()?.GotBlocked();
            }
        }
    }
}
