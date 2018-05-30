using CQRSTutorial.Cafe.Common;
using System.Threading.Tasks;

namespace CQRSTutorial.Cafe.Messaging
{
    public interface ICommandSender
    {
        Task Send<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}