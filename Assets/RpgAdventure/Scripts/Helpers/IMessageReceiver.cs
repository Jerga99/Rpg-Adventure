using System;

namespace RpgAdventure
{
    public enum MessageType
    {
        DAMAGED,
        DEAD
    }

    public interface IMessageReceiver
    {
        void OnReceiveMessage(MessageType type);
    }
}

