using UnityEngine;
using System.Collections;

namespace RpgAdventure
{
    public class ReplaceWithRagdoll : MonoBehaviour
    {
        public GameObject ragdollPrefab;

        public void Replace()
        {
            GameObject ragdollInstance = Instantiate(ragdollPrefab, transform.position, transform.rotation);
            ragdollInstance.SetActive(false);

            Transform ragdollTransform = ragdollInstance.transform;
            Transform currentTransform = transform;

            ragdollTransform.rotation = currentTransform.rotation;
            ragdollTransform.position = currentTransform.position;

            while (currentTransform != null && ragdollTransform != null)
            {
                if (currentTransform.name == ragdollTransform.name)
                {
                    ragdollTransform.position = currentTransform.position;
                    ragdollTransform.rotation = currentTransform.rotation;
                }

                if (currentTransform.childCount > 0)
                {
                    currentTransform = currentTransform.GetChild(0);
                    ragdollTransform = ragdollTransform.GetChild(0);
                }
                else
                {
                    while (currentTransform != null)
                    {
                        if (currentTransform.parent == null || ragdollTransform.parent == null)
                        {
                            currentTransform = null;
                            ragdollTransform = null;
                        }
                        else if (currentTransform.GetSiblingIndex() + 1 == currentTransform.parent.childCount ||
                                 currentTransform.GetSiblingIndex() + 1 == ragdollTransform.parent.childCount)
                        {
                            currentTransform = currentTransform.parent;
                            ragdollTransform = ragdollTransform.parent;
                        }
                        else
                        {
                            currentTransform = currentTransform.parent.GetChild(currentTransform.GetSiblingIndex() + 1);
                            ragdollTransform = ragdollTransform.parent.GetChild(ragdollTransform.GetSiblingIndex() + 1);
                            break;
                        }
                    }
                }
            }


            ragdollInstance.SetActive(true);
            Destroy(gameObject);
        }
    }

}