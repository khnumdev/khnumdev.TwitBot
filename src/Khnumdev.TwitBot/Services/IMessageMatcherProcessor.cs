namespace Khnumdev.TwitBot.Services
{
    using System.Threading.Tasks;

    public interface IMessageMatcherProcessor
    {
        Task<string> ProcessAsync(string input);
    }
}
