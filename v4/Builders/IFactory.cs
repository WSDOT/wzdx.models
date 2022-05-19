namespace Wsdot.Wzdx.v4.Builders
{
    public interface IBuilder<out TResult>
    {
        TResult Result();
    }

    public interface IFactory<out TBuilder>
    {
        TBuilder Create();
    }
}