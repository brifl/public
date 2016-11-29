namespace Pi3BackgroundApp
{
    internal interface IOutputDevice<in TMessage> : IDevice
    {
        void Send(TMessage message);
    }
}
