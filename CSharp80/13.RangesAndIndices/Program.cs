#region Define

#region C#7.0 Integer index (start)
// #define _01_CSharp70_INTEGER_INDEX_START
#endregion C#7.0 Integer index (start)

#region C#8.0 Index (start)
// #define _02_CSharp80_INDEX_START
#endregion C#8.0 Index (start)

#region C#7.0 Integer index (end)
// #define _03_CSharp70_INTEGER_INDEX_END
#endregion C#7.0 Integer index (end)

#region C#8.0 Index (end)
// #define _04_CSharp80_INDEX_END
#endregion C#8.0 Index (end)

#region C#7.0 Substring (start)
// #define _05_CSharp70_SUBSTRING_START
#endregion C#7.0 Range (start)

#region C#8.0 Range (start)
// #define _06_CSharp80_RANGE_START
#endregion C#8.0 Range (start)

#region C#7.0 Substring (end)
// #define _07_CSharp70_SUBSTRING_END
#endregion C#7.0 Range (end)

#region C#8.0 Range (end)
// #define _08_CSharp80_RANGE_END
#endregion C#8.0 Range (end)

#region C#8.0 Range
#define _09_CSharp80_RANGE
#endregion C#8.0 Range

#endregion Define

using _13.RangesAndIndices.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace _13.RangesAndIndices
{
    /* Execute-Example -ProjectName 13.RangesAndIndices -LangVersion 7.0 -DefineSection _01_CSharp70_INTEGER_INDEX_START
     *
     * Prints:
     * Input array: '[Hello, Word, from, C#, 7.0!]',
     * input index: '0',
     * output: 'Hello'.
     */

    #region C#7.0 Integer index (start)

#if _01_CSharp70_INTEGER_INDEX_START

    #region C#7.0 Integer index test class (start)

    public class StartIndexTestData : IEnumerable<object[]>
    {
        private static readonly string[] INPUT = new string[] {
            "Hello", "Word", "from", "C#", "7.0!"
        };
        private static readonly string EXPECTED_OUTPUT = INPUT[0];

        public IEnumerator<object[]> GetEnumerator()
        {
            // Inputs: string input, int index
            // Outputs: string expectedOutput

            yield return new object[] {
                INPUT,
                0,
                EXPECTED_OUTPUT,
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion C#7.0 Integer index test class (start)

    internal class Program
    {

    #region C#7.0 Integer index impl (start)

        private static void PrintTestOutput(string[] input, int indices, string actualOutput)
        {
            ConsolColours.WrapPowerShellColors();

            Colorful.Console.WriteLineFormatted(format: "Input array: '[{0}]',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: string.Join(", ", input));
            Colorful.Console.WriteLineFormatted(format: "input index: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: indices);
            Colorful.Console.WriteLineFormatted(format: "output: '{0}'.",
                styledColor: Color.LightGreen,
                defaultColor: Color.White,
                arg0: actualOutput);
        }

        private static void GetFirstElementFromStringArray(Action<string[], int, string> summaryAction)
        {
            StartIndexTestData testDataCollection = new StartIndexTestData();
            foreach (var testData in testDataCollection)
            {
                // Arrange
                string[] input = (string[])testData[0];
                int startIndex = (int)testData[1];
                string expectedOutput = (string)testData[2];

                // Act
                string actualOutput = input[startIndex];

                // Assert
                summaryAction(input, startIndex, actualOutput);
            }
        }

    #endregion C#7.0 Integer index impl (start)

        private static void Main()
        {
            GetFirstElementFromStringArray(PrintTestOutput);
        }

        /* Expected output:
         *
         * Input array: '[Hello, Word, from, C#, 7.0!]',
         * input index: '0',
         * output: 'Hello'.
         *
         */
    }

#endif

    #endregion C#7.0 Integer index (start)

    /* Execute-Example -ProjectName 13.RangesAndIndices -LangVersion 8.0 -DefineSection _02_CSharp80_INDEX_START
     *
     * Prints:
     * Input array: '[Hello, Word, from, C#, 8.0!, This, is, the, indices, feature!]',
     * input index: '0',
     * output: 'Hello'.
     * ...
     */

    #region C#8.0 Index (start)

#if _02_CSharp80_INDEX_START

    #region C#8.0 Index test class (start)

    public class StartIndexTestData : IEnumerable<object[]>
    {
        private static readonly string[] INPUT = new string[] {
            "Hello", "Word", "from", "C#", "8.0!",
            "This", "is", "the", "indices", "feature!"
        };
        private static readonly string EXPECTED_OUTPUT = INPUT[0];

        public IEnumerator<object[]> GetEnumerator()
        {
            // Inputs: string input, Index indices
            // Outputs: string expectedOutput

            yield return new object[] {
                INPUT,
                Index.Start,
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                (Index)0,
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                Index.FromStart(0),
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                new Index(0),
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                new Index(0,
                    fromEnd: false),
                EXPECTED_OUTPUT,
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion C#8.0 Index test class (start)

    internal class Program
    {

    #region C#8.0 Index impl (start)

        private static void PrintTestOutput(string[] input, Index indices, string actualOutput)
        {
            ConsolColours.WrapPowerShellColors();

            Colorful.Console.WriteLineFormatted(format: "Input array: '[{0}]',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: string.Join(", ", input));
            Colorful.Console.WriteLineFormatted(format: "input index: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: indices);
            Colorful.Console.WriteLineFormatted(format: "output: '{0}'.",
                styledColor: Color.LightGreen,
                defaultColor: Color.White,
                arg0: actualOutput);
        }

        private static void GetFirstElementFromStringArray(Action<string[], Index, string> summaryAction)
        {
            StartIndexTestData testDataCollection = new StartIndexTestData();
            foreach (var testData in testDataCollection)
            {
                // Arrange
                string[] input = (string[])testData[0];
                Index startIndex = (Index)testData[1];
                string expectedOutput = (string)testData[2];

                // Act
                string actualOutput = input[startIndex];

                // Assert
                summaryAction(input, startIndex, actualOutput);
            }
        }

    #endregion C#8.0 Index impl (start)

        private static void Main()
        {
            GetFirstElementFromStringArray(PrintTestOutput);
        }

        /* Expected output:
         *
         * Input array: '[Hello, Word, from, C#, 8.0!, This, is, the, indices, feature!]',
         * input index: '0',
         * output: 'Hello'.
         * ...
         *
         */
    }

#endif

    #endregion C#8.0 Index (start)

    /* Execute-Example -ProjectName 13.RangesAndIndices -LangVersion 7.0 -DefineSection _03_CSharp70_INTEGER_INDEX_END
     *
     * Prints:
     * Input array: '[Hello, Word, from, C#, 7.0!]',
     * input index: '4',
     * output: '7.0!'.
     */

    #region C#7.0 Integer index (end)

#if _03_CSharp70_INTEGER_INDEX_END

    #region C#7.0 Integer index test class (end)

    public class EndIndexTestData : IEnumerable<object[]>
    {
        private static readonly string[] INPUT = new string[] {
            "Hello", "Word", "from", "C#", "7.0!"
        };
        private static readonly string EXPECTED_OUTPUT = INPUT[INPUT.Length - 1];

        public IEnumerator<object[]> GetEnumerator()
        {
            // Inputs: string input, int index
            // Outputs: string expectedOutput

            yield return new object[] {
                INPUT,
                INPUT.Length - 1,
                EXPECTED_OUTPUT,
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion C#7.0 Integer index test class (end)

    internal class Program
    {

    #region C#7.0 Integer index impl (end)

        private static void PrintTestOutput(string[] input, int indices, string actualOutput)
        {
            ConsolColours.WrapPowerShellColors();

            Colorful.Console.WriteLineFormatted(format: "Input array: '[{0}]',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: string.Join(", ", input));
            Colorful.Console.WriteLineFormatted(format: "input index: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: indices);
            Colorful.Console.WriteLineFormatted(format: "output: '{0}'.",
                styledColor: Color.LightGreen,
                defaultColor: Color.White,
                arg0: actualOutput);
        }

        private static void GetLastElementFromStringArray(Action<string[], int, string> summaryAction)
        {
            EndIndexTestData testDataCollection = new EndIndexTestData();
            foreach (var testData in testDataCollection)
            {
                // Arrange
                string[] input = (string[])testData[0];
                int startIndex = (int)testData[1];
                string expectedOutput = (string)testData[2];

                // Act
                string actualOutput = input[startIndex];

                // Assert
                summaryAction(input, startIndex, actualOutput);
            }
        }

    #endregion C#7.0 Integer index impl (end)

        private static void Main()
        {
            GetLastElementFromStringArray(PrintTestOutput);
        }

        /* Expected output:
         *
         * Input array: '[Hello, Word, from, C#, 7.0!]',
         * input index: '4',
         * output: '7.0!'.
         *
         */
    }

#endif

    #endregion C#7.0 Integer index (end)

    /* Execute-Example -ProjectName 13.RangesAndIndices -LangVersion 8.0 -DefineSection _04_CSharp80_INDEX_END
     *
     * Prints:
     * Input array: '[Hello, Word, from, C#, 8.0!, This, is, the, indices, feature!]',
     * input index: 9,
     * output: 'feature!'.
     * ...
     */

    #region C#8.0 Index (end)

#if _04_CSharp80_INDEX_END

    #region C#8.0 Index test class (end)

    public class EndIndexTestData : IEnumerable<object[]>
    {
        private static readonly string[] INPUT = new string[] {
            "Hello", "Word", "from", "C#", "8.0!",
            "This", "is", "the", "indices", "feature!"
        };
        private static readonly string EXPECTED_OUTPUT = INPUT[INPUT.Length - 1];

        public IEnumerator<object[]> GetEnumerator()
        {
            // Inputs: string input, Index indices
            // Outputs: string expectedOutput

            yield return new object[] {
                INPUT,
                (Index)(
                    INPUT.Length-1),
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                ^1,
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                Index.FromEnd(1),
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                new Index(
                    INPUT.Length-1),
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                new Index(1,
                    fromEnd: true),
                EXPECTED_OUTPUT,
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion C#8.0 Index test class (end)

    internal class Program
    {

    #region C#8.0 Index impl (end)

        private static void PrintTestOutput(string[] input, Index indices, string actualOutput)
        {
            ConsolColours.WrapPowerShellColors();

            Colorful.Console.WriteLineFormatted(format: "Input array: '[{0}]',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: string.Join(", ", input));
            Colorful.Console.WriteLineFormatted(format: "input index: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: indices);
            Colorful.Console.WriteLineFormatted(format: "output: '{0}'.",
                styledColor: Color.LightGreen,
                defaultColor: Color.White,
                arg0: actualOutput);
        }

        private static void GetLastElementFromStringArray(Action<string[], Index, string> summaryAction)
        {
            EndIndexTestData testDataCollection = new EndIndexTestData();
            foreach (var testData in testDataCollection)
            {
                // Arrange
                string[] input = (string[])testData[0];
                Index endIndex = (Index)testData[1];
                string expectedOutput = (string)testData[2];

                // Act
                string actualOutput = input[endIndex];

                // Assert
                summaryAction(input, endIndex, actualOutput);
            }
        }

    #endregion C#8.0 Index impl (end)

        private static void Main()
        {
            GetLastElementFromStringArray(PrintTestOutput);
        }

        /* Expected output:
         *
         * Input array: '[Hello, Word, from, C#, 8.0!, This, is, the, indices, feature!]',
         * input index: '9',
         * output: 'Hello'.
         * ...
         *
         */
    }

#endif

    #endregion C#8.0 Index (start)

    /* Execute-Example -ProjectName 13.RangesAndIndices -LangVersion 7.0 -DefineSection _05_CSharp70_SUBSTRING_START
     *
     * Prints:
     * Input string: 'Hello Word from C# 7.0!',
     * input range: '0',
     * output: 'Hello Word from C# 7.0!'.
     * ...
     */

    #region C#7.0 Substring (start)

#if _05_CSharp70_SUBSTRING_START

    #region C#7.0 Substring test class (start)

    public class StartRangeTestData : IEnumerable<object[]>
    {
        private static readonly string INPUT = "Hello Word from C# 7.0!";
        private static readonly string EXPECTED_OUTPUT = INPUT.Substring(0);

        public IEnumerator<object[]> GetEnumerator()
        {
            // Inputs: string input, int range
            // Outputs: string expectedOutput

            yield return new object[] {
                INPUT,
                0,
                EXPECTED_OUTPUT,
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion C#7.0 Substring test class (start)

    internal class Program
    {

    #region C#7.0 Substring impl (start)

        private static void PrintTestOutput(string input, int range, string actualOutput)
        {
            ConsolColours.WrapPowerShellColors();

            Colorful.Console.WriteLineFormatted(format: "Input string: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: input);
            Colorful.Console.WriteLineFormatted(format: "input range: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: range);
            Colorful.Console.WriteLineFormatted(format: "output: '{0}'.",
                styledColor: Color.LightGreen,
                defaultColor: Color.White,
                arg0: actualOutput);
        }

        private static void GetStringFromStart(Action<string, int, string> summaryAction)
        {
            StartRangeTestData testDataCollection = new StartRangeTestData();
            foreach (var testData in testDataCollection)
            {
                // Arrange
                string input = (string)testData[0];
                int beginRange = (int)testData[1];
                string expectedOutput = (string)testData[2];

                // Act
                string actualOutput = input.Substring(beginRange);

                // Assert
                summaryAction(input, beginRange, actualOutput);
            }
        }

    #endregion C#7.0 Substring impl (start)

        private static void Main()
        {
            GetStringFromStart(PrintTestOutput);
        }

        /* Expected output:
         * 
         * Input string: 'Hello Word from C# 7.0!',
         * input range: '0',
         * output: 'Hello Word from C# 7.0!'.
         * ...
         *
         */
    }

#endif

    #endregion C#7.0 Substring (start)

    /* Execute-Example -ProjectName 13.RangesAndIndices -LangVersion 8.0 -DefineSection _06_CSharp80_RANGE_START
     *
     * Prints:
     * Input string: 'Hello Word from C# 8.0! This is the range feature!',
     * input range: '0..^0',
     * output: 'Hello Word from C# 8.0! This is the range feature!'.
     * ...
     */

    #region C#8.0 Range (start)

#if _06_CSharp80_RANGE_START

    #region C#8.0 Range test class (start)

    public class StartRangeTestData : IEnumerable<object[]>
    {
        private static readonly string INPUT = "Hello Word from C# 8.0! This is the range feature!";
        private static readonly string EXPECTED_OUTPUT = INPUT.Substring(0);

        public IEnumerator<object[]> GetEnumerator()
        {
            // Inputs: string input, Range range
            // Outputs: string expectedOutput

            yield return new object[] {
                INPUT,
                Range.All,
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                Range.StartAt(0),
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                Range.StartAt(
                    Index.Start),
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                new Range(0,^0),
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                0..,
                EXPECTED_OUTPUT,
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion C#8.0 Range test class (start)

    internal class Program
    {

    #region C#8.0 Range impl (start)

        private static void PrintTestOutput(string input, Range range, string actualOutput)
        {
            ConsolColours.WrapPowerShellColors();

            Colorful.Console.WriteLineFormatted(format: "Input string: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: input);
            Colorful.Console.WriteLineFormatted(format: "input range: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: range);
            Colorful.Console.WriteLineFormatted(format: "output: '{0}'.",
                styledColor: Color.LightGreen,
                defaultColor: Color.White,
                arg0: actualOutput);
        }

        private static void GetStringFromStart(Action<string, Range, string> summaryAction)
        {
            StartRangeTestData testDataCollection = new StartRangeTestData();
            foreach (var testData in testDataCollection)
            {
                // Arrange
                string input = (string)testData[0];
                Range beginRange = (Range)testData[1];
                string expectedOutput = (string)testData[2];

                // Act
                string actualOutput = input[beginRange];

                // Assert
                summaryAction(input, beginRange, actualOutput);
            }
        }

    #endregion C#8.0 Range impl (start)

        private static void Main()
        {
            GetStringFromStart(PrintTestOutput);
        }

        /* Expected output:
         * 
         * Input string: 'Hello Word from C# 8.0! This is the range feature!',
         * input range: '0..^0',
         * output: 'Hello Word from C# 8.0! This is the range feature!'.
         * ...
         *
         */
    }

#endif

    #endregion C#8.0 Range (start)

    /* Execute-Example -ProjectName 13.RangesAndIndices -LangVersion 7.0 -DefineSection _07_CSharp70_SUBSTRING_END
     *
     * Prints:
     * Input string: 'Hello Word from C# 7.0!',
     * input range: '23',
     * output: 'Hello Word from C# 7.0!'.
     * ...
     */

    #region C#7.0 Substring (end)

#if _07_CSharp70_SUBSTRING_END

    #region C#7.0 Substring test class (end)

    public class EndRangeTestData : IEnumerable<object[]>
    {
        private static readonly string INPUT = "Hello Word from C# 7.0!";
        private static readonly string EXPECTED_OUTPUT = INPUT.Substring(0, INPUT.Length);

        public IEnumerator<object[]> GetEnumerator()
        {
            // Inputs: string input, Range range
            // Outputs: string expectedOutput

            yield return new object[] {
                INPUT,
                INPUT.Length,
                EXPECTED_OUTPUT,
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion C#7.0 Substring test class (end)

    internal class Program
    {

    #region C#7.0 Substring impl (end)

        private static void PrintTestOutput(string input, int range, string actualOutput)
        {
            ConsolColours.WrapPowerShellColors();

            Colorful.Console.WriteLineFormatted(format: "Input string: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: input);
            Colorful.Console.WriteLineFormatted(format: "input range: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: range);
            Colorful.Console.WriteLineFormatted(format: "output: '{0}'.",
                styledColor: Color.LightGreen,
                defaultColor: Color.White,
                arg0: actualOutput);
        }

        private static void GetStringToEnd(Action<string, int, string> summaryAction)
        {
            EndRangeTestData testDataCollection = new EndRangeTestData();
            foreach (var testData in testDataCollection)
            {
                // Arrange
                string input = (string)testData[0];
                int endRange = (int)testData[1];
                string expectedOutput = (string)testData[2];

                // Act
                string actualOutput = input.Substring(0, endRange);

                // Assert
                summaryAction(input, endRange, actualOutput);
            }
        }

    #endregion C#7.0 Substring impl (end)

        private static void Main()
        {
            GetStringToEnd(PrintTestOutput);
        }

        /* Expected output:
         * 
         * Input string: 'Hello Word from C# 7.0!',
         * input range: '23',
         * output: 'Hello Word from C# 7.0!'.
         * ...
         *
         */
    }

#endif

    #endregion C#7.0 Substring (end)

    /* Execute-Example -ProjectName 13.RangesAndIndices -LangVersion 8.0 -DefineSection _08_CSharp80_RANGE_END
     *
     * Prints:
     * Input string: 'Hello Word from C# 8.0! This is the range feature!',
     * input range: '0..^0',
     * output: 'Hello Word from C# 8.0! This is the range feature!'.
     * ...
     */

    #region C#8.0 Range (end)

#if _08_CSharp80_RANGE_END

    #region C#8.0 Range test class (end)

    public class EndRangeTestData : IEnumerable<object[]>
    {
        private static readonly string INPUT = "Hello Word from C# 8.0! This is the range feature!";
        private static readonly string EXPECTED_OUTPUT = INPUT[0..^0];

        public IEnumerator<object[]> GetEnumerator()
        {
            // Inputs: string input, Range range
            // Outputs: string expectedOutput

            yield return new object[] {
                INPUT,
                Range.All,
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                ..^0,
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                Range.EndAt(0),
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                Range.EndAt(
                    Index.End),
                EXPECTED_OUTPUT,
            };

            yield return new object[] {
                INPUT,
                new Range(0,^0),
                EXPECTED_OUTPUT,
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion C#8.0 Range test class (end)

    internal class Program
    {

    #region C#8.0 Range impl (end)

        private static void PrintTestOutput(string input, Range range, string actualOutput)
        {
            ConsolColours.WrapPowerShellColors();

            Colorful.Console.WriteLineFormatted(format: "Input string: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: input);
            Colorful.Console.WriteLineFormatted(format: "input range: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: range);
            Colorful.Console.WriteLineFormatted(format: "output: '{0}'.",
                styledColor: Color.LightGreen,
                defaultColor: Color.White,
                arg0: actualOutput);
        }

        private static void GetStringToEnd(Action<string, Range, string> summaryAction)
        {
            EndRangeTestData testDataCollection = new EndRangeTestData();
            foreach (var testData in testDataCollection)
            {
                // Arrange
                string input = (string)testData[0];
                Range endRange = (Range)testData[1];
                string expectedOutput = (string)testData[2];

                // Act
                string actualOutput = input[endRange];

                // Assert
                summaryAction(input, endRange, actualOutput);
            }
        }

    #endregion C#8.0 Range impl (end)

        private static void Main()
        {
            GetStringToEnd(PrintTestOutput);
        }

        /* Expected output:
         * 
         * Input string: 'Hello Word from C# 8.0! This is the range feature!',
         * input range: '0..^0',
         * output: 'Hello Word from C# 8.0! This is the range feature!'.
         * ...
         *
         */
    }

#endif

    #endregion C#8.0 Range (end)

    /* Execute-Example -ProjectName 13.RangesAndIndices -LangVersion 8.0 -DefineSection _09_CSharp80_RANGE
     *
     * Prints:
     * Input string: 'Hello Word from C# 8.0! This is the range feature!',
     * input range: '0..^0',
     * expected output: 'Hello Word from C# 8.0! This is the range feature!'.
     * actual output: 'Hello Word from C# 8.0! This is the range feature!'.
     * ...
     */

    #region C#8.0 Range

#if _09_CSharp80_RANGE

    #region C#8.0 Range test class

    public class RangeTestData : IEnumerable<object[]>
    {
        private static readonly string INPUT = "Hello Word from C# 8.0! This is the range feature!";

        public IEnumerator<object[]> GetEnumerator()
        {
            // Inputs: string input, Range range
            // Outputs: string expectedOutput

            yield return new object[] {
                INPUT,
                ..,
                INPUT.Substring(0, INPUT.Length)
            };

            yield return new object[] {
                INPUT,
                6..10,
                INPUT.Substring(6, 10 - 6)
            };

            yield return new object[] {
                INPUT,
                6..^(INPUT.Length-10),
               INPUT.Substring(6, (INPUT.Length - (INPUT.Length-10)) - 6)
            };

            yield return new object[] {
                INPUT,
                ^44..^40,
                INPUT.Substring(INPUT.Length-44, (INPUT.Length-40) - (INPUT.Length-44))
            };

            yield return new object[] {
                INPUT,
                ^44..10,
                INPUT.Substring(INPUT.Length-44, 10 - (INPUT.Length-44))
            };

            yield return new object[] {
                INPUT,
                ..10,
                INPUT.Substring(0, 10)
            };

            yield return new object[] {
                INPUT,
                ..^40,
                INPUT.Substring(0,INPUT.Length - 40)
            };

            yield return new object[] {
                INPUT,
                24..,
                INPUT.Substring(24)
            };

            yield return new object[] {
                INPUT,
                ^26..,
                INPUT.Substring(INPUT.Length - 26)
            };

            yield return new object[] {
                INPUT,
                new Range(24,^0),
                INPUT.Substring(24, INPUT.Length -24)
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion C#8.0 Range test class

    internal class Program
    {

        #region C#8.0 Range impl

        private static void PrintTestOutput(string input, Range range, string expectedOutput, string actualOutput)
        {
            ConsolColours.WrapPowerShellColors();

            Colorful.Console.WriteLineFormatted(format: "Input string: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: input);
            Colorful.Console.WriteLineFormatted(format: "input range: '{0}',",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: range);
            Colorful.Console.WriteLineFormatted(format: "expected output: '{0}'.",
                styledColor: Color.LightGray,
                defaultColor: Color.White,
                arg0: expectedOutput);
            Colorful.Console.WriteLineFormatted(format: "actual output: '{0}'.",
                styledColor: Color.LightGreen,
                defaultColor: Color.White,
                arg0: actualOutput);
            Console.WriteLine();
        }

        private static void GetString(Action<string, Range, string, string> summaryAction)
        {
            RangeTestData testDataCollection = new RangeTestData();
            foreach (var testData in testDataCollection)
            {
                // Arrange
                string input = (string)testData[0];
                Range range = (Range)testData[1];
                string expectedOutput = (string)testData[2];

                // Act
                string actualOutput = input[range];

                // Assert
                summaryAction(input, range, expectedOutput, actualOutput);
            }
        }

        #endregion C#8.0 Range impl

        private static void Main()
        {
            GetString(PrintTestOutput);
        }

        /* Expected output:
         * 
         * Input string: 'Hello Word from C# 8.0! This is the range feature!',
         * input range: '0..^0',
         * expected output: 'Hello Word from C# 8.0! This is the range feature!'.
         * actual output: 'Hello Word from C# 8.0! This is the range feature!'.
         * ...
         *
         */
    }

#endif

    #endregion C#8.0 Range
}