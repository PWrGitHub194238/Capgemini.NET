using System;
using System.Collections.Generic;
using System.Text;

namespace _16.SwitchExpressions.CompositePattern
{
    internal abstract class ABox : IBox
    {
        private static int ident = 0;

        protected static string Ident { get => new string(' ', ident * 2); }

        protected Type type;

        protected Stack<IBox> boxes;

        public ABox() => type = GetType();

        public virtual void Push(IBox box) => boxes?.Push(box);

        public virtual void GetInside()
        {
            if (type.Equals(typeof(Box)))
            {
                GetInsideBox();
            }
            else if (type.Equals(typeof(Book)))
            {
                PrintLabel();
            }
        }

        protected void GetInsideBox()
        {
            PrintLabel();
            AddIdent();
            while (boxes.TryPop(out IBox box))
            {
                GetInsideBox(box);
            }
            RemoveIdent();
        }

        protected virtual void GetInsideBox(IBox box) => box?.GetInside();

        protected void PrintIcon(IconEnum icon)
        {
            Console.Write(' ');
            Console.Write((char)icon);
            Console.Write(' ');
        }

        protected static void AddIdent() => ident += 1;

        protected static void RemoveIdent() => ident -= 1;

        public abstract void PrintLabel();

        public void PrintLabel(string label) => Console.WriteLine($"{Ident}{label}");

        public virtual IBox Pop() => boxes?.Pop();
    }
}
