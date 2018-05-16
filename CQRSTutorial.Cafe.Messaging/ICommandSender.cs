using System.Threading.Tasks;
using CQRSTutorial.Cafe.Commands;

namespace CQRSTutorial.Cafe.Messaging
{
    public interface ICommandSender
    {
        Task Send<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}