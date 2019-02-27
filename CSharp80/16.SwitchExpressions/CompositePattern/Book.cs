using System;

namespace _16.SwitchExpressions.CompositePattern
{
    internal class Book : Program.BookBox
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string BookType { get; set; } = string.Empty;

        public override void PrintLabel() => Console.WriteLine($"{Ident}[{BookType.PadRight(10)}] {Title}" +
            $"{(Subtitle != null ? $"\n{Ident}{string.Empty.PadRight(10 + 6)}" + Subtitle : "")}");
    }
}
