using System;

namespace CQRSTutorial.Cafe.Common
{
    public interface ICommand
    {
        Guid Id { get; set; }
    }
}
