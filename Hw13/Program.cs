
using Hw13.Entities;
using Hw13.Services;
using Hw13.Enum;
using Hw13.Contracts;
using Hw13.Configuration;
using Hw13.Repositories;
using Colors.Net;
using Colors.Net.StringColorExtensions;
using ConsoleTables;
using System.Threading.Tasks;
ProgressBar _progressBar = new ProgressBar();
IUserService userService = new UserService(new UserRepository(new LibraryDbContext()));
IBookService bookService = new BookService(new BookRepository(new LibraryDbContext()));
bool loggedIn = false;

while (true)
{
    Console.Clear();
    ColoredConsole.WriteLine("*********Welcome ToDO List*********".DarkGreen());
    ColoredConsole.WriteLine("1.Register".DarkBlue());
    ColoredConsole.WriteLine("2.Login".DarkBlue());
    ColoredConsole.WriteLine("3.Exit".DarkRed());

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            try
            {
                Console.Clear();
                ColoredConsole.WriteLine("********* Register New User *********".DarkGreen());
                ColoredConsole.WriteLine("Enter Full Name: ".DarkYellow());
                string fullName = Console.ReadLine();
                ColoredConsole.WriteLine("Enter Username: ".DarkYellow());
                string userName = Console.ReadLine();
                ColoredConsole.WriteLine("Enter Password: ".DarkYellow());
                string password = Console.ReadLine();
                ColoredConsole.WriteLine("Enter Phone Number: ".DarkYellow());
                string phone = Console.ReadLine();
                ColoredConsole.WriteLine("Enter Role (1:Manager,2:User): ".DarkCyan());
                int role = Convert.ToInt32(Console.ReadLine());
                if (role > 2 && role == 0)
                {
                    ColoredConsole.WriteLine("Wrong|1:Manager,2:User".DarkRed());
                }

                userService.Register(fullName, userName, password, phone, role);
                ColoredConsole.WriteLine("Register Successful".DarkGreen());
                Console.ReadKey();
            }
            catch (FormatException fe)
            {
                ColoredConsole.WriteLine($"Error: {fe.Message}".DarkRed());

            }
            catch (Exception ex)
            {
                ColoredConsole.WriteLine($"Error: {ex.Message}".DarkRed());

            }
            finally
            {
                _progressBar.DisPlay();
            }
            break;

        case "2":
            ColoredConsole.WriteLine("Enter Username: ".DarkYellow());
            string username = Console.ReadLine();
            ColoredConsole.WriteLine("Enter Password: ".DarkYellow());
            string pass = Console.ReadLine();

            try
            {
                userService.Login(username, pass);
                var currentUser = userService.GetCurrentUser();
                if (currentUser == null)
                {
                    ColoredConsole.WriteLine("User Not Logged".DarkRed());
                    Console.ReadKey();
                    break;
                }
                loggedIn = true;
                ColoredConsole.WriteLine("Login Successful".DarkGreen());
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                ColoredConsole.WriteLine($"Error: {ex.Message}".DarkRed());
            }
            finally
            {
                _progressBar.DisPlay();
            }

            break;

        case "3":
            return;
        default:
            ColoredConsole.WriteLine("Invalid".DarkRed());
            Console.ReadKey();
            break;
    }

    if (loggedIn)
    {
        try
        {
            var currentUser = userService.GetCurrentUser();
            if (currentUser.Role == RoleUserEnum.User)
            {
                bool inMenu = true;
                while (inMenu)
                {
                    try
                    {

                        Console.Clear();
                        Console.Clear();
                        ColoredConsole.WriteLine("********* Member Menu *********".DarkGreen());
                        ColoredConsole.WriteLine("1.Borrow Book".DarkBlue());
                        ColoredConsole.WriteLine("2.Return Book".DarkBlue());
                        ColoredConsole.WriteLine("3.View Borrowed Books".DarkBlue());
                        ColoredConsole.WriteLine("4.View Available Books".DarkBlue());
                        ColoredConsole.WriteLine("5.Logout".DarkRed());

                        string input = Console.ReadLine();

                        switch (input)
                        {
                            case "1":

                                var availableBooks = bookService.GetAllBooks().Where(b => b.Status == StatusBookEnum.NotBorrowed).ToList();
                                var table = ConsoleTable.From<Book>(availableBooks);
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                table.Write();
                                Console.ResetColor();
                                ColoredConsole.WriteLine("Enter Book ID to Borrow: ".DarkYellow());

                                int bookId = int.Parse(Console.ReadLine());

                                try
                                {
                                    bool isBookBorrowed = bookService.BorrowBook(currentUser.Id, bookId);
                                    if (isBookBorrowed)
                                    {
                                        ColoredConsole.WriteLine("Borrowed Successfully".DarkGreen());
                                    }
                                    else
                                    {
                                        ColoredConsole.WriteLine("Failed".DarkRed());
                                    }
                                }
                                catch (FormatException)
                                {
                                    ColoredConsole.WriteLine("Invalid Number".DarkRed());
                                }

                                Console.ReadKey();
                                break;

                            case "2":
                                try
                                {
                                    var returnBooks = bookService.GetUserBooks(currentUser.Id);
                                    if (returnBooks.Any())
                                    {
                                        var table1 = ConsoleTable.From<Book>(returnBooks);
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        table1.Write();
                                        Console.ResetColor();
                                        ColoredConsole.WriteLine("Enter Book ID to Return: ".DarkYellow());
                                        int bokId = int.Parse(Console.ReadLine());
                                        bool returnBook = bookService.ReturnBook(currentUser.Id, bokId);
                                        if (returnBook)
                                        {
                                            ColoredConsole.WriteLine("Return Successfully".DarkGreen());
                                        }
                                        else
                                        {
                                            ColoredConsole.WriteLine("Failed".DarkRed());
                                        }
                                    }
                                    else
                                    {
                                        ColoredConsole.WriteLine("No borrowed books found.".DarkRed());
                                    }
                                }
                                catch (FormatException)
                                {
                                    ColoredConsole.WriteLine("Invalid Number".DarkRed());
                                }

                                Console.ReadKey();
                                break;

                            case "3":
                                var borrowedBooks = bookService.GetUserBooks(currentUser.Id);
                                if (borrowedBooks.Any())
                                {
                                    var table1 = ConsoleTable.From<Book>(borrowedBooks);
                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    table1.Write();
                                    Console.ResetColor();
                                }
                                else
                                {
                                    ColoredConsole.WriteLine("Not Found.".DarkRed());
                                }
                                Console.ReadKey();
                                break;

                            case "4":
                                var availableBook = bookService.GetAllBooks().Where(b => b.Status == StatusBookEnum.NotBorrowed).ToList();
                                var table2 = ConsoleTable.From<Book>(availableBook);
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                table2.Write();
                                Console.ResetColor();
                                Console.ReadKey();
                                break;

                            case "5":
                                userService.Logout();
                                inMenu = false;
                                ColoredConsole.WriteLine("Logged Out".DarkRed());
                                Console.ReadKey();
                                break;

                        }
                    }
                    catch (FormatException fe)
                    {
                        ColoredConsole.WriteLine($"Error: {fe.Message}".DarkRed());
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        ColoredConsole.WriteLine($"Error: {ex.Message}".DarkRed());
                        Console.ReadKey();
                    }
                    finally
                    {
                        _progressBar.DisPlay();
                    }
                }
            }
            if (currentUser.Role == RoleUserEnum.Maneger)
            {
                bool manager = true;
                while (manager)
                {
                    try
                    {
                        Console.Clear();
                        ColoredConsole.WriteLine("********* Manager Menu *********".DarkGreen());
                        ColoredConsole.WriteLine("1.View All Books".DarkBlue());
                        ColoredConsole.WriteLine("2.View All Users".DarkBlue());
                        ColoredConsole.WriteLine("3.View Book EndTime Barrowed".DarkBlue());
                        ColoredConsole.WriteLine("4.Logout".DarkRed());

                        string managr = Console.ReadLine();

                        switch (managr)
                        {
                            case "1":
                                var allBooks = bookService.GetAllBooks();
                                var table = ConsoleTable.From<Book>(allBooks);
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                table.Write();
                                Console.ResetColor();
                                Console.ReadKey();
                                break;

                            case "2":
                                var allUsers = userService.GetAllUser();
                                var table1 = ConsoleTable.From<User>(allUsers);
                                Console.ForegroundColor = ConsoleColor.Green;
                                table1.Write();
                                Console.ResetColor();
                                Console.ReadKey();
                                break;



                            case "3":
                                var endTimeBook = bookService.GetListOfBookEndTime();
                                var table3 = ConsoleTable.From<Book>(endTimeBook);
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                table3.Write();
                                Console.ResetColor();
                                ColoredConsole.WriteLine("1. Return Book".DarkBlue());
                                ColoredConsole.WriteLine("2. Add End Time".DarkBlue());
                                int option = int.Parse(Console.ReadLine());
                                if (option == 1)
                                {
                                    ColoredConsole.WriteLine("Enter BookId to Return: ".DarkBlue());
                                    int returnBookId = int.Parse(Console.ReadLine());
                                    try
                                    {
                                        var bookUser = bookService.GetBookById(returnBookId);
                                        bool result = bookService.ReturnBook(bookUser.UserId.Value, returnBookId);
                                        if (result)
                                        {
                                            ColoredConsole.WriteLine("Returned Successfully".DarkGreen());
                                        }
                                        else
                                        {
                                            ColoredConsole.WriteLine("Failed ".DarkRed());
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ColoredConsole.WriteLine($"Error: {ex.Message}".DarkRed());
                                    }
                                    Console.ReadKey();
                                }
                                else if (option == 2)
                                {
                                    ColoredConsole.WriteLine("Enter BookId to Add End Time: ".DarkBlue());
                                    int bokId = int.Parse(Console.ReadLine());

                                    ColoredConsole.WriteLine("Enter Day To Add EndTime: ".DarkBlue());
                                    int newTime = int.Parse(Console.ReadLine());

                                    try
                                    {
                                        bookService.AddEndTimeBarrow(bokId, newTime);
                                        ColoredConsole.WriteLine("End Time Added Successfully".DarkGreen());
                                    }
                                    catch (Exception ex)
                                    {
                                        ColoredConsole.WriteLine($"Error: {ex.Message}".DarkRed());
                                    }
                                }
                                else
                                {
                                    ColoredConsole.WriteLine("Invalid".DarkRed());
                                }
                                Console.ReadKey();
                                break;

                            case "4":
                                userService.Logout();
                                manager = false;
                                ColoredConsole.WriteLine("Logged Out".DarkRed());
                                Console.ReadKey();
                                break;

                        }
                    }
                    catch (FormatException fe)
                    {
                        ColoredConsole.WriteLine($"Error: {fe.Message}".DarkRed());
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        ColoredConsole.WriteLine($"Error: {ex.Message}".DarkRed());
                        Console.ReadKey();
                    }
                    finally
                    {
                        _progressBar.DisPlay();
                    }
                }
            }
        }
        catch (FormatException fe)
        {
            ColoredConsole.WriteLine($"Error: {fe.Message}".DarkRed());
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            ColoredConsole.WriteLine($"Error: {ex.Message}".DarkRed());
            Console.ReadKey();
        }
        finally
        {
            _progressBar.DisPlay();
        }
    }
}





