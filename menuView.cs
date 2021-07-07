using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalBank
{
    class menuView
    {
      public string menuPush = "0";
        public int menuLog0()
        {
            Console.Clear();
            while (Convert.ToInt16(menuPush) == 0 || Convert.ToInt16(menuPush) > 3 || Convert.ToInt16(menuPush) < 0)
            {
                Console.WriteLine("1 - Вход\n");
                Console.WriteLine("2 - Регистрация\n");
                Console.WriteLine("3 - Выход");
                Console.WriteLine("Введите значение");
                menuPush = Console.ReadLine();
            }
            if (Convert.ToInt16(menuPush) == 1) menuLog1();
            else if (Convert.ToInt16(menuPush) == 2) return 0;
            else return 0;
            Console.WriteLine("Выход");
            return 0;
        }
        public int menuLog1()
        {
            Console.Clear();
            menuPush = "0";
            while (Convert.ToInt16(menuPush) == 0 || Convert.ToInt16(menuPush) > 4 || Convert.ToInt16(menuPush) < 0)
            {
                Console.WriteLine("Вход в Онлайн систему обслуживания банка\n");
                Console.WriteLine("1 - Клиент \n");
                Console.WriteLine("2 - Менеджер \n");
                Console.WriteLine("3 - Кассир \n");
                Console.WriteLine("0 - Назад \n");
                menuPush = Console.ReadLine();
            }
            if (Convert.ToInt16(menuPush) == 0) return 0;
            else return 0;
        }


    }
}
