using System;
using System.Collections.Generic;

namespace _16.SwitchExpressions.CompositePattern
{
    internal class Box : Program.BookBox
    {
        #region
        public string Label { get; set; }
        public char StartingLetter => Label.Length > 0 ? Label[1] : char.MinValue;

        public char EndingLetter => Label.Length > 0 ? Label[Label.Length - 1] : char.MaxValue;

        public Box() : base() => boxes = new Stack<IBox>();

        public override void PrintLabel() => Console.WriteLine($"{Ident}{Label}");
        #endregion

        public void Deconstruct(
            out (
                bool letterGtD,
                bool letterGeA,
                bool lengthGt5) metadata,
            out int ident) =>
            (metadata, ident) = ((
                StartingLetter > 'D',
                StartingLetter >= 'A',
                Label.Length >= 5
            ),
            Ident.Length / 2);
    }
}