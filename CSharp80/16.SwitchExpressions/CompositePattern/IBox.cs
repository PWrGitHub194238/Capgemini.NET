namespace _16.SwitchExpressions.CompositePattern
{
    internal interface IBox
    {
        void Push(IBox box);

        void GetInside();

        void PrintLabel();

        IBox Pop();
    }
}
