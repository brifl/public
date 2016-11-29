namespace Pi3BackgroundApp
{
    internal interface IFactory<out T>
    {
        T Build();
    }
}
