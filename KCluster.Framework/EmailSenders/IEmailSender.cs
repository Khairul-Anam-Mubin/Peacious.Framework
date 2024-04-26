namespace KCluster.Framework.EmailSenders;

public interface IEmailSender
{
    Task SendAsync(Email email);
}