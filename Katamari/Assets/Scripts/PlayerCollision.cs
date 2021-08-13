using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class that implements actions during a collision.
/// </summary>
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject _stickyItems = null;
    private Dictionary<string, float> _oldMassesOfStickyItems = null;

    /// <summary>
    /// Start is called before the first frame update.
    /// Initialize storage for masses of sticky items.
    /// </summary>
    private void Start()
    {
        _oldMassesOfStickyItems = new Dictionary<string, float>();
    }

    /// <summary>
    /// On a collision check if the collision object has a tag "Sticky", otherwise ignore it.
    /// If an object has a tag "Sticky" and has a smaller mass than katamari ball, add it to the katamari ball, 
    /// store it's mass to the storage and add it to the katamari ball's mass.
    /// If an object has a tag "Sticky" and has a greater mass than katamari ball, get its previous mass and 
    /// substract katamari ball's mass by it and remove the object with it's previous mass.
    /// </summary>
    /// <param name="collision">Object that katamari ball has collided with.</param>
    private void OnCollisionStay(Collision collision)
    { 
        GameObject otherObject = collision.gameObject;

        if (otherObject.tag.Equals("Sticky"))
        {
            Collider collider = collision.GetContact(0).thisCollider;
            float otherMass = otherObject.GetComponent<Rigidbody>().mass;
            if (GetKatamariMass() > otherMass)
            {
                if (!_oldMassesOfStickyItems.ContainsKey(otherObject.name))
                {
                    Destroy(otherObject.GetComponent<Rigidbody>());
                    otherObject.transform.SetParent(collider.transform);
                    _oldMassesOfStickyItems.Add(otherObject.name, otherMass);
                    UpdateKatamariMass(otherMass, "add");
                    Debug.Log("Object " + otherObject.name + " has been added to Katamari with mass: " + otherMass.ToString());
                }
            }

            else if (GetKatamariMass() < otherMass && !collider.gameObject.CompareTag("Player"))
            {
                Transform [] children = collider.GetComponentsInChildren<Transform>();
                foreach (Transform child in children)
                {
                    if ( _oldMassesOfStickyItems.ContainsKey(child.gameObject.name))
                    {
                        child.SetParent(_stickyItems.transform);
                        child.gameObject.AddComponent<Rigidbody>();
                        float oldMass = _oldMassesOfStickyItems[child.gameObject.name];
                        _oldMassesOfStickyItems.Remove(child.gameObject.name);
                        child.gameObject.GetComponent<Rigidbody>().mass = oldMass;
                        UpdateKatamariMass(oldMass, "substract");
                        Debug.Log("Object " + child.gameObject.name + " has been removed from Katamari with mass: " + oldMass.ToString());
                    }
                }
            }
        }
    }

    /// <summary>
    /// A helper function to get katamari ball's current mass.
    /// </summary>
    /// <returns>Mass of the katamari ball</returns>
    private float GetKatamariMass()
    {
        return gameObject.GetComponent<Rigidbody>().mass;
    }

    /// <summary>
    /// A helper function to change katamari ball's mass.
    /// </summary>
    /// <param name="mass">Mass to be added to katamari ball.</param>
    /// <param name="operation">Determines the type of operation that will be performed (substraction or addition)</param>
    private void UpdateKatamariMass(float mass, string operation)
    {
        if (operation == "add")
            gameObject.GetComponent<Rigidbody>().mass += mass;
        else if (operation == "substract")
            gameObject.GetComponent<Rigidbody>().mass -= mass;
        else
            Debug.Log("Error while doing operation.");
    }
}
