using System;
using UnityEngine;

namespace RpgAdventure
{
    [System.Serializable]
    public class DialogAnswer
    {
        [TextArea(3, 15)]
        public string text;
        public bool forceDialogQuit;
        public string questId;
    }

    [System.Serializable]
    public class DialogQuery
    {
        [TextArea(3, 15)]
        public string text;
        public DialogAnswer answer;
        public bool isAsked;
        public bool isAlwaysAsked;
    }

    [System.Serializable]
    public class Dialog
    {
        [TextArea(3, 15)]
        public string welcomeText;
        public DialogQuery[] queries;
    }
}