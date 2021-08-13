using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Class for defining the camera position
    /// to view the selected target.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject _katamari = null;
        private Vector3 _offset;
        private float _rotationSpeed = 7f;
        private float _smoothFactor = 1.0f;
        private float _horizontalInput = 0;

        /// <summary>
        /// Start is called before the first frame update.
        /// Initializes the distance between camera and target.
        /// </summary>
        private void Start()
        {
            _offset = transform.position - _katamari.transform.position;
        }

        /// <summary>
        /// Update is called once per frame.
        /// Updates values that store player's input.
        /// </summary>
        private void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
        }

        /// <summary>
        /// LateUpdate is called every frame, after all Update functions have been called.
        /// Updates camera's position and rotation to be behind the player in a fixed distance,
        /// according to player's horizontal input (right and left arrows).
        /// </summary>
        private void LateUpdate()
        {
            var cameraTurnAngle = Quaternion.AngleAxis(_horizontalInput * _rotationSpeed, Vector3.up);
            _offset = cameraTurnAngle * _offset;
            transform.position = Vector3.Slerp(transform.position, _katamari.transform.position + _offset, _smoothFactor);
            transform.LookAt(_katamari.transform);
        }
    }
}
