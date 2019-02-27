#region Define

#region C#7.0 Pattern matching (if type of)
//#define _01_CSharp70_PATTERN_MATCHING_IFTYPEOF
#endregion C#7.0 Pattern matching (if type of)

#region C#7.0 Pattern matching (if is type)
//#define _02_CSharp70_PATTERN_MATCHING_IFISTYPE
#endregion C#7.0 Pattern matching (if is type)

#region C#7.0 Pattern matching (switch)
//#define _03_CSharp70_PATTERN_MATCHING_SWITCH
#endregion C#7.0 Pattern matching (switch)

#region C#7.0 Pattern matching (if)
//#define _04_CSharp70_PATTERN_MATCHING_IF
#endregion C#7.0 Pattern matching (if)

#region C#7.0 Pattern matching (case if)
//#define _05_CSharp70_PATTERN_MATCHING_CASEIF
#endregion C#7.0 Pattern matching (case if)

#region C#7.0 Pattern matching (case when)
#define _06_CSharp70_PATTERN_MATCHING_CASEWHEN
#endregion C#7.0 Pattern matching (case when)

#endregion Define

using _16.SwitchExpressions.CompositePattern;
using System;

namespace _16.SwitchExpressions
{
    /* Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 7.0 -DefineSection _01_CSharp70_PATTERN_MATCHING_IFTYPEOF
     *
     * Prints:
     * ♦ Books
     * ♦   [A-C]
     * ◊     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ◊     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Pattern matching (if type of)

#if _01_CSharp70_PATTERN_MATCHING_IFTYPEOF

    internal class Program
    {

    #region C#7.0 Pattern matching impl (if type of)

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

    #endregion C#7.0 Pattern matching (is type of)

    /* Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 7.0 -DefineSection _02_CSharp70_PATTERN_MATCHING_IFISTYPE
     *
     * Prints:
     * ♦ Books
     * ♦   [A-C]
     * ◊     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ◊     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Pattern matching (if is type)

#if _02_CSharp70_PATTERN_MATCHING_IFISTYPE

    internal class Program
    {

    #region C#7.0 Pattern matching impl (if is type)

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

    #endregion C#7.0 Pattern matching impl (if is type)

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

    #endregion C#7.0 Pattern matching (if is type)

    /* Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 7.0 -DefineSection _03_CSharp70_PATTERN_MATCHING_SWITCH
     *
     * Prints:
     * ♦ Books
     * ♦   [A-C]
     * ◊     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ◊     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Pattern matching (switch)

#if _03_CSharp70_PATTERN_MATCHING_SWITCH

    internal class Program
    {

    #region C#7.0 Pattern matching impl (switch)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                switch (this)
                {
                    case Box _:
                        PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                        GetInsideBox();
                        break;
                    case Book book:
                        book.PrintIcon(IconEnum.LOZENGE);
                        book.PrintLabel();
                        break;
                }
            }
        }

    #endregion C#7.0 Pattern matching impl (switch)

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

    #endregion C#7.0 Pattern matching (switch)

    /* Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 7.0 -DefineSection _04_CSharp70_PATTERN_MATCHING_IF
     *
     * Prints:
     * ○ Books
     * ♦   [A-C]
     * ☼     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ☼     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Pattern matching (if)

#if _04_CSharp70_PATTERN_MATCHING_IF

    internal class Program
    {

    #region C#7.0 Pattern matching impl (if)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                if (this is Box && ((Box)this).Label.StartsWith('['))
                {
                    PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                    GetInsideBox();
                }
                else if (this is Box)
                {
                    PrintIcon(IconEnum.WHITE_CIRCLE);
                    GetInsideBox();
                }
                else if (this is Book)
                {
                    Book book = (Book)this;
                    book.PrintIcon(IconEnum.WHITE_SUN_WITH_RAYS);
                    book.PrintLabel();
                }
            }
        }

    #endregion C#7.0 Pattern matching impl (if)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ○ Books
         * ♦   [A-C]
         * ☼     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ☼     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

    #endregion C#7.0 Pattern matching (if)

    /* Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 7.0 -DefineSection _05_CSharp70_PATTERN_MATCHING_CASEIF
     *
     * The code will not even compile because of:
     * CS8120 C# The switch case has already been handled by a previous case.
     */

    #region C#7.0 Pattern matching (case if)

#if _05_CSharp70_PATTERN_MATCHING_CASEIF

    internal class Program
    {

    #region C#7.0 Pattern matching impl (case if)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                switch (this)
                {
                    case Box box:
                        if (box.Label.StartsWith('['))
                        {
                            PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                            GetInsideBox();
                        }
                        break;
                    case Box _:
                        PrintIcon(IconEnum.WHITE_CIRCLE);
                        GetInsideBox();
                        break;
                    case Book book:
                        book.PrintIcon(IconEnum.WHITE_SUN_WITH_RAYS);
                        book.PrintLabel();
                        break;
                }
            }
        }

    #endregion C#7.0 Pattern matching impl (case if)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         *
         */
    }

#endif

    #endregion C#7.0 Pattern matching (case if)

    /* Execute-Example -ProjectName 15.RecursivePatterns -LangVersion 7.0 -DefineSection _06_CSharp70_PATTERN_MATCHING_CASEWHEN
     *
     * Prints:
     * ○ Books
     * ♦   [A-C]
     * ☼     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ☼     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Pattern matching (case when)

#if _06_CSharp70_PATTERN_MATCHING_CASEWHEN

    internal class Program
    {

        #region C#7.0 Pattern matching impl (case when)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                switch (this)
                {
                    case Box box when box.Label.StartsWith('['):
                        PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                        GetInsideBox();
                        break;
                    case Box _:
                        PrintIcon(IconEnum.WHITE_CIRCLE);
                        GetInsideBox();
                        break;
                    case Book book:
                        book.PrintIcon(IconEnum.WHITE_SUN_WITH_RAYS);
                        book.PrintLabel();
                        break;
                }
            }
        }

        #endregion C#7.0 Pattern matching impl (case when)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ○ Books
         * ♦   [A-C]
         * ☼     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ☼     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

    #endregion C#7.0 Pattern matching (case when)
}