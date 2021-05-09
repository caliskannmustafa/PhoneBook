using System;

namespace Phonebook.EventBus
{
    public interface IEventBus
    {
        void PushMessage(string queueName, string message);
    }
}
