namespace Pi3BackgroundApp.Common
{
    internal interface IOutputDevice<in TMessage> : IDevice
    {
        void Send(TMessage message);
    }
}
