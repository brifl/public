namespace Pi3BackgroundApp.Common
{
    internal interface IPollable<out TValue>
    {
        TValue GetValue();
    }
}
