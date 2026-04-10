using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            winPanel.SetActive(true);
        }
    }
}