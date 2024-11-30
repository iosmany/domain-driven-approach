namespace App.Domain.Services
{
    public interface IBus
    {
        void Send(string message);
    }
}