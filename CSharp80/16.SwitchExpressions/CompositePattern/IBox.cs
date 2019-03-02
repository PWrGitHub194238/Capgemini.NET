namespace _16.SwitchExpressions.CompositePattern
{
    internal interface IBox
    {
        void Push(IBox box);

        void GetInside();

        void PrintLabel();

        void PrintLabel(string label);

        IBox Pop();
    }
}
