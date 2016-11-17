namespace PlantSitter
{
    internal interface IOutputDevice<in TMessage> : IDevice
    {
        void Send(TMessage message);
    }
}
