#region Define

#region C#7.0 Pattern matching (type of)
// #define _01_CSharp70_PATTERN_MATCHING_TYPEOF
#endregion C#7.0 Pattern matching (type of)

#region C#7.0 Pattern matching (is)
// #define _02_CSharp70_PATTERN_MATCHING_IS
#endregion C#7.0 Pattern matching (is)

#region C#7.0 Recursive patterns
// #define _03a_CSharp70_RECURSIVE_PATTERNS
// #define _03b_CSharp70_RECURSIVE_PATTERNS
#endregion C#7.0 Recursive patterns

#region C#8.0 Recursive patterns
// #define _04a_CSharp80_RECURSIVE_PATTERNS
// #define _04b_CSharp80_RECURSIVE_PATTERNS
// #define _04c_CSharp80_RECURSIVE_PATTERNS
#define _04d_CSharp80_RECURSIVE_PATTERNS
#endregion C#8.0 Recursive patterns

#endregion Define

using _15.RecursivePatterns.CompositePattern;
using System;

namespace _15.RecursivePatterns
{
    /* Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 7.0 -DefineSection _01_CSharp70_PATTERN_MATCHING_TYPEOF
     *
     * Prints:
     * ♦ Books
     * ♦   [A-C]
     * ◊     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ◊     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Pattern matching (type of)

#if _01_CSharp70_PATTERN_MATCHING_TYPEOF

    internal class Program
    {

    #region C#7.0 Pattern matching impl (type of)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                if (this.GetType().Equals(typeof(Box)))
                {
                    PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                    GetInsideBox();
                }
                else if (this.GetType().Equals(typeof(Book)))
                {
                    Book book = (Book)this;
                    book.PrintIcon(IconEnum.LOZENGE);
                    book.PrintLabel();
                }
            }
        }

    #endregion C#7.0 Pattern matching impl (type of)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♦ Books
         * ♦   [A-C]
         * ◊     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ◊     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

    #endregion C#7.0 Pattern matching (type of)

    /* Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 7.3 -DefineSection _02_CSharp70_PATTERN_MATCHING_IS
     *
     * Prints:
     * ♦ Books
     * ♦   [A-C]
     * ◊     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ◊     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Pattern matching (is)

#if _02_CSharp70_PATTERN_MATCHING_IS

    internal class Program
    {

    #region C#7.0 Pattern matching impl (is)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                if (this is Box)
                {
                    PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                    GetInsideBox();
                }
                else if (this is Book book)
                {
                    book.PrintIcon(IconEnum.LOZENGE);
                    book.PrintLabel();
                }
            }
        }

    #endregion C#7.0 Pattern matching impl (is)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♦ Books
         * ♦   [A-C]
         * ◊     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ◊     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

    #endregion C#7.0 Pattern matching (type of)

    /* Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 7.0 -DefineSection _03a_CSharp70_RECURSIVE_PATTERNS
     *
     * Prints:
     * ♦ Books
     * ♥   [A-C]
     * ◊     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ◊     [CleanCode ] Clean Architecture
     * ...
     * 
     * Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 7.0 -DefineSection _03b_CSharp70_RECURSIVE_PATTERNS
     *
     * Prints:
     * ♦ Books
     * ♥   [A-C]
     * ◊     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ◊     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Recursive patterns

#if _03a_CSharp70_RECURSIVE_PATTERNS

    internal class Program
    {

    #region C#7.0 Recursive patterns impl

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                if (this is Box && ((Box)this).Label.StartsWith('['))
                {
                    ((Box)this).PrintIcon(IconEnum.BLACK_HEART_SUIT);
                    ((Box)this).GetInsideBox();
                }
                else if (this is Box)
                {
                    PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                    GetInsideBox();
                }
                else if (this is Book)
                {
                    PrintIcon(IconEnum.LOZENGE);
                    PrintLabel();
                }
            }
        }

    #endregion C#7.0 Recursive patterns impl

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♦ Books
         * ♥   [A-C]
         * ◊     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ◊     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

#if _03b_CSharp70_RECURSIVE_PATTERNS

    internal class Program
    {

    #region C#7.0 Recursive patterns impl

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                if (this is Box box && box.Label.StartsWith('['))
                {
                    box.PrintIcon(IconEnum.BLACK_HEART_SUIT);
                    box.GetInsideBox();
                }
                else if (this is Box)
                {
                    PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                    GetInsideBox();
                }
                else if (this is Book)
                {
                    PrintIcon(IconEnum.LOZENGE);
                    PrintLabel();
                }
            }
        }

    #endregion C#7.0 Recursive patterns impl

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♦ Books
         * ♥   [A-C]
         * ◊     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ◊     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

    #endregion C#7.0 Recursive patterns

    /* Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 8.0 -DefineSection _04a_CSharp80_RECURSIVE_PATTERNS
     *
     * Prints:
     * ♦ Books
     * ♥   [A-C]
     * ◊     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ◊     [CleanCode ] Clean Architecture
     * ...
     * 
     * Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 8.0 -DefineSection _04b_CSharp80_RECURSIVE_PATTERNS
     *
     * Prints:
     * ♥ Books
     * ◊   [A-C]
     * ◊   [D-F]
     * ◊   [G-I]
     * ◊   [J-L]
     * ...
     * 
     * Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 8.0 -DefineSection _04c_CSharp80_RECURSIVE_PATTERNS
     *
     * Prints:
     * ♥ Books
     * ◊   [A-C]
     * ◊   [D-F]
     * ◊   [G-I]
     * ◊   [J-L]
     * ...
     * 
     * Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 8.0 -DefineSection _04d_CSharp80_RECURSIVE_PATTERNS
     *
     * Prints:
     * ♥ Books
     * ◊   [A-C]
     * ◊   [D-F]
     * ◊   [G-I]
     * ◊   [J-L]
     * ...
     */

