using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 2;

        [SerializeField]
        private float _rotateSpeed = 0.08f;

        public const string TAG = "Player";

        private void Update()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
            {
                transform.position += transform.forward * _speed * Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            Vector3 movementRotated = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * movement;

            if (movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementRotated.normalized), _rotateSpeed);
        }
    }
}