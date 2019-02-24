using System;
using System.Collections.Generic;

namespace _15.RecursivePatterns.CompositePattern
{
    internal class Box : Program.BookBox
    {
        public string Label { get; set; }

        public Box() : base() => boxes = new Stack<IBox>();

        public override void PrintLabel() => Console.WriteLine($"{Ident}{Label}");

        public void Deconstruct(out (int length, bool startsWithB) metadata, out int ident)
            => (metadata, ident) = ((Label.Length, Label.StartsWith('B')), Ident.Length / 2);
    }
}