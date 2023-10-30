
using chat_app;
using chat_app.models;
using Sharprompt;

string databaseName = "Chat";
string tablename = "Users";
string tablename1 = "Chatting";




while (true)
{
    var choice = Prompt.Select("", new[] { "Kirish", "Ro`yhatdan o`tish", "Exit" });

    if (choice == "Kirish")
    {
        Console.WriteLine("Username :");
        string user_name = Console.ReadLine();
        List<string> pass = Services.GetUsername(tablename, databaseName, user_name);
        if (pass != null)
        {

            Console.WriteLine("Password :");

            string passw = Console.ReadLine();
            if (passw.Equals(pass[3]))
            {
                while (true)
                {
                    var user = new User(pass);
                    var user1id = Prompt.Select("", Services.GetAll(tablename, databaseName).Select(x => x[2]).ToList());
                    var chat = Prompt.Select("", new[] { "Xabarlarni ko`rish", "Xabar yuborish", "Exit" });
                    if (chat == "Xabarlarni ko`rish")
                    {
                        user.GetAllChats(tablename1, databaseName, user1id).Select(x => Services.GetUsernameById(tablename, databaseName, x[1]) + " :  " + x[3] + "  :" + x[4]).
                            ToList().ForEach(x => Console.WriteLine("\t" + x));

                    }
                    else if (chat == "Xabar yuborish")
                    {
                        Console.WriteLine("Xabar kiriting");
                        string s = Console.ReadLine();
                        if (user.SendMessage(tablename1, databaseName, user1id, s))
                        {
                            Console.WriteLine("Xabar yuborildi");
                        }
                        else
                        {
                            Console.WriteLine("Xabar yuborilmadi");
                        }

                    }
                    else
                    {
                        break;
                    }

                }
            }
            else
            {
                Console.WriteLine("Parol noto`gri kiritilgan !!!");
                continue;
            }

        }
        else
        {
            Console.WriteLine("Username topilmadi !!!");
            continue;
        }


    }

    else if (choice == "Ro`yhatdan o`tish")
    {
        Console.WriteLine("Ismingizni kiriting");
        string name = Console.ReadLine();
        Console.WriteLine("Username kiriting");
        string newUsername = Console.ReadLine();
        if (Services.CheckUsername(tablename, databaseName, newUsername))
        {
            Console.WriteLine("Parol kiriting");
            string p = Console.ReadLine();
            List<Table> user = new List<Table>()
            {
                new Table()
                {
                    ColumnName ="Name",
                    Value1 = name,
                },
                new Table()
                {
                    ColumnName ="Username",
                    Value1 = newUsername,
                },
                new Table()
                {
                    ColumnName ="password",
                    Value1 = p
                }

            };
            if (Services.Register(tablename, databaseName, user))
            {
                Console.WriteLine("Royhatdan o`tdingiz !!!");
            }
            else
            {
                Console.WriteLine("Ro`yhatdan o`tishda xatolik !!!");
            }


        }
        else
        {
            Console.WriteLine("Bunday Username band");
        }

    }
    else
    {
        return 0;
    }
}







