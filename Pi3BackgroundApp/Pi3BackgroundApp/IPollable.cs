namespace Pi3BackgroundApp
{
    internal interface IPollable<out TValue>
    {
        TValue GetValue();
    }
}
