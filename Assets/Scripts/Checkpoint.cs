using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool _activateOnStart = false;
    [SerializeField] private GameObject _signObject; // arrastra el PNG aquí

    private void Start()
    {
        if (_signObject != null)
        {
            _signObject.SetActive(false);
            Debug.Log("Sign oculto: " + _signObject.name);
        }
        else
        {
            Debug.Log("Sign Object no asignado en: " + gameObject.name);
        }

        if (_activateOnStart)
            ActivateForNearbyPlayer();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Health health = col.GetComponent<Health>();
        if (health != null)
        {
            health.SetRespawnPoint(transform.position);
            ShowSign();
        }
    }

    private void ShowSign()
    {
        if (_signObject != null)
            _signObject.SetActive(true);
    }

    private void ActivateForNearbyPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            player.GetComponent<Health>()?.SetRespawnPoint(transform.position);
    }
}