using System;
using Microsoft.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace LocalBank
{
    class Program
    {
        static List<Credit> credits = new List<Credit>();
        static int CreditId, CurrenciesId, CardsId;
        static List<Currencies> currenciesS = new List<Currencies>();
        static List<Card> cards = new List<Card>();
        static List<int> CashboxList = new List<int>() ;
        static List<Credit> creditList = new List<Credit>();
        static Client client;
        static Manager manager;
        static Cashier cashier;
        static string strBlock = "================================";
        static string menuPush;
        static string strEnter = "Введите значение : ";
        static SqliteConnection bd_connection = new SqliteConnection("Filename=bankdata.db;Foreign Keys=False");
        static void Main(string[] args)
        {
            //Глобальные переменные
            string id;
            //БД
            string findClient = "SELECT * FROM Clients WHERE _id = ";
            string findManager = "SELECT * FROM Managers WHERE _id = ";
            string findCashiers = "SELECT * FROM  Cashiers WHERE _id = ";
            bd_connection.Open();
            //вход
            while (true)
            {
                menuPush = "0";
                Console.Clear();
                Console.WriteLine("1 - Клиент");
                Console.WriteLine("2 - Менеджер");
                Console.WriteLine("3 - Кассир");
                Console.WriteLine("4 - Выйти");
                Console.WriteLine("Введите значение");
                menuPush = Console.ReadLine();
                switch (Convert.ToInt16(menuPush))
                {
                    case 1:
                        {
                            Console.Clear();
                            /*поиск записи*/
                            Console.WriteLine("Введите айди клиента : ");
                            id = Console.ReadLine();
                            SqliteCommand command = new SqliteCommand(findClient + id, bd_connection);
                            SqliteDataReader reader = command.ExecuteReader();//считывание данных
                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read())   // построчно считываем данные
                                {
                                    client = new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDouble(4));
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Такого нет");
                                Console.WriteLine("Добавление пользователя (0 - отмена)");
                                Console.WriteLine("Введите имя");
                                string firstname = Console.ReadLine();
                                if ((firstname) == "0") { break; }
                                Console.WriteLine("Введите инициалы или фамилию");
                                string secondname = Console.ReadLine();
                                Client client = new Client();
                                client.setSecondName(secondname);
                                client.setFirstName(firstname);
                                string clientAdd = "INSERT INTO Clients (fName,sName) values ('"+client.getFirstName()+ "','" +client.getSecondName()+ "');";

                                SqliteCommand command2 = new SqliteCommand(clientAdd, bd_connection);
                                command2.ExecuteNonQuery();
                                Console.ReadKey();
                                break;
                            }


                            //события
                            
                            clientInfo();
                            client.getCash();
                        }
                        break;

                    case 2:
                        {
                            Console.Clear();
                            /*поиск записи*/
                            Console.WriteLine("Введите айди менеджера : ");
                            id = Console.ReadLine();
                            SqliteCommand command = new SqliteCommand(findManager + id, bd_connection);
                            SqliteDataReader reader = command.ExecuteReader();//считывание данных
                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read())   // построчно считываем данные
                                {
                                    manager = new Manager(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Такого нет");
                                Console.WriteLine("Добавление менеджера (0 - отмена)");
                                Console.WriteLine("Введите имя");
                                string firstname = Console.ReadLine();
                                if ((firstname) == "0") { break; }
                                Console.WriteLine("Введите инициалы или фамилию");
                                string secondname = Console.ReadLine();
                                Manager manager = new Manager();
                                manager.setSecondName(secondname);
                                manager.setFirstName(firstname);
                                string managerAdd = "INSERT INTO Managers (fName,sName) values ('" + manager.getFirstName() + "','" + manager.getSecondName() + "');";

                                SqliteCommand command2 = new SqliteCommand(managerAdd, bd_connection);
                                command2.ExecuteNonQuery();
                                Console.ReadKey();
                                break;
                            }


                            //события
                            managerInfo();
                        }
                        break;


                    case 3: {

                            Console.Clear();
                            /*поиск записи*/
                            Console.WriteLine("Введите айди кассира : ");
                            id = Console.ReadLine();
                            SqliteCommand command = new SqliteCommand(findManager + id, bd_connection);
                            SqliteDataReader reader = command.ExecuteReader();//считывание данных
                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read())   // построчно считываем данные
                                {
                                    cashier = new Cashier(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Такого нет");
                                Console.WriteLine("Добавление кассира (0 - отмена)");
                                Console.WriteLine("Введите имя");
                                string firstname = Console.ReadLine();
                                if ((firstname) == "0") { break; }
                                Console.WriteLine("Введите инициалы или фамилию");
                                string secondname = Console.ReadLine();
                                Cashier cashier = new Cashier();
                                cashier.setSecondName(secondname);
                                cashier.setFirstName(firstname);
                                string cashierAdd = "INSERT INTO Cashiers (fName,sName) values ('" + cashier.getFirstName() + "','" + cashier.getSecondName() + "');";

                                SqliteCommand command2 = new SqliteCommand(cashierAdd, bd_connection);
                                command2.ExecuteNonQuery();
                                Console.ReadKey();
                                break;
                            }

                            //события
                            
                            cashierInfo();
                        }
                        break;
                    case 4:
                        {
                            if(client!= null)
                            clientUpdate();
                            return;
                        }
                        break;
                    default:
                        break;
                }


            }
        }

        public static void managerInfo()
        {
            while (Convert.ToInt16(menuPush) != 4)
            {
                Console.Clear();
                manager.show();
                Console.WriteLine("1 - Операции с кредитами");
                Console.WriteLine("2 - Операции с картами");
                Console.WriteLine("3 - Клиенты банка");
                Console.WriteLine("4 - Выход");
                Console.WriteLine(strBlock);
                menuPush = Console.ReadLine();
                switch (Convert.ToInt16(menuPush))
                {
                    case 1:
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine(strBlock);
                                creditView();
                                Console.WriteLine(strBlock);
                                Console.WriteLine("Операции с кредитом");//событие
                                Console.WriteLine("1 - Одобрить кредит");//событие
                                Console.WriteLine("2 - Отклонить кредит");//событие
                                Console.WriteLine("3 - Закрыть кредит");//событие
                                Console.WriteLine("4 - Назад");
                                Console.WriteLine(strBlock);
                                menuPush = Console.ReadLine();
                                Console.WriteLine("Введите айди кредита :");
                                switch (Convert.ToInt16(menuPush))
                                {
                                    case 1:
                                        {
                                            menuPush = Console.ReadLine();

                                            foreach (Credit one in credits)
                                            {
                                                if (one.getId() == Convert.ToInt16(menuPush))
                                                {

                                                    clientShow(one.getId_client());
                                                    string creditOK = manager.ВыдатьКредит(one);
                                                    SqliteCommand command2 = new SqliteCommand(creditOK, bd_connection);
                                                    SqliteDataReader reader2 = command2.ExecuteReader();//считывание данных
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                    case 2:
                                        {
                                            menuPush = Console.ReadLine();
                                            foreach (Credit one in credits)
                                            {
                                                if (one.getId() == Convert.ToInt16(menuPush) && one.getStatusCredit() == 0)
                                                {
                                                    clientShow(one.getId_client());
                                                    string creditNO = "DELETE FROM Credits where _id = " + one.getId();
                                                    SqliteCommand command = new SqliteCommand(creditNO, bd_connection);
                                                    SqliteDataReader reader = command.ExecuteReader();//считывание данных
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                    case 3:
                                        {
                                            menuPush = Console.ReadLine();
                                            foreach (Credit one in credits)
                                            {
                                                if (one.getId() == Convert.ToInt16(menuPush) && one.getStatusCredit() == 2)
                                                {

                                                    Console.Clear();
                                                    client = clientShow(one.getId_client());
                                                    Console.WriteLine("Кредитная история");
                                                    creditInfo(client);
                                                    Console.WriteLine(one.history);
                                                    Console.WriteLine(strBlock);
                                                    Console.WriteLine("Измените приоритет клиента в зависимости от срока погашения");
                                                    menuPush = Console.ReadLine();
                                                    client.setPriority(Convert.ToInt32(menuPush));
                                                    Console.WriteLine(strBlock);
                                                    string creditNO = "DELETE FROM Credits where _id = " + one.getId();
                                                    SqliteCommand command = new SqliteCommand(creditNO, bd_connection);
                                                    SqliteDataReader reader = command.ExecuteReader();//считывание данных
                                                    string updateClient = "UPDATE Clients SET Priority = " + client.getPriority() + " , cash = " + client.getCash() + " where _id = " + client.getId() + ";";

                                                    SqliteCommand command2 = new SqliteCommand(updateClient, bd_connection);
                                                    command.ExecuteNonQuery();
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                    case 4:
                                        {
                                        }
                                        break;
                                    default: break;
                                }
                                if (Convert.ToInt16(menuPush) == 4) break;
                            }
                        }
                        break;
                    case 2:
                        {
                            menuPush = null;
                            while (Convert.ToInt16(menuPush) != 3)
                            {
                                Console.Clear();
                                Console.WriteLine(strBlock);

                                Console.WriteLine("1 - Выдать карту");//событие
                                Console.WriteLine("2 - Закрыть карту");
                                Console.WriteLine("3 - Назад");

                                Console.WriteLine(strBlock);
                                menuPush = Console.ReadLine();

                                switch (Convert.ToInt16(menuPush))
                                {
                                    case 1:
                                        {
                                            string cardVIEW = manager.ВыдатьКарту();
                                            SqliteCommand command = new SqliteCommand(cardVIEW, bd_connection);
                                            SqliteDataReader reader = command.ExecuteReader();//считывание данных
                                            if (reader.HasRows) // если есть данные
                                            {
                                                while (reader.Read())   // построчно считываем данные
                                                {
                                                    Card one = new Card(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt16(4), reader.GetInt16(5));
                                                    if (one.getStatus() == 0)
                                                        Console.WriteLine(one.getId() + ": номер карты = " + one.getNumberCredit() + " , дата открытия : " + one.getTimeStart() + " , дата закрытия :" + one.getTimeEnd());
                                                }
                                            }
                                            Console.WriteLine("Введите айди карты :");
                                            menuPush = Console.ReadLine();
                                            Console.WriteLine("Введите номер карты");

                                            string updateClient = "UPDATE Cards SET status = 1, numberCredit = '" + Console.ReadLine()+"' where _id = " + menuPush + ";";
                                            Console.WriteLine("Ожидайте выдачу карты, спасибо");
                                            SqliteCommand command2 = new SqliteCommand(updateClient, bd_connection);
                                            command2.ExecuteNonQuery();
                                            Console.ReadLine();
                                        }
                                        break;

                                    case 2:
                                        {
                                            string cardVIEW = manager.ЗакрытьКарту();
                                            SqliteCommand command = new SqliteCommand(cardVIEW, bd_connection);
                                            SqliteDataReader reader = command.ExecuteReader();//считывание данных
                                            if (reader.HasRows) // если есть данные
                                            {
                                                while (reader.Read())   // построчно считываем данные
                                                {
                                                    Card one = new Card(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt16(4), reader.GetInt16(5));
                                                    if (one.getStatus() == 2)
                                                        Console.WriteLine(one.getId() + ": номер карты = " + one.getNumberCredit() + " , дата открытия : " + one.getTimeStart() + " , дата закрытия :" + one.getTimeEnd());
                                                }
                                            }
                                            Console.WriteLine("Введите айди карты :");
                                            menuPush = Console.ReadLine();
                                            if (menuPush != "")
                                            {
                                                string cardDEL = "DELETE FROM Cards where _id = " + menuPush + ";";
                                                SqliteCommand command2 = new SqliteCommand(cardDEL, bd_connection);
                                                command2.ExecuteNonQuery();
                                                Console.WriteLine("Информация по карте успешно удалена");
                                            }
                                            else Console.WriteLine("Отмена удаления карты");
                                            menuPush = "4";
                                            Console.ReadLine();
                                        }
                                        break;
                                }
                            }
                        }
                        break;

                    default: { break; }

                    case 3:
                        {
                            menuPush = null;
                            while (Convert.ToInt16(menuPush) != 4)
                            {
                                Console.Clear();
                                Console.WriteLine(strBlock);
                                Console.WriteLine("1 - Вывести информацию о клиенте по айди");//событие
                                Console.WriteLine("2 - Удалить клиента по айди");//событие
                                Console.WriteLine("3 - Вывести список всех клиентов");//событие
                                Console.WriteLine("4 - Назад");
                                Console.WriteLine(strBlock);
                                menuPush = Console.ReadLine();
                                switch (Convert.ToInt16(menuPush))
                                {

                                    case 1:
                                        {
                                            Console.WriteLine("Введите айди клиента");
                                            int id = Convert.ToInt32(Console.ReadLine());
                                            client = clientShow(id);
                                            cardInfo(client);
                                            Console.WriteLine(strBlock);
                                            creditInfo(client);
                                            Console.ReadLine();
                                        }
                                        break;

                                    case 2:
                                        {
                                            
                                            string id = manager.УдалитьКлиента();
                                            Console.WriteLine("Клиент " + id + " удален");
                                            string com1 = "DELETE FROM Cards where id_client = " + id;
                                            SqliteCommand command1 = new SqliteCommand(com1, bd_connection);

                                            command1.ExecuteNonQuery();
                                            string com12 = "DELETE FROM Credits where id_client = " + id;
                                            SqliteCommand command2 = new SqliteCommand(com12, bd_connection);

                                            command2.ExecuteNonQuery();

                                            string com123 = "DELETE FROM Cashbox where id_client = " + id;
                                            SqliteCommand command3 = new SqliteCommand(com123, bd_connection);


                                            command3.ExecuteNonQuery();

                                            string com1234 = "DELETE FROM Clients where _id = " + id;
                                            SqliteCommand command4 = new SqliteCommand(com1234, bd_connection);

                                            command4.ExecuteNonQuery();
                                            bd_connection.Open();
                                            
                                            Console.ReadLine();
                                        }
                                        break;

                                    case 3:
                                        {
                                            string clientList = manager.ВывестиСписокКлиентов();
                                            SqliteCommand command = new SqliteCommand(clientList, bd_connection);
                                            SqliteDataReader reader = command.ExecuteReader();//считывание данных
                                            if (reader.HasRows) // если есть данные
                                            {
                                                while (reader.Read())   // построчно считываем данные
                                                {
                                                    client = new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDouble(4));
                                                    client.show();

                                                }
                                            }
                                            else { Console.WriteLine("Клиентов нет"); }
                                            Console.ReadLine();
                                        }
                                        break;

                                    case 4:
                                        { menuPush = null; }
                                        break;
                                    default: break;
                                }
                            
                            }
                        }
                        break;
                    case 4: { } break;
                }
            }
        }

        public static void cashierInfo()
        {
            menuPush = null;
            while (Convert.ToInt16(menuPush) != 3)
            {
                Console.Clear(); 
                cashier.show();
                currencyInfo();
                Console.WriteLine(strBlock);
                Console.WriteLine("Операции с валютой");//событие
                Console.WriteLine("1 - Добавить валюту");//событие
                Console.WriteLine("2 - Удалить валюту");//событие
                Console.WriteLine("3 - Назад");
                Console.WriteLine(strBlock);
                menuPush = Console.ReadLine();
                switch (Convert.ToInt16(menuPush))
                {
                    case 1:
                        {
                            Console.Clear();
                            string currenciesAdd = cashier.ДобавитьВалюту();
                            SqliteCommand command = new SqliteCommand(currenciesAdd, bd_connection);
                            command.ExecuteNonQuery();
                        }
                        break;
                    case 2:
                        {
                            Console.WriteLine("Введите айди валюты :");
                            menuPush = Console.ReadLine();
                            foreach (Currencies one in currenciesS)
                            {
                                if ((one.getId() == Convert.ToInt16(menuPush)))
                                {

                                    string currenciesDel = cashier.УдалитьВалюту(menuPush);
                                    SqliteCommand command = new SqliteCommand(currenciesDel, bd_connection);
                                    command.ExecuteNonQuery();
                                 }
                            }
                        }
                        break;
                    case 3:
                        {
                            
                        }
                        break;
                  
                        break;
                    default: break;
                }
            }
        }

        //события
        static public void clientInfo()
        {
            menuPush = null;
            while (Convert.ToInt16(menuPush) != 4)
            {
                Console.Clear();
                client.show();
                Console.WriteLine("1 - Операции с кредитами");
                Console.WriteLine("2 - Операции с картами");
                Console.WriteLine("3 - Операции с валютами и наличкой");
                Console.WriteLine("4 - Выход из аккаунта");
                Console.WriteLine(strBlock);
                menuPush = Console.ReadLine();
                switch (Convert.ToInt16(menuPush))
                {
                    case 1:
                        {
                            menuPush = null;
                            while (Convert.ToInt16(menuPush) != 3)
                            {
                                Console.Clear();
                                Console.WriteLine(strBlock);
                                creditInfo(client);
                                Console.WriteLine("Операции с кредитом");//событие
                                Console.WriteLine("1 - Оставить заявку на кредит");//событие
                                Console.WriteLine("2 - Внести сумму на погашение");//событие
                                Console.WriteLine("3 - Назад");
                                Console.WriteLine(strBlock);
                                menuPush = Console.ReadLine();
                                switch (Convert.ToInt16(menuPush))
                                {
                                    case 1:
                                        {
                                            Credit credit = new Credit();
                                            string insertStr = (client.ЗапроситьКредит(CreditId, client.getId()));
                                            CreditId++; 
                                            client.getCash();
                                            SqliteCommand command = new SqliteCommand(insertStr, bd_connection);
                                            command.ExecuteNonQuery();
                                        }
                                        break;
                                    case 2:
                                        {
                                            Console.WriteLine("Введите айди кредита :");
                                            menuPush = Console.ReadLine();
                                            foreach (Credit one in credits)
                                            {
                                                if ((one.getId() == Convert.ToInt16(menuPush)) && (one.getId_client() == client.getId()))
                                                {
                                                    Console.WriteLine("Введите сумму");
                                                    menuPush = Console.ReadLine();

                                                    if (Convert.ToDouble(menuPush) <= client.getCash())
                                                    {
                                                        string insertStr = client.ПогаситьКредит(one, Convert.ToDouble(menuPush));
                                                        client.getCash();
                                                        SqliteCommand command = new SqliteCommand(insertStr, bd_connection);
                                                        command.ExecuteNonQuery();
                                                    }
                                                    else Console.WriteLine("Не хватает средств");

                                                    break;
                                                }
                                            }
                                            menuPush = "0";
                                        }
                                        break;
                                    case 3: { } break;
                                    default: break;
                                }

                            }
                        }
                        break;
                    case 2:
                        {
                            menuPush = null;
                            while (Convert.ToInt16(menuPush) != 3)
                            {
                                Console.Clear();
                                Console.WriteLine(strBlock);
                                cardInfo(client);
                                Console.WriteLine(strBlock);
                                Console.WriteLine("1 - Оформить карту");//событие
                                Console.WriteLine("2 - Закрыть карту");//событие
                                Console.WriteLine("3 - Назад");
                                Console.WriteLine(strBlock);
                                menuPush = Console.ReadLine();
                                switch (Convert.ToInt16(menuPush))
                                {
                                    case 1:
                                        {
                                            Card card = new Card();
                                            string insertStr = (client.ОформитьКарту(CardsId, client.getId()));
                                            CardsId++;
                                            SqliteCommand command = new SqliteCommand(insertStr, bd_connection);
                                            command.ExecuteNonQuery();
                                        }
                                        break;
                                    case 2:
                                        {
                                            Console.WriteLine("Введите айди карты : ");
                                            menuPush = Console.ReadLine();
                                            foreach (Card one in cards)
                                            {
                                                if ((one.getId() == Convert.ToInt16(menuPush)) && (one.getId_client() == client.getId()))
                                                {
                                                    string insertStr = client.ЗакрытьКарту(one);
                                                    SqliteCommand command = new SqliteCommand(insertStr, bd_connection);
                                                    command.ExecuteNonQuery();
                                                }
                                                else Console.WriteLine("Неверный номер карты, повторите позже");
                                                break;
                                            }
                                        }
                                        break;
                                    case 3:
                                        { }
                                        break;
                                    default: break;
                                }
                            }
                        }
                        break;
                    case 3:
                        {
                            menuPush = null;
                            while (Convert.ToInt16(menuPush) != 4)
                            {
                                Console.Clear();
                                Console.WriteLine(strBlock);
                                Console.WriteLine("Доступные валюты");
                                currencyInfo();
                                Console.WriteLine(strBlock);
                                cashboxShow();
                                Console.WriteLine(strBlock);
                                Console.WriteLine("Сумма = " + client.getCash());
                                Console.WriteLine(strBlock);
                                Console.WriteLine("1 - Купить валюту");//событие
                                Console.WriteLine("2 - Продать валюту");//событие
                                Console.WriteLine("3 - Внести наличку");
                                Console.WriteLine("4 - Назад");
                                Console.WriteLine(strBlock);

                                menuPush = Console.ReadLine();

                                switch (Convert.ToInt16(menuPush))
                                {
                                    case 1:
                                        {
                                            bool status = false;
                                            Console.WriteLine(strBlock);
                                            Console.WriteLine("Введите айди валюты : ");
                                            menuPush = Console.ReadLine();
                                            foreach(int one in CashboxList)
                                            {
                                                if (one == Convert.ToInt16(menuPush)) status = true;
                                            }
                                            foreach (Currencies one in currenciesS)
                                            {
                                                if (one.getId() == Convert.ToInt16(menuPush))
                                                {
                                                    Currencies valute = new Currencies();
                                                    string insertStr = (client.КупитьВалюту(one, client.getId(), CurrenciesId,status));
                                                    CurrenciesId++;
                                                    SqliteCommand command = new SqliteCommand(insertStr, bd_connection);
                                                    command.ExecuteNonQuery();
                                                    break;
                                                }
                                                
                                            }
                                        }
                                        break;
                                    case 2:
                                        {

                                            Console.WriteLine(strBlock);
                                            Console.WriteLine("Введите айди сделки : ");
                                            menuPush = Console.ReadLine();
                                            foreach (Currencies one in currenciesS)
                                            {
                                                if (one.getId() == Convert.ToInt16(menuPush))
                                                {
                                                    string insertStr = (client.ПродатьВалюту(client, one));
                                                    SqliteCommand command = new SqliteCommand(insertStr, bd_connection);
                                                    command.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                        break;
                                    case 3:
                                        {

                                            Console.WriteLine(strBlock);
                                            Console.WriteLine("Сколько налички хотите внести?");
                                            menuPush = Console.ReadLine();
                                            client.setCash(Convert.ToDouble(menuPush)+client.getCash());
                                            Console.WriteLine("Вы внесли наличные средства, спасибо за выбор нашего банка!");
                                            Console.ReadKey();
                                        }
                                        break;

                                    default:
                                        break;

                                }

                            }
                        }
                        break;
                    default: break;
                }
            }
        }

        static public Client clientShow(int id)
        {
            string findClient = "SELECT * FROM Clients WHERE _id = ";
            SqliteCommand command = new SqliteCommand(findClient + id, bd_connection);
            SqliteDataReader reader = command.ExecuteReader();//считывание данных
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read())   // построчно считываем данные
                {
                    client = new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDouble(4));

                    break;
                }
            }
            Console.WriteLine(strBlock);
            client.show();
            return client;
        }
        

    static public void cashboxShow()
        {
            string creditsInfo = "SELECT * FROM Cashbox where id_client = " + client.getId();
            SqliteCommand command = new SqliteCommand(creditsInfo, bd_connection);
            SqliteDataReader reader = command.ExecuteReader();//считывание данных
            if (reader.HasRows) // если есть данные

                while (reader.Read())
                {  // построчно считываем данные
                    CashboxList.Add(reader.GetInt32(0));
                    Console.WriteLine(reader.GetInt32(0) + " : " + reader.GetString(1) + "|1 : " + reader.GetDouble(2) + "| " + reader.GetDouble(3) + " " + reader.GetString(1) + "|Дата начала " + reader.GetString(5));
                }
            }

        static public void cashboxSellShow()
        {
            string creditsInfo = "SELECT * FROM Cashbox where Status = 1";
            SqliteCommand command = new SqliteCommand(creditsInfo, bd_connection);
            SqliteDataReader reader = command.ExecuteReader();//считывание данных
            if (reader.HasRows) // если есть данные

                while (reader.Read())   // построчно считываем данные

                    Console.WriteLine("Номер сделки : " + reader.GetInt32(0) + "|" + reader.GetString(1) + "|1 : " + reader.GetDouble(2) + "|Покупка = " + reader.GetDouble(3) + "|Продажа = " + reader.GetDouble(4) + "|Дата начала " + reader.GetString(8));
        }

        static public void currencyInfo()
        {
            currenciesS.Clear();
            string creditsInfo = "SELECT * FROM Currencies";
            SqliteCommand command = new SqliteCommand(creditsInfo, bd_connection);
            SqliteDataReader reader = command.ExecuteReader();//считывание данных
            currenciesS.Clear();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read())   // построчно считываем данные
                {
                    Currencies currencies = new Currencies(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2));
                    CurrenciesId = currencies.getId();
                    currenciesS.Add(currencies);
                }
                foreach (Currencies one in currenciesS)
                {

                    Console.WriteLine(one.getId() + ": " + one.getAbr() + "," + one.getCurrency() );
                }
            }
            else
                Console.WriteLine("Валют нет");
        }



        static public void creditView()
        {
            string creditViews = "SELECT * FROM Credits";
            SqliteCommand command = new SqliteCommand(creditViews, bd_connection);
            credits.Clear();
            SqliteDataReader reader = command.ExecuteReader();//считывание данных
            if (reader.HasRows)
            { // если есть данные
                while (reader.Read())
                { // построчно считываем данные
                    Credit one = new Credit(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDouble(4), reader.GetInt16(5), reader.GetString(6), reader.GetDouble(7), reader.GetInt32(8));
                    credits.Add(one);
                }
                Console.WriteLine("Заявки на открытие кредита : \n");
                foreach (Credit one in credits)
                {
                    if (one.getStatusCredit() == 0)
                    {
                    
                        Console.WriteLine(one.getId() + ": клиент " + one.getId_client() + ", кредит " + one.getNameCredit() + " , на сумму = " + one.getSumCredit() + " , дата подачи " + one.getTimeStart() + ", дата закрытия " + one.getTimeEnd() + ", под процент = " + one.getPercent());

                    }
                }
                Console.WriteLine(strBlock);
                Console.WriteLine("Заявки на закрытие кредита : \n");
                foreach (Credit one in credits)
                {
                    if (one.getStatusCredit() == 2)
                    {

                        Console.WriteLine(one.getId() + ": клиент " + one.getId_client() + ", кредит " + one.getNameCredit() + " , на сумму = " + one.getSumCredit() + " , дата подачи " + one.getTimeStart() + ", дата закрытия " + one.getTimeEnd() + ", под процент = " + one.getPercent());

                    }

                }
                Console.WriteLine(strBlock);
            }
            else
            {
                Console.WriteLine("Кредитов нет");
                return;
            }
        }

            //static public void creditOperation
            static public void creditInfo(Client client) {
            credits.Clear();
            string creditsInfo = "SELECT * FROM Credits where id_client = " + client.getId();
            SqliteCommand command = new SqliteCommand(creditsInfo, bd_connection);
            SqliteDataReader reader = command.ExecuteReader();//считывание данных
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read())   // построчно считываем данные
                {
                    Credit credit = new Credit(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDouble(4), reader.GetInt16(5), reader.GetString(6), reader.GetDouble(7), reader.GetInt32(8));
                    CreditId = credit.getId();
                    credits.Add(credit);
                }
                
                Console.WriteLine("Заявки на кредиты : \n");
                foreach (Credit one in credits)
                {
                    if (one.getStatusCredit()==0)
                    {
                        Console.WriteLine(one.getId() + ":"+one.getNameCredit() + " , на сумму = " + one.getSumCredit()+ " , дата подачи " + one.getTimeStart() + ", дата закрытия " + one.getTimeEnd() + ", под процент = " + one.getPercent());
                        
                    }
                    
                }
                Console.WriteLine(strBlock);
                Console.WriteLine("Текущие кредиты : \n");
                foreach (Credit one in credits)
                {
                    if (one.getStatusCredit()==1)
                    {
                        Console.WriteLine(one.getId() + ":" + one.getNameCredit() + " , на сумму = " + one.getSumCredit() + " , дата подачи " + one.getTimeStart() + ", дата закрытия " + one.getTimeEnd() + ", под процент = " + one.getPercent());
                    }
                    
                }
                Console.WriteLine(strBlock);
                Console.WriteLine("Выплаченные кредиты : \n");
                foreach (Credit one in credits)
                {
                    if (one.getStatusCredit() == 2)
                    {
                        Console.WriteLine(one.getId() + ":" + one.getNameCredit() + " , на сумму = " + one.getSumCredit() + " , дата подачи " + one.getTimeStart() + ", дата закрытия " + one.getTimeEnd() + ", под процент = " + one.getPercent());
                    }

                }
                Console.WriteLine(strBlock);
                return;
            }
            else
            {
                Console.WriteLine("Кредитов нет");
                return;
            }
        }

        static public void cardInfo(Client client)
        {
            cards.Clear();
            string creditsInfo = "SELECT * FROM Cards where id_client = " + client.getId();
            SqliteCommand command = new SqliteCommand(creditsInfo, bd_connection);
            SqliteDataReader reader = command.ExecuteReader();//считывание данных
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        Card card = new Card(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5));
                        CardsId = card.getId();
                        cards.Add(card);
                    }

                    Console.WriteLine("Заявки на карты : \n");
                    foreach (Card one in cards)
                    {
                        if (one.getStatus() == 0)
                        {
                            Console.WriteLine(one.getId() + ": номер карты = " + one.getNumberCredit() + " , дата открытия : " + one.getTimeStart() + " , дата закрытия :" + one.getTimeEnd());
                        }
                    }
                    Console.WriteLine(strBlock);
                    Console.WriteLine("Закрытые карты : \n");
                    foreach (Card one in cards)
                    {
                        if (one.getStatus() == 2)
                        {
                            Console.WriteLine(one.getId() + ": номер карты = " + one.getNumberCredit() + " , дата открытия : " + one.getTimeStart() + " , дата закрытия :" + one.getTimeEnd());
                        }
                    }
                    Console.WriteLine(strBlock);
                    Console.WriteLine("Текущие карты : \n");
                    foreach (Card one in cards)
                    {
                        if (one.getStatus() == 1)
                        {
                            Console.WriteLine(one.getId() + ": номер карты = " + one.getNumberCredit() + " , дата открытия : " + one.getTimeStart() + " , дата закрытия :" + one.getTimeEnd());
                        }
                    }
                }
                else Console.WriteLine("Карт нет");
            
        }

        static public void clientUpdate()
        {
            string clientSave = "UPDATE Clients set Priority = " + client.getPriority()+", cash = "+client.getCash()+" where _id = "+client.getId();
            SqliteCommand command = new SqliteCommand(clientSave, bd_connection);
            command.ExecuteNonQuery();
        }
    }
}
