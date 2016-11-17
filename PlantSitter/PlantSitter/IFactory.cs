namespace PlantSitter
{
    internal interface IFactory<out T>
    {
        T Build();
    }
}
