using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private GameObject explodeFX;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            Destroy(gameObject);
            GameObject Fx = Instantiate(explodeFX, transform.position, Quaternion.identity);
            Destroy(Fx);
        }
    }
}
