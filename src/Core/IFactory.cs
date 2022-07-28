namespace Wzdx.Core
{
    public interface IFactory<out TBuilder>
    {
        TBuilder Create();
    }
}