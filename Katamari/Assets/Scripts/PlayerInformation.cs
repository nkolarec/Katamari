using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// Class for viewing information about hierarchy and katamari mass
    /// </summary>
    public class PlayerInformation : MonoBehaviour
    {
        [SerializeField] private Text _katamariMass = null;
        [SerializeField] private Text _hierarchy = null;
        private Rigidbody _katamari = null;
        private Renderer [] _renderers = null;

        /// <summary>
        /// Start is called before the first frame update.
        /// Initialize Rigidbody component.
        /// </summary>
        private void Start()
        {
            _katamari = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Update is called once per frame.
        /// Get rendered objects and update the view of hierarchy and katamari ball mass
        /// </summary>
        private void Update()
        {
            _katamariMass.text = "Katamari mass: " + _katamari.mass.ToString("0.0");
            _hierarchy.text = "";
            _renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in _renderers)
            {
                _hierarchy.text += FormatLine(renderer.gameObject);
            }
        }

        /// <summary>
        /// A function for formatting line in a scroll view for an object in hierarchy of the katamari ball.
        /// </summary>
        private static string FormatLine(GameObject objectInHierarchy)
        {
            string line = "   ";
            string objectName = objectInHierarchy.name;
            if (objectInHierarchy.transform.parent == null) return "\n   > " + objectName + "   \n";
            while (objectInHierarchy.transform.parent != null)
            {
                objectInHierarchy = objectInHierarchy.transform.parent.gameObject;
                line += "   ";
            }
            return line + "> " + objectName + "\n";
        }
    }
}
