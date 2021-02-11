using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace RpgAdventure
{
    public class QuestManager : MonoBehaviour
    {
        public Quest[] quests;

        private void Awake()
        {
            LoadQuestsFromDB();
        }

        private void LoadQuestsFromDB()
        {
            using (StreamReader reader = new StreamReader("Assets/RpgAdventure/DB/QuestDB.json"))
            {
                string json = reader.ReadToEnd();
                quests = JsonUtility.FromJson<Quest[]>(json);
                Debug.Log(quests);
            }
        }
    }
}
