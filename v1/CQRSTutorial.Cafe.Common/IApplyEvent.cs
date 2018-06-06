namespace CQRSTutorial.Cafe.Common
{
    public interface IApplyEvent<TEvent>
    {
        void Apply(TEvent e);
    }
}