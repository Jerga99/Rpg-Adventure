using UnityEngine;
using System.Collections;

namespace RpgAdventure
{
    public class QuestGiver : MonoBehaviour
    {
        public Quest quest;


        private void OnMouseEnter()
        {
            Debug.Log("OnMouseEnter");
        }

        private void OnMouseExit()
        {
            Debug.Log("OnMouseExit");
        }
    }
}

