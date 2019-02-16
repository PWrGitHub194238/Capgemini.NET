#region Define

#region C#7.3 on null exception
// #define _01_CSharp73_ON_NULL_EXCEPTION
#endregion C#7.3 on null exception

#region C#8.0 warning on null (#nullable disabled)
// #define _02_CSharp80_DISABLE_NULLABLE
#endregion C#8.0 warning on null (#nullable disabled)

#region C#8.0 warning on null (#nullable enabled)
// #define _03_CSharp80_ENABLE_NULLABLE
#endregion C#8.0 warning on null (#nullable enabled)

#region C#8.0 warning on null (#nullable safeonly)
// #define _04_CSharp80_ENABLE_SAFEONLY
#endregion C#8.0 warning on null (#nullable safeonly)

#region C#8.0 warning on null (#nullable enabled restore)
// #define _05_CSharp80_RESTORE
#endregion C#8.0 warning on null (#nullable enabled restore)

#region C#8.0 != null (#nullable enabled)
// #define _06_CSharp80_CHECK_NULL
#endregion C#8.0 != null (#nullable enabled)

#region C#8.0 IsNull(value) (#nullable enabled)
#define _07_CSharp80_CHECK_IS_NULL
#endregion C#8.0 IsNull(value) (#nullable enabled)

#endregion Define

using _11.NullableReferenceTpes.Utils;
using System;

namespace _11.NullableReferenceTpes
{
    /* Execute-Example -ProjectName 11.NullableReferenceTpes -LangVersion 7.3 -DefineSection _01_CSharp73_ON_NULL_EXCEPTIONE
     *
     * Throws an exception because we assigned some nulls.
     * No wonning has been risen.
     */

    #region C#7.3 on null exception

#if _01_CSharp73_ON_NULL_EXCEPTION

    internal class Program
    {
        private static void Main()
        {
            string s = null;
            try
            {
                ConsolColours.PrintFancyText(lines: new string[] {
                    s
                });
            }
            catch (Exception ex) when (false
                || ex is NullReferenceException
                || ex is ArgumentNullException
            )
            {
                Console.WriteLine("I have thrown the exception, because You left a 'null' " +
                    "value assigned to some reference type, you moron!");
            }
        }

        /* Expected output:
         *
         * I have thrown the exception, because You left a 'null' value assigned to some reference type, you moron!
         *
         */
    }

#endif

    #endregion C#7.3 on null exception

    /* Execute-Example -ProjectName 11.NullableReferenceTpes -LangVersion 8.0 -DefineSection _02_CSharp80_DISABLE_NULLABLE
     *
     * Throws an exception because we assigned some nulls.
     * No wonning has been risen.
     */

    #region C#8.0 warning on null (#nullable disabled)

#if _02_CSharp80_DISABLE_NULLABLE
#nullable disable

    internal class Program
    {
        private static void Main()
        {

            string s = null;
            try
            {
                ConsolColours.PrintFancyText(lines: new string[] {
                    s
                });
            }
            catch (Exception ex) when (false
                || ex is NullReferenceException
                || ex is ArgumentNullException
            )
            {
                Console.WriteLine("I have thrown the exception, because You left a 'null' " +
                    "value assigned to some reference type, you moron!");
            }
        }

        /* Expected output:
         *
         * I have thrown the exception, because You left a 'null' value assigned to some reference type, you moron!
         *
         */
    }

#nullable restore
#endif

    #endregion C#8.0 warning on null (#nullable disabled)

    /* Execute-Example -ProjectName 11.NullableReferenceTpes -LangVersion 8.0 -DefineSection _03_CSharp80_ENABLE_NULLABLE
     *
     * Throws an exception because we assigned some nulls.
     * Two warnings will be risen:
     * - CS8601 C# Possible null reference assignment.
     * - CS8600 C# Converting null literal or possible null value to non-nullable type.
     */

    #region C#8.0 warning on null (#nullable enabled)

#if _03_CSharp80_ENABLE_NULLABLE
#nullable enable

    internal class Program
    {
        private static void Main()
        {

            string s = null;
            try
            {
                ConsolColours.PrintFancyText(lines: new string[] {
                    s
                });
            }
            catch (Exception ex) when (false
                || ex is NullReferenceException
                || ex is ArgumentNullException
            )
            {
                Console.WriteLine("I have thrown the exception, because You left a 'null' " +
                    "value assigned to some reference type, you moron!");
            }
        }

        /* Expected output:
         *
         * I have thrown the exception, because You left a 'null' value assigned to some reference type, you moron!
         *
         */
    }

#nullable restore
#endif