    #region C#8.0 Recursive patterns

#if _04a_CSharp80_RECURSIVE_PATTERNS

    internal class Program
    {

    #region C#8.0 Recursive patterns impl

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                if (this is Box { Label: string label } box
                && label.Length.Equals(5) && label.StartsWith('['))
                {
                    box.PrintIcon(IconEnum.BLACK_HEART_SUIT);
                    box.GetInsideBox();
                }
                else if (this is Box)
                {
                    PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                    GetInsideBox();
                }
                else if (this is Book)
                {
                    PrintIcon(IconEnum.LOZENGE);
                    PrintLabel();
                }
            }
        }

    #endregion C#8.0 Recursive patterns impl

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♦ Books
         * ♥   [A-C]
         * ◊     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ◊     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

#if _04b_CSharp80_RECURSIVE_PATTERNS

    internal class Program
    {

        #region C#8.0 Recursive patterns impl

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                if (this is Box { Label: { Length: 5 } label } box
                && label.StartsWith('B'))
                {
                    box.PrintIcon(IconEnum.BLACK_HEART_SUIT);
                    box.GetInsideBox();
                }
                else if (this is null)
                {
                    PrintIcon(IconEnum.X);
                    Console.WriteLine("I am the null.");
                }
                else if (this is { })
                {
                    PrintIcon(IconEnum.LOZENGE);
                    PrintLabel();
                }
            }
        }

        #endregion C#8.0 Recursive patterns impl

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♥ Books
         * ◊   [A-C]
         * ◊   [D-F]
         * ◊   [G-I]
         * ◊   [J-L]
         * ...
         *
         */
    }

#endif

#if _04c_CSharp80_RECURSIVE_PATTERNS

    internal class Program
    {

    #region C#8.0 Recursive patterns impl

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                if (this is Box { Label: string label } box
                && label.Length.Equals(5) && label.StartsWith('B'))
                {
                    box.PrintIcon(IconEnum.BLACK_HEART_SUIT);
                    box.GetInsideBox();
                }
                else if (this is null)
                {
                    PrintIcon(IconEnum.X);
                    Console.WriteLine("I am the null.");
                }
                else if (this is { })
                {
                    PrintIcon(IconEnum.LOZENGE);
                    PrintLabel();
                }
            }
        }

    #endregion C#8.0 Recursive patterns impl

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♥ Books
         * ◊   [A-C]
         * ◊   [D-F]
         * ◊   [G-I]
         * ◊   [J-L]
         * ...
         *
         */
    }

#endif

#if _04d_CSharp80_RECURSIVE_PATTERNS

    internal class Program
    {

    #region C#8.0 Recursive patterns impl

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                if (this is Box (var metadata, var _) { Label: "Books" } box
                && metadata.length.Equals(5)
                && metadata.startsWithB)
                {
                    box.PrintIcon(IconEnum.BLACK_HEART_SUIT);
                    box.GetInsideBox();
                }
                else if (this is null)
                {
                    PrintIcon(IconEnum.X);
                    Console.WriteLine("I am the null.");
                }
                else if (this is { })
                {
                    PrintIcon(IconEnum.LOZENGE);
                    PrintLabel();
                }
            }
        }

    #endregion C#8.0 Recursive patterns impl

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♥ Books
         * ◊   [A-C]
         * ◊   [D-F]
         * ◊   [G-I]
         * ◊   [J-L]
         * ...
         *
         */
    }

#endif

    #endregion C#8.0 Recursive patterns
}