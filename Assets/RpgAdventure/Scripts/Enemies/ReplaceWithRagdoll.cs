using UnityEngine;
using System.Collections;

namespace RpgAdventure
{
    public class ReplaceWithRagdoll : MonoBehaviour
    {
        public GameObject ragdollPrefab;

        public void Replace()
        {
            GameObject ragdolInstance = Instantiate(ragdollPrefab, transform.position, transform.rotation);

            // iterate over all children of transform and copy positions and rotations to the ragdoll

            Destroy(gameObject);
        }
    }

}