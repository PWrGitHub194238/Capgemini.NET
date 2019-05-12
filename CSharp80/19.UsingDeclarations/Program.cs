#region Define

#region C#7.0 Example
#define _01_CSharp70_EXAMPLE
#endregion C#7.0 Example

#region C#7.1 Example
//#define _02_CSharp71_EXAMPLE
//#define _02_CSharp71_EXAMPLE_Question
#endregion C#7.1 Example

#endregion Define

using System;

namespace _19.UsingDeclarations
{
    /* Execute-Example -ProjectName 00.Example -LangVersion 7.0 -DefineSection _01_CSharp70_EXAMPLE
     *
     * Prints 'Hello Word from C# 7.0!'.
     */

    #region C#7.0 Example

#if _01_CSharp70_EXAMPLE

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello Word from C# 7.0!");
        }

        /* Expected output:
         *
         * Hello Word from C# 7.0!
         *
         */
    }

#endif

    #endregion C#7.0 Example

    /* Execute-Example -ProjectName 00.Example -LangVersion 7.1 -DefineSection _02_CSharp71_EXAMPLE
     *
     * Prints 'Hello Word from C# 7.1!'.
     */

    #region C#7.1 Example

#if _02_CSharp71_EXAMPLE

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello Word from C# 7.1!");
        }

        /* Expected output:
         *
         * Hello Word from C# 7.1!
         *
         */
    }

#endif

    #endregion C#7.0 Example
}