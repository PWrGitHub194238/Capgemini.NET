using _15.RecursivePatterns.CompositePattern;

namespace _15.RecursivePatterns
{
    internal static class BoxRepository
    {
        internal static IBox GetBox()
        {
            IBox box = new Box
            {
                Label = "Books"
            };

            box.Push(GetYZBox());
            box.Push(GetVWXBox());
            box.Push(GetSTUBox());
            box.Push(GetPQRBox());
            box.Push(GetMNOBox());
            box.Push(GetJKLBox());
            box.Push(GetGHIBox());
            box.Push(GetDEFBox());
            box.Push(GetABCBox());
            box.Push(null);

            return box;
        }

        private static IBox GetYZBox()
        {
            IBox box = new Box
            {
                Label = "[Y-Z]"
            };

            #region book list

            #endregion book list

            return box;
        }

        private static IBox GetVWXBox()
        {
            IBox box = new Box
            {
                Label = "[V-X]"
            };

            #region book list

            box.Push(new Book
            {
                Title = "Windows PowerShell Best Practices",
                BookType = "PowerShell"
            });

            #endregion book list

            return box;
        }

        private static IBox GetSTUBox()
        {
            IBox box = new Box
            {
                Label = "[S-U]"
            };

            #region book list

            box.Push(new Book
            {
                Title = "TDD. Test-Driven Development techniques",
                BookType = "CleanCode"
            });

            #endregion book list

            return box;
        }

        private static IBox GetPQRBox()
        {
            IBox box = new Box
            {
                Label = "[P-R]"
            };

            #region book list

            box.Push(new Book
            {
                Title = "Python Machine Learning",
                BookType = "ML"
            });

            box.Push(new Book
            {
                Title = "Pro ASP.NET MVC 5",
                BookType = "ASP.NET"
            });

            box.Push(new Book
            {
                Title = "Pro ASP.NET Core MVC 2",
                BookType = "ASP.NET"
            });

            box.Push(new Book
            {
                Title = "Pro Angular",
                BookType = "Angular"
            });

            #endregion book list

            return box;
        }

        private static IBox GetMNOBox()
        {
            IBox box = new Box
            {
                Label = "[M-O]"
            };

            #region book list

            box.Push(new Book
            {
                Title = "Node.js in Action",
                BookType = "JS"
            });

            #endregion book list

            return box;
        }

        private static IBox GetJKLBox()
        {
            IBox box = new Box
            {
                Label = "[J-L]"
            };
            #region book list

            box.Push(new Book
            {
                Title = "JavaScript and JQuery",
                Subtitle = "Interactive Front-End Web Development",
                BookType = "Front"
            });

            #endregion book list

            return box;
        }

        private static IBox GetGHIBox()
        {
            IBox box = new Box
            {
                Label = "[G-I]"
            };

            #region book list

            box.Push(new Book
            {
                Title = "HTML and CSS",
                Subtitle = "Design and Build Websites",
                BookType = "Front"
            });

            #endregion book list

            return box;
        }

        private static IBox GetDEFBox()
        {
            IBox box = new Box
            {
                Label = "[D-F]"
            };

            #region book list

            box.Push(new Book
            {
                Title = "Design Patterns",
                Subtitle = "Elements of Reusable Object-Oriented Software",
                BookType = "CleanCode"
            });

            #endregion book list

            return box;
        }

        private static IBox GetABCBox()
        {
            IBox abcBox = new Box
            {
                Label = "[A-C]"
            };

            #region book list

            abcBox.Push(new Book
            {
                Title = "Adaptive Code",
                Subtitle = "Agile coding with design patterns and SOLID principles",
                BookType = "CleanCode"
            });

            abcBox.Push(new Book
            {
                Title = "ASP.NET Core 2 and Angular 5",
                BookType = "Angular"
            });

            abcBox.Push(new Book
            {
                Title = "ASP.NET Core 2 and Angular 5",
                BookType = "ASP.NET"
            });

            abcBox.Push(new Book
            {
                Title = "Algorithms",
                BookType = "Algorithms"
            });

            abcBox.Push(new Book
            {
                Title = "Clean Architecture",
                Subtitle = "A Craftsman's Guide to Software Structure and Design",
                BookType = "CleanCode"
            });

            abcBox.Push(new Book
            {
                Title = "Clean Code",
                Subtitle = "A Handbook of Agile Software Craftsmanship",
                BookType = "CleanCode"
            });

            #endregion book list

            return abcBox;
        }
    }
}
