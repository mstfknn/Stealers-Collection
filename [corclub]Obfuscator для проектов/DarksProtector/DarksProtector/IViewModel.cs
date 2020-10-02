namespace DarksProtector
{
    public interface IViewModel<TModel>
    {
        TModel Model { get; }
    }
}