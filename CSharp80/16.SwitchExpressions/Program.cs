#region Define

#region C#7.0 Pattern matching (if: type of)
// #define _01_CSharp70_PATTERN_MATCHING_IF_TYPEOF
#endregion C#7.0 Pattern matching (if: type of)

#region C#7.0 Pattern matching (if: is type)
// #define _02_CSharp70_PATTERN_MATCHING_IF_ISTYPE
#endregion C#7.0 Pattern matching (if: is type)

#region C#7.0 Pattern matching (switch)
// #define _03_CSharp70_PATTERN_MATCHING_SWITCH
#endregion C#7.0 Pattern matching (switch)

#region C#7.0 Pattern matching (if)
// #define _04_CSharp70_PATTERN_MATCHING_IF
#endregion C#7.0 Pattern matching (if)

#region C#7.0 Pattern matching (switch: case if)
// #define _05_CSharp70_PATTERN_MATCHING_SWITCH_CASEIF
#endregion C#7.0 Pattern matching (switch: case if)

#region C#7.0 Pattern matching (switch: case when)
// #define _06_CSharp70_PATTERN_MATCHING_SWITCH_CASEWHEN
#endregion C#7.0 Pattern matching (switch: case when)

#region C#7.0 Pattern matching (if: null & default)
// #define _07_CSharp70_PATTERN_MATCHING_IF_NULLDEFAULT
#endregion C#7.0 Pattern matching (if: null & default)

#region C#7.0 Pattern matching (switch: null & default)
// #define _08_CSharp70_PATTERN_MATCHING_SWITCH_NULLDEFAULT
#endregion C#7.0 Pattern matching (switch: null & default)

#region C#7.0 Pattern matching (if: object)
// #define _09_CSharp70_PATTERN_MATCHING_IF_OBJECT
#endregion C#7.0 Pattern matching (if: object)

#region C#7.0 Pattern matching (switch: var)
// #define _10a_CSharp70_PATTERN_MATCHING_SWITCH_VAR
// #define _10b_CSharp70_PATTERN_MATCHING_SWITCH_VAR
// #define _10c_CSharp70_PATTERN_MATCHING_SWITCH_VAR
#endregion C#7.0 Pattern matching (switch: var)

#region C#8.0 Switch expressions (if)
// #define _11_CSharp70_SWITCH_EXPRESSIONS_IF
#endregion C#8.0 Switch expressions (if)

#region C#8.0 Switch expressions (switch)
// #define _12a_CSharp70_SWITCH_STATEMENT
// #define _12b_CSharp80_SWITCH_EXPRESSION
// #define _12c_CSharp80_SWITCH_STATEMENT_PROPERTY_PATTERN
// #define _12d_CSharp80_SWITCH_EXPRESSION_PROPERTY_PATTERN
// #define _12e_CSharp80_SWITCH_STATEMENT_PROPERTY_REC_PATTERN
// #define _12f_CSharp80_SWITCH_EXPRESSION_PROPERTY_REC_PATTERN
// #define _12g_CSharp80_SWITCH_EXPRESSION_POSITIONAL_PATTERN
#endregion C#8.0 Switch expressions (switch)

#region C#8.0 Switch expressions (switch tuple)
// #define _13a_CSharp71_SWITCH_EXPRESSION_TUPLE_PATTERN
#define _13b_CSharp80_SWITCH_EXPRESSION_TUPLE_PATTERN
#endregion C#8.0 Switch expressions (switch tuple)

#endregion Define

using _16.SwitchExpressions.CompositePattern;
using System;

namespace _16.SwitchExpressions
{
    /* Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _01_CSharp70_PATTERN_MATCHING_IF_TYPEOF
     *
     * Prints:
     * ♦ Books
     * ♦   [A-C]
     * ◊     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ◊     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Pattern matching (if: type of)

#if _01_CSharp70_PATTERN_MATCHING_IF_TYPEOF

    internal class Program
    {

    #region C#7.0 Pattern matching impl (if: type of)

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

    /* Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _02_CSharp70_PATTERN_MATCHING_IF_ISTYPE
     *
     * Prints:
     * ♦ Books
     * ♦   [A-C]
     * ◊     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ◊     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Pattern matching (if: is type)

#if _02_CSharp70_PATTERN_MATCHING_IF_ISTYPE

    internal class Program
    {

    #region C#7.0 Pattern matching impl (if: is type)

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

    #endregion C#7.0 Pattern matching impl (if: is type)

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

    #endregion C#7.0 Pattern matching (if: is type)

    /* Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _03_CSharp70_PATTERN_MATCHING_SWITCH
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

    /* Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _04_CSharp70_PATTERN_MATCHING_IF
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

    /* Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _05_CSharp70_PATTERN_MATCHING_SWITCH_CASEIF
     *
     * The code will not even compile because of:
     * - CS8120 C# The switch case has already been handled by a previous case.
     */

