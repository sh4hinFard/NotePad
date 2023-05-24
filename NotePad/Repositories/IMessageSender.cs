namespace NotePad.Repositories
{
    public interface IMessageSender
    {
        void SendMessageSmtp(string email,string username, int Id);
    }
}
