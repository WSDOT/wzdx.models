namespace Wsdot.Wzdx.Core
{
    public interface IBuilder<out TResult>
    {
        TResult Result();
    }
}