    #region C#7.0 Pattern matching (switch: case if)

#if _05_CSharp70_PATTERN_MATCHING_SWITCH_CASEIF

    internal class Program
    {

    #region C#7.0 Pattern matching impl (switch: case if)

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

    #endregion C#7.0 Pattern matching impl (switch: case if)

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

    #endregion C#7.0 Pattern matching (switch: case if)

    /* Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _06_CSharp70_PATTERN_MATCHING_SWITCH_CASEWHEN
     *
     * Prints:
     * ○ Books
     * ♦   [A-C]
     * ☼     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ☼     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Pattern matching (switch: case when)

#if _06_CSharp70_PATTERN_MATCHING_SWITCH_CASEWHEN

    internal class Program
    {

    #region C#7.0 Pattern matching impl (switch: case when)

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

    #endregion C#7.0 Pattern matching impl (switch: case when)

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

    #endregion C#7.0 Pattern matching (switch: case when)

    /* Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _07_CSharp70_PATTERN_MATCHING_IF_NULLDEFAULT
     *
     * Prints:
     * ♪ Books
     * ♪   Nothing in the box
     * ♪   [A-C]
     * ♫     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ♫     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Pattern matching (if: null & default)

#if _07_CSharp70_PATTERN_MATCHING_IF_NULLDEFAULT

    internal class Program
    {

    #region C#7.0 Pattern matching impl (if: null & default)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                if (this is Box box)
                {
                    box.PrintIcon(IconEnum.EIGHTH_NOTE);
                    box.GetInsideBox();
                }
                else
                {
                    PrintIcon(IconEnum.BEAMED_EIGHTH_NOTES);
                    PrintLabel();
                }
            }

            protected override void GetInsideBox(IBox box)
            {
                if (box is null)
                {
                    PrintIcon(IconEnum.EIGHTH_NOTE);
                    PrintLabel("Nothing in the box");
                }
                else
                {
                    base.GetInsideBox(box);
                }
            }
        }

    #endregion C#7.0 Pattern matching impl (if: null & default)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♪ Books
         * ♪   Nothing in the box
         * ♪   [A-C]
         * ♫     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♫     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

    #endregion C#7.0 Pattern matching (if: null & default)

    /* Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _08_CSharp70_PATTERN_MATCHING_SWITCH_NULLDEFAULT
     *
     * Prints:
     * ♪ Books
     * ♪   Nothing in the box
     * ♪   [A-C]
     * ♫     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ♫     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Pattern matching (switch: null & default)

#if _08_CSharp70_PATTERN_MATCHING_SWITCH_NULLDEFAULT

    internal class Program
    {

    #region C#7.0 Pattern matching impl (switch: null & default)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                switch (this)
                {
                    case Box box:
                        box.PrintIcon(IconEnum.EIGHTH_NOTE);
                        box.GetInsideBox();
                        break;
                    default:
                        PrintIcon(IconEnum.BEAMED_EIGHTH_NOTES);
                        PrintLabel();
                        break;
                }
            }

            protected override void GetInsideBox(IBox box)
            {
                switch (box)
                {
                    case null:
                        PrintIcon(IconEnum.EIGHTH_NOTE);
                        PrintLabel("Nothing in the box");
                        break;
                    default:
                        base.GetInsideBox(box);
                        break;
                }
            }
        }

    #endregion C#7.0 Pattern matching impl (switch: null & default)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♪ Books
         * ♪   [A-C]
         * ♫     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♫     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

    #endregion C#7.0 Pattern matching (switch: null & default)

    /* Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _09_CSharp70_PATTERN_MATCHING_IF_OBJECT
     *
     * Prints:
     * ♪ Books
     * ♪   Nothing in the box
     * ♪   [A-C]
     * ♫     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ♫     [CleanCode ] Clean Architecture
     * ...
     * 
     */

    #region C#7.0 Pattern matching (if: object)

#if _09_CSharp70_PATTERN_MATCHING_IF_OBJECT

