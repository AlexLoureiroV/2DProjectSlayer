using UnityEngine;

public class InputController : MonoBehaviour
{
    #region Fields
    [SerializeField] private Jetpack _jetpack;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        if (_jetpack == null)
        {
            _jetpack = GetComponent<Jetpack>();
            if (_jetpack == null)
                Debug.LogError("Jetpack NO encontrado en " + gameObject.name);
            else
                Debug.Log("Jetpack encontrado en " + gameObject.name);
        }
        else
        {
            Debug.Log("Jetpack asignado desde Inspector en " + gameObject.name);
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal < 0)
        {
            _jetpack.FlyHorizontal(Jetpack.Direction.Left);
            _jetpack.WalkHorizontal(Jetpack.Direction.Left);
        }
        else if (horizontal > 0)
        {
            _jetpack.FlyHorizontal(Jetpack.Direction.Right);
            _jetpack.WalkHorizontal(Jetpack.Direction.Right);
        }
        else
        {
            _jetpack.StopWalking();
        }

        if (vertical > 0)
            _jetpack.FlyUp();
        else
            _jetpack.StopFlying();
    }
    #endregion
}