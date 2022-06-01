namespace Wsdot.Wzdx.Core
{
    public interface IFactory<out TBuilder>
    {
        TBuilder Create();
    }
}