    internal class Program
    {

    #region C#7.0 Pattern matching impl (if: object)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                if (this is Box box)
                {
                    box.PrintIcon(IconEnum.EIGHTH_NOTE);
                    box.GetInsideBox();
                }
                else
                {
                    PrintIcon(IconEnum.BEAMED_EIGHTH_NOTES);
                    PrintLabel();
                }
            }

            protected override void GetInsideBox(IBox box)
            {
                if (box is null)
                {
                    PrintIcon(IconEnum.EIGHTH_NOTE);
                    PrintLabel("Nothing in the box");
                }
                else if (box is object)
                {
                    base.GetInsideBox(box);
                }
            }
        }

    #endregion C#7.0 Pattern matching impl (if: object)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♪ Books
         * ♪   Nothing in the box
         * ♪   [A-C]
         * ♫     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♫     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

    #endregion C#7.0 Pattern matching (if: null & default)

    /* Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _10a_CSharp70_PATTERN_MATCHING_SWITCH_VAR
     *
     * Prints:
     * ♪ Books
     * ♪   Nothing in the box
     * ♪   [A-C]
     * ♫     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ♫     [CleanCode ] Clean Architecture
     * ...
     * 
     * Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _10b_CSharp70_PATTERN_MATCHING_SWITCH_VAR
     *
     * The code will not even compile because of:
     * - CS8120 C# The switch case has already been handled by a previous case.
     * 
     * Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _10c_CSharp70_PATTERN_MATCHING_SWITCH_VAR
     *
     * Prints:
     * ♪ Books
     * ♪   Nothing in the box
     * ♪   [A-C]
     * ♫     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ♫     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#7.0 Pattern matching (switch: var)

#if _10a_CSharp70_PATTERN_MATCHING_SWITCH_VAR

    internal class Program
    {

    #region C#7.0 Pattern matching impl (switch: var)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                switch (this)
                {
                    case Box box:
                        box.PrintIcon(IconEnum.EIGHTH_NOTE);
                        box.GetInsideBox();
                        break;
                    default:
                        PrintIcon(IconEnum.BEAMED_EIGHTH_NOTES);
                        PrintLabel();
                        break;
                }
            }

            protected override void GetInsideBox(IBox box)
            {
                switch (box)
                {
                    case null:
                        PrintIcon(IconEnum.EIGHTH_NOTE);
                        PrintLabel("Nothing in the box");
                        break;
                    case var vBox:
                        base.GetInsideBox(vBox);
                        break;
                }
            }
        }

    #endregion C#7.0 Pattern matching impl (switch: var)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♪ Books
         * ♪   Nothing in the box
         * ♪   [A-C]
         * ♫     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♫     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

#if _10b_CSharp70_PATTERN_MATCHING_SWITCH_VAR

    internal class Program
    {

    #region C#7.0 Pattern matching impl (switch: var)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                switch (this)
                {
                    case Box box:
                        box.PrintIcon(IconEnum.EIGHTH_NOTE);
                        box.GetInsideBox();
                        break;
                    default:
                        PrintIcon(IconEnum.BEAMED_EIGHTH_NOTES);
                        PrintLabel();
                        break;
                }
            }

            protected override void GetInsideBox(IBox box)
            {
                switch (box)
                {
                    case var vBox:
                        PrintIcon(IconEnum.EIGHTH_NOTE);
                        PrintLabel("Nothing in the box");
                        break;
                    case var vBox when vBox != null:
                        base.GetInsideBox(vBox);
                        break;
                }
            }
        }

    #endregion C#7.0 Pattern matching impl (switch: var)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♪ Books
         * ♪   Nothing in the box
         * ♪   [A-C]
         * ♫     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♫     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

#if _10c_CSharp70_PATTERN_MATCHING_SWITCH_VAR

    internal class Program
    {

    #region C#7.0 Pattern matching impl (switch: var)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                switch (this)
                {
                    case Box box:
                        box.PrintIcon(IconEnum.EIGHTH_NOTE);
                        box.GetInsideBox();
                        break;
                    default:
                        PrintIcon(IconEnum.BEAMED_EIGHTH_NOTES);
                        PrintLabel();
                        break;
                }
            }

            protected override void GetInsideBox(IBox box)
            {
                switch (box)
                {
                    case var vBox when vBox is null:
                        PrintIcon(IconEnum.EIGHTH_NOTE);
                        PrintLabel("Nothing in the box");
                        break;
                    case var vBox when vBox != null:
                        base.GetInsideBox(vBox);
                        break;
                }
            }
        }

    #endregion C#7.0 Pattern matching impl (switch: var)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♪ Books
         * ♪   Nothing in the box
         * ♪   [A-C]
         * ♫     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♫     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

    #endregion C#7.0 Pattern matching (switch: var)

    /* Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _11_CSharp70_SWITCH_EXPRESSIONS_IF
     *
     * Prints:
     * ♣ Books
     * ♦   [A-C]
     * ♥     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ♥     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#8.0 Switch expressions (if)

#if _11_CSharp70_SWITCH_EXPRESSIONS_IF

    internal class Program
    {

    #region C#8.0 Switch expressions impl (if)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                if (this is Box boxD
                    && boxD.StartingLetter > 'D')
                {
                    PrintIcon(IconEnum.BLACK_CLUB_SUIT);
                    boxD.GetInsideBox();
                }
                else if (this is Box boxA
                    && boxA.StartingLetter >= 'A')
                {
                    PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                    boxA.GetInsideBox();
                }
                else if (this is Book book)
                {
                    book.PrintIcon(IconEnum.BLACK_HEART_SUIT);
                    book.PrintLabel();
                }
                else
                {
                    PrintIcon(IconEnum.BLACK_SPADE_SUIT);
                    PrintLabel();
                }
            }
        }

    #endregion C#8.0 Switch expressions impl (if)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♣ Books
         * ♦   [A-C]
         * ♥     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♥     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

    #endregion C#8.0 Switch expressions (if)

    /* Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.0 -DefineSection _12a_CSharp70_SWITCH_STATEMENT
     *
     * Prints:
     * ♣ Books
     * ♦   [A-C]
     * ♥     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ♥     [CleanCode ] Clean Architecture
     * ...
     * 
     * Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 8.0 -DefineSection _12b_CSharp80_SWITCH_EXPRESSION
     *
     * Prints:
     * ♣ Books
     * ♦   [A-C]
     * ♥     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ♥     [CleanCode ] Clean Architecture
     * ...
     * 
     * Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 8.0 -DefineSection _12c_CSharp80_SWITCH_STATEMENT_PROPERTY_PATTERN
     *
     * Prints:
     * ♣ Books
     * ♦   [A-C]
     * ♥     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ♥     [CleanCode ] Clean Architecture
     * ...
     * 
     * Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 8.0 -DefineSection _12d_CSharp80_SWITCH_EXPRESSION_PROPERTY_PATTERN
     *
     * Prints:
     * ♣ Books
     * ♦   [A-C]
     * ♥     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ♥     [CleanCode ] Clean Architecture
     * ...
     * 
     * Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 8.0 -DefineSection _12e_CSharp80_SWITCH_STATEMENT_PROPERTY_REC_PATTERN
     *
     * Prints:
     * ♣ Books
     * ♦   [A-C]
     * ♥     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ♥     [CleanCode ] Clean Architecture
     * ...
     * 
     * Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 8.0 -DefineSection _12f_CSharp80_SWITCH_EXPRESSION_PROPERTY_REC_PATTERN
     *
     * Prints:
     * ♣ Books
     * ♦   [A-C]
     * ♥     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ♥     [CleanCode ] Clean Architecture
     * ...
     * 
     * Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 8.0 -DefineSection _12g_CSharp80_SWITCH_EXPRESSION_POSITIONAL_PATTERN
     *
     * Prints:
     * ♣ Books
     * ♦   [A-C]
     * ♥     [CleanCode ] Clean Code
     *                    A Handbook of Agile Software Craftsmanship
     * ♥     [CleanCode ] Clean Architecture
     * ...
     */

