namespace PlantSitter
{
    internal interface IPollable<out TValue>
    {
        TValue GetValue();
    }
}