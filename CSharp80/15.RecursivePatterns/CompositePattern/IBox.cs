namespace _15.RecursivePatterns.CompositePattern
{
    internal interface IBox
    {
        void Push(IBox box);

        void GetInside();

        void PrintLabel();

        IBox Pop();
    }
}