    #region C#8.0 Switch expressions (switch)

#if _12a_CSharp70_SWITCH_STATEMENT

    internal class Program
    {

    #region C#8.0 Switch expressions impl (switch)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                switch (this)
                {
                    case Box boxD when boxD.StartingLetter > 'D':
                        PrintIcon(IconEnum.BLACK_CLUB_SUIT);
                        boxD.GetInsideBox();
                        break;
                    case Box boxA when boxA.StartingLetter >= 'A':
                        PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                        boxA.GetInsideBox();
                        break;
                    case Book book:
                        book.PrintIcon(IconEnum.BLACK_HEART_SUIT);
                        book.PrintLabel();
                        break;
                    default:
                        PrintIcon(IconEnum.BLACK_SPADE_SUIT);
                        PrintLabel();
                        break;
                }
            }
        }

    #endregion C#8.0 Switch expressions impl (switch)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♣ Books
         * ♦   [A-C]
         * ♥     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♥     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

#if _12b_CSharp80_SWITCH_EXPRESSION

    internal class Program
    {

    #region C#8.0 Switch expressions impl (switch)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                int getInsideBoxD(Box boxD)
                {
                    PrintIcon(IconEnum.BLACK_CLUB_SUIT);
                    boxD.GetInsideBox();
                    return 0;
                }
                int getInsideBoxA(Box boxA)
                {
                    PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                    boxA.GetInsideBox();
                    return 0;
                }
                int readBook(Book book)
                {
                    book.PrintIcon(IconEnum.BLACK_HEART_SUIT);
                    book.PrintLabel();
                    return 0;
                }
                int doDefault()
                {
                    PrintIcon(IconEnum.BLACK_SPADE_SUIT);
                    PrintLabel();
                    return 0;
                }

                _ = this switch
                {
                    Box boxD when boxD.StartingLetter > 'D'
                        => getInsideBoxD(boxD),
                    Box boxA when boxA.StartingLetter >= 'A'
                        => getInsideBoxA(boxA),
                    Book book =>
                        readBook(book),
                    _ =>
                        doDefault()
                };
            }
        }

    #endregion C#8.0 Switch expressions impl (switch)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♣ Books
         * ♦   [A-C]
         * ♥     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♥     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

