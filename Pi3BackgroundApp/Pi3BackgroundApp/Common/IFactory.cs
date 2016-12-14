namespace Pi3BackgroundApp.Common
{
    internal interface IFactory<out T>
    {
        T Build();
    }
}
