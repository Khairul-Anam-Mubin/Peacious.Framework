namespace Peacious.Framework.MessageBrokers;

public class MessageBrokerConfig
{
    public string MessageBrokerName { get; private set; }
    public string Host { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }

    public MessageBrokerConfig(string messageBrokerName, string host, string userName, string password)
    {
        MessageBrokerName = messageBrokerName;
        Host = host;
        UserName = userName;
        Password = password;
    }
}