#if _12c_CSharp80_SWITCH_STATEMENT_PROPERTY_PATTERN

    internal class Program
    {

    #region C#8.0 Switch expressions impl (switch)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                switch (this)
                {
                    case Box
                    { StartingLetter: char letter }
                    boxD
                    when letter > 'D':
                        PrintIcon(IconEnum.BLACK_CLUB_SUIT);
                        boxD.GetInsideBox();
                        break;
                    case Box
                    { StartingLetter: char letter }
                    boxA
                    when letter >= 'A':
                        PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                        boxA.GetInsideBox();
                        break;
                    case Book book:
                        book.PrintIcon(IconEnum.BLACK_HEART_SUIT);
                        book.PrintLabel();
                        break;
                    default:
                        PrintIcon(IconEnum.BLACK_SPADE_SUIT);
                        PrintLabel();
                        break;
                }
            }
        }

    #endregion C#8.0 Switch expressions impl (switch)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♣ Books
         * ♦   [A-C]
         * ♥     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♥     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

#if _12d_CSharp80_SWITCH_EXPRESSION_PROPERTY_PATTERN

    internal class Program
    {

    #region C#8.0 Switch expressions impl (switch)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                int getInsideBoxD(Box boxD)
                {
                    PrintIcon(IconEnum.BLACK_CLUB_SUIT);
                    boxD.GetInsideBox();
                    return 0;
                }
                int getInsideBoxA(Box boxA)
                {
                    PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                    boxA.GetInsideBox();
                    return 0;
                }
                int readBook(Book book)
                {
                    book.PrintIcon(IconEnum.BLACK_HEART_SUIT);
                    book.PrintLabel();
                    return 0;
                }
                int doDefault()
                {
                    PrintIcon(IconEnum.BLACK_SPADE_SUIT);
                    PrintLabel();
                    return 0;
                }

                _ = this switch
                {
                    Box
                    { StartingLetter: char letter }
                    boxD when letter > 'D'
                        => getInsideBoxD(boxD),
                    Box
                    { StartingLetter: char letter }
                    boxA when letter >= 'A'
                        => getInsideBoxA(boxA),
                    Book book =>
                        readBook(book),
                    _ =>
                        doDefault()
                };
            }
        }

    #endregion C#8.0 Switch expressions impl (switch)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♣ Books
         * ♦   [A-C]
         * ♥     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♥     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

