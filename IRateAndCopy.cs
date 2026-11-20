namespace Lab7_Variant2
{
    public interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }
}