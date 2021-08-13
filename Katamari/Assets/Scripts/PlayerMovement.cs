using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Class for defining the movement of the katamari ball 
    /// according to the player input.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _katamari = null;
        [SerializeField] private GameObject _camera = null;
        private float _verticalInput = 0;
        private  float _movementSpeed = 12.0f;


        /// <summary>
        /// Start is called before the first frame update. 
        /// Initializes Rigidbody component.
        /// </summary>
        private void Start()
        {
            _katamari = GetComponent<Rigidbody>();  
        }

        /// <summary>
        /// Update is called once per frame.
        /// Updates values that store player's vertical input (up and down arrows).
        /// </summary>
        private void Update()
        {
            _verticalInput = Input.GetAxis("Vertical");
        }

        /// <summary>
        /// FixedUpdate is called every fixed frame-rate frame and it is used for physics calculations.
        /// Gets camera's facing direction and moves player towards it.
        /// Rolls the katamari ball according to player input by adding the force.
        /// </summary>
        private void FixedUpdate()
        {
            var cameraForward = _camera.transform.forward;
            cameraForward = new Vector3(cameraForward.x, 0f, cameraForward.z);
            cameraForward.Normalize();
            var cameraXAxis = cameraForward * _verticalInput;
            _katamari.AddForce(cameraXAxis * _movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}