#if _12e_CSharp80_SWITCH_STATEMENT_PROPERTY_REC_PATTERN

    internal class Program
    {

    #region C#8.0 Switch expressions impl (switch)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                switch (this)
                {
                    case Box
                    { StartingLetter: char letter, Label: { Length: int length } }
                    boxD
                    when letter > 'D' && length >= 5:
                        PrintIcon(IconEnum.BLACK_CLUB_SUIT);
                        boxD.GetInsideBox();
                        break;
                    case Box
                    { StartingLetter: char letter, Label: { Length: int length } }
                    boxA
                    when letter >= 'A' && length >= 5:
                        PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                        boxA.GetInsideBox();
                        break;
                    case Book book:
                        book.PrintIcon(IconEnum.BLACK_HEART_SUIT);
                        book.PrintLabel();
                        break;
                    default:
                        PrintIcon(IconEnum.BLACK_SPADE_SUIT);
                        PrintLabel();
                        break;
                }
            }
        }

    #endregion C#8.0 Switch expressions impl (switch)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♣ Books
         * ♦   [A-C]
         * ♥     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♥     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

#if _12f_CSharp80_SWITCH_EXPRESSION_PROPERTY_REC_PATTERN

    internal class Program
    {

    #region C#8.0 Switch expressions impl (switch)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                int getInsideBoxD(Box boxD)
                {
                    PrintIcon(IconEnum.BLACK_CLUB_SUIT);
                    boxD.GetInsideBox();
                    return 0;
                }
                int getInsideBoxA(Box boxA)
                {
                    PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                    boxA.GetInsideBox();
                    return 0;
                }
                int readBook(Book book)
                {
                    book.PrintIcon(IconEnum.BLACK_HEART_SUIT);
                    book.PrintLabel();
                    return 0;
                }
                int doDefault()
                {
                    PrintIcon(IconEnum.BLACK_SPADE_SUIT);
                    PrintLabel();
                    return 0;
                }

                _ = this switch
                {
                    Box
                    { StartingLetter: char letter, Label: { Length: int length } }
                    boxD
                     when letter > 'D' && length >= 5
                        => getInsideBoxD(boxD),
                    Box
                    { StartingLetter: char letter, Label: { Length: int length } }
                    boxA
                    when letter >= 'A' && length >= 5
                        => getInsideBoxA(boxA),
                    Book book =>
                        readBook(book),
                    _ =>
                        doDefault()
                };
            }
        }

    #endregion C#8.0 Switch expressions impl (switch)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♣ Books
         * ♦   [A-C]
         * ♥     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♥     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

#if _12g_CSharp80_SWITCH_EXPRESSION_POSITIONAL_PATTERN

    internal class Program
    {

    #region C#8.0 Switch expressions impl (switch)

        internal abstract class BookBox : ABox
        {
            public override void GetInside()
            {
                int getInsideBoxD(Box boxD)
                {
                    PrintIcon(IconEnum.BLACK_CLUB_SUIT);
                    boxD.GetInsideBox();
                    return 0;
                }
                int getInsideBoxA(Box boxA)
                {
                    PrintIcon(IconEnum.BLACK_DIAMOND_SUIT);
                    boxA.GetInsideBox();
                    return 0;
                }
                int readBook(Book book)
                {
                    book.PrintIcon(IconEnum.BLACK_HEART_SUIT);
                    book.PrintLabel();
                    return 0;
                }
                int doDefault()
                {
                    PrintIcon(IconEnum.BLACK_SPADE_SUIT);
                    PrintLabel();
                    return 0;
                }

                _ = this switch
                {
                    Box(var metadata, _) boxD when metadata.letterGtD && metadata.lengthGt5
                        => getInsideBoxD(boxD),
                    Box(var metadata, _) boxA when metadata.letterGeA && metadata.lengthGt5
                        => getInsideBoxA(boxA),
                    Book book =>
                        readBook(book),
                    _ =>
                        doDefault()
                };
            }
        }

    #endregion C#8.0 Switch expressions impl (switch)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            BoxRepository.GetBox().GetInside();
        }

        /* Expected output:
         * 
         * ♣ Books
         * ♦   [A-C]
         * ♥     [CleanCode ] Clean Code
         *                    A Handbook of Agile Software Craftsmanship
         * ♥     [CleanCode ] Clean Architecture
         * ...
         *
         */
    }

#endif

    #endregion C#8.0 Switch expressions (switch)

    /* Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 7.1 -DefineSection _13a_CSharp71_SWITCH_EXPRESSION_TUPLE_PATTERN
     *
     * Prints:
     * (^-^)
     * (°ʖ°)
     * ʕ•ᴥ•ʔ
     * (╯°□°)╯  ┻━┻
     * 
     * Execute-Example -ProjectName 16.SwitchExpressions -LangVersion 8.0 -DefineSection _13b_CSharp80_SWITCH_EXPRESSION_TUPLE_PATTERN
     *
     * Prints:
     * (^-^)
     * (°ʖ°)
     * ʕ•ᴥ•ʔ
     * (╯°□°)╯  ┻━┻
     */

    #region C#8.0 Switch expressions (switch tuple)

