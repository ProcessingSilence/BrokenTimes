using UnityEngine;

public class BladeHitbox : MonoBehaviour
{
    public int hitPlayerFlag;

    public PlayerController my_PlayerController_script;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (hitPlayerFlag == 0)
            {
                hitPlayerFlag = 1;
                my_PlayerController_script = other.GetComponent<PlayerController>();
            }
        }
    }
}
