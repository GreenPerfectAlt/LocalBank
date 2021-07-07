
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Cashier : User {

    public Cashier() {
    }
    public Cashier(int id, string fName, string sName)
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

    public void ПросмотрВалюты(List<Currencies> currenciesS)
    {
        foreach(Currencies one in currenciesS)
        {
            Console.WriteLine(one.getId()+":"+ ""+one.getAbr()+"|"+one.getCurrency()+" руб.");
        }
    }

  

    public string ДобавитьВалюту() {
        string abr,cur;
        Console.WriteLine("Введите аббревиатуру валюты в ISO 4217");
        abr = Console.ReadLine();
        Console.WriteLine("Введите курс валюты относительно рубля");
        cur = Console.ReadLine();
        return "INSERT INTO Currencies (Abr, Currency) values ('"+abr+"', "+cur+");";
    }

    public string УдалитьВалюту(string _id) {
        return "DELETE FROM Currencies where _id = " + _id + ";";
    }

}