#if _13a_CSharp71_SWITCH_EXPRESSION_TUPLE_PATTERN

    #region C#8.0 Switch expressions impl (switch tuple)

    public static class IconEnumExtension
    {
        public static string ToString(this EmotconEnum icon, params EmotconEnum[] icons)
        {
            switch ((icon, eye: icons[0], nose: icons[1]))
            {
                case var tuple when tuple.icon == EmotconEnum.HAND_1:
                    return $"({(char)tuple.eye}{(char)tuple.nose}{(char)tuple.eye})";
                case var tuple when tuple.icon == EmotconEnum.HAND_2:
                    return $"({(char)tuple.eye}{(char)tuple.nose}{(char)tuple.eye})";
                case var tuple when tuple.icon == EmotconEnum.HAND_3:
                    return $"({(char)tuple.eye}{(char)tuple.nose}{(char)tuple.eye})";
                default:
                    return @"(╯°□°)╯  ┻━┻";
            }
        }
    }

    #endregion C#8.0 Switch expressions impl (switch tuple)

    internal class Program
    {

    #region C#8.0 Switch expressions impl (switch)

        internal abstract class BookBox : ABox { }

    #endregion C#8.0 Switch expressions impl (switch)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine(EmotconEnum.HAND_1.ToString(EmotconEnum.EYE_1, EmotconEnum.NOSE_1));
            Console.WriteLine(EmotconEnum.HAND_2.ToString(EmotconEnum.EYE_2, EmotconEnum.NOSE_2));
            Console.WriteLine(EmotconEnum.HAND_3.ToString(EmotconEnum.EYE_3, EmotconEnum.NOSE_3));
            Console.WriteLine(EmotconEnum.HAND_4.ToString(EmotconEnum.EYE_4, EmotconEnum.NOSE_4));
        }

        /* Expected output:
         * 
         * (^-^)
         * (°ʖ°)
         * ʕ•ᴥ•ʔ
         * (╯°□°)╯  ┻━┻
         *
         */
    }

#endif

#if _13b_CSharp80_SWITCH_EXPRESSION_TUPLE_PATTERN

    #region C#8.0 Switch expressions impl (switch tuple)

    public static class IconEnumExtension
    {
        public static string ToString(this EmotconEnum icon, params EmotconEnum[] icons) =>
            Tuple.Create(icon, icons[0], icons[1]) switch
        {
            (EmotconEnum.HAND_1, EmotconEnum eye, EmotconEnum nose) =>
                $"({(char)eye}{(char)nose}{(char)eye})",
            (EmotconEnum.HAND_2, EmotconEnum eye, EmotconEnum nose) =>
                $"({(char)eye}{(char)nose}{(char)eye})",
            (EmotconEnum.HAND_3, EmotconEnum eye, EmotconEnum nose) =>
                $"ʕ{(char)eye}{(char)nose}{(char)eye}ʔ",
            _ => @"(╯°□°)╯  ┻━┻"
        };
    }

    #endregion C#8.0 Switch expressions impl (switch tuple)

    internal class Program
    {

        #region C#8.0 Switch expressions impl (switch)

        internal abstract class BookBox : ABox { }

        #endregion C#8.0 Switch expressions impl (switch)

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine(EmotconEnum.HAND_1.ToString(EmotconEnum.EYE_1, EmotconEnum.NOSE_1));
            Console.WriteLine(EmotconEnum.HAND_2.ToString(EmotconEnum.EYE_2, EmotconEnum.NOSE_2));
            Console.WriteLine(EmotconEnum.HAND_3.ToString(EmotconEnum.EYE_3, EmotconEnum.NOSE_3));
            Console.WriteLine(EmotconEnum.HAND_4.ToString(EmotconEnum.EYE_4, EmotconEnum.NOSE_4));
        }

        /* Expected output:
         * 
         * (^-^)
         * (°ʖ°)
         * ʕ•ᴥ•ʔ
         * (╯°□°)╯  ┻━┻
         *
         */
    }

#endif

    #endregion C#8.0 Switch expressions (switch tuple)
}