using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private int _speed = 2;

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += -transform.right * _speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position += -transform.forward * _speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * _speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * _speed * Time.deltaTime;
            }
        }
    }
}