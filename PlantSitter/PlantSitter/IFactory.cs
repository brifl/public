namespace PlantSitter
{
    interface IFactory<out T>
    {
        T Build();
    }
}