    #endregion C#8.0 warning on null (#nullable enabled)

    /* Execute-Example -ProjectName 11.NullableReferenceTpes -LangVersion 8.0 -DefineSection _04_CSharp80_ENABLE_SAFEONLY
     *
     * Throws an exception because we assigned some nulls.
     * One warning will be risen:
     * - CS8601 C# Possible null reference assignment.
     */

    #region C#8.0 warning on null (#nullable safeonly)

#if _04_CSharp80_ENABLE_SAFEONLY
#nullable safeonly

    internal class Program
    {
        private static void Main()
        {

            string s = null;
            try
            {
                ConsolColours.PrintFancyText(lines: new string[] {
                    s
                });
            }
            catch (Exception ex) when (false
                || ex is NullReferenceException
                || ex is ArgumentNullException
            )
            {
                Console.WriteLine("I have thrown the exception, because You left a 'null' " +
                    "value assigned to some reference type, you moron!");
            }
        }

        /* Expected output:
         *
         * I have thrown the exception, because You left a 'null' value assigned to some reference type, you moron!
         *
         */
    }

#nullable restore
#endif

    #endregion C#8.0 warning on null (#nullable safeonly)

    /* Execute-Example -ProjectName 11.NullableReferenceTpes -LangVersion 8.0 -DefineSection _05_CSharp80_RESTORE
     *
     * Throws an exception because we assigned some nulls.
     * One warning will be risen:
     * - CS8600 C# Converting null literal or possible null value to non-nullable type.
     */

    #region C#8.0 warning on null (#nullable enabled restore)

#if _05_CSharp80_RESTORE
#nullable enable

    internal class Program
    {
        private static void Main()
        {

            string s = null;
            try
            {
#nullable restore
                ConsolColours.PrintFancyText(lines: new string[] {
                    s
                });
            }
            catch (Exception ex) when (false
                || ex is NullReferenceException
                || ex is ArgumentNullException
            )
            {
                Console.WriteLine("I have thrown the exception, because You left a 'null' " +
                    "value assigned to some reference type, you moron!");
            }
        }

        /* Expected output:
         *
         * I have thrown the exception, because You left a 'null' value assigned to some reference type, you moron!
         *
         */
    }

#nullable restore
#endif

    #endregion C#8.0 warning on null (#nullable enabled restore)

    /* Execute-Example -ProjectName 11.NullableReferenceTpes -LangVersion 8.0 -DefineSection _06_CSharp80_CHECK_NULL
     *
     * Compiles with a proper console output.
     */

    #region C#8.0 != null (#nullable enabled)

#if _06_CSharp80_CHECK_NULL
#nullable enable

    internal class Program
    {
        private static void Main()
        {

            string? s = null;
            if (s != null)
            {
                ConsolColours.PrintFancyText(lines: new string[] {
                    s
                });
            }
            else
            {
                Console.WriteLine("I have exit, because You left a 'null' " +
                    "value assigned to some reference type, you moron!");
            }
        }

        /* Expected output:
         *
         * I have exit, because You left a 'null' value assigned to some reference type, you moron!
         *
         */
    }

#nullable restore
#endif

    #endregion C#8.0 != null (#nullable enabled)

    /* Execute-Example -ProjectName 11.NullableReferenceTpes -LangVersion 8.0 -DefineSection _07_CSharp80_CHECK_IS_NULL
     *
     * Compiles with a proper console output.
     * One warning will be risen:
     * - CS8601 C# Possible null reference assignment.
     */

    #region C#8.0 IsNull(value) (#nullable enabled)

#if _07_CSharp80_CHECK_IS_NULL
#nullable enable

    internal class Program
    {
        private static void Main()
        {

            bool IsNull(string? s) => s == null;

            string? s = null;
            if (!IsNull(s))
            {
                ConsolColours.PrintFancyText(lines: new string[] {
                    s
                });
            }
            else
            {
                Console.WriteLine("I have exit, because You left a 'null' " +
                    "value assigned to some reference type, you moron!");
                Console.ReadKey();
                ConsolColours.PrintFancyText(lines: new string[] {
                    "Capgemini",
                    ".NET",
                    "Community",
                    "Wroclaw",
                    "2019"
                });
            }
        }

        /* Expected output:
         *
         * I have exit, because You left a 'null' value assigned to some reference type, you moron!
         *
         */
    }

#nullable restore
#endif

    #endregion C#8.0 IsNull(value) (#nullable enabled)
}