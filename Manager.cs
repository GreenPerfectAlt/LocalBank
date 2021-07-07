
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Manager : User
{

    public Manager()
    {
    }

    public Manager(int id, string fName, string sName)
    {
        this.id = id;
        this.firstName = fName;
        this.secondName = sName;
    }

    public override void show()
    {
        Console.WriteLine("Айди : " + id);
        Console.WriteLine("Имя : " + firstName);
        Console.WriteLine("Фамилия : " + secondName);
        Console.WriteLine("\n================================");
    }



    public string ВыдатьКредит(Credit creditNEW)
    {
        Console.WriteLine("Введите процент : ");
        string percentEnter = Console.ReadLine();
         creditNEW.history += "\n" + (DateTime.Today.Day.ToString() + "." + DateTime.Today.Month.ToString() + "." + DateTime.Today.Year.ToString()) + ", кредит одобрен";
        return "\nUPDATE Credits SET status = 1 , percent = " + percentEnter + ", history = '" + creditNEW.history + "' where _id = " + creditNEW.getId();
    }

    public string ЗакрытьКредит(Client client, Credit creditDEl)
    {
        return "DELETE FROM Credits where _id = " + creditDEl.getId();
    }

    public string ВыдатьКарту()
    {
        return "SELECT * FROM Cards where status = 0";
    }

    public string ЗакрытьКарту()
    {
        return "SELECT * FROM Cards where status = 2";
    }

    public string ВывестиСписокКлиентов()
    {
        return "select * from clients";
    }

    public string УдалитьКлиента()
    {
        Console.WriteLine("Введите айди клиента");
         return Console.ReadLine();
    }

}