using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Client : User
{
    protected int priority;
    protected double cash;

    public Client()
    {
    }

    private int _priority { get => priority; set => priority = value; }

    private double _cash { get => cash; set => cash = value; }




    /// <summary>
    /// @param string  
    /// @param string  
    /// @param DateTime  
    /// @return
    /// </summary>
    public Client(int _id, string _firstName, string _secondName, int _priority, double _cash)
    {
        // TODO implement here
        id = _id;
        firstName = _firstName;
        secondName = _secondName;
        priority = _priority;
        cash = _cash;
    }


    public override void show()
    {
        Console.WriteLine("Айди : " + id);
        Console.WriteLine("Имя : " + firstName);
        Console.WriteLine("Фамилия : " + secondName);
        Console.WriteLine("Приоритет : " + priority);
        Console.WriteLine("Сумма в местной валюте : " + cash);
        Console.WriteLine("\n================================");
    }


    public string ЗапроситьКредит(int CreditId,int ClientId)
    {
        string enter;
        Credit credit = new Credit();
        credit.setId((CreditId + 1));
        credit.setTimeStart(DateTime.Today.Day.ToString()+"."+ DateTime.Today.Month.ToString()+"."+ DateTime.Today.Year.ToString());
        credit.setStatusCredit(0);
        credit.setId_client(ClientId);
        Console.WriteLine("Заявка на кредит ");
        Console.WriteLine("Введите название кредита : ");
        enter = Console.ReadLine();
        credit.setNameCredit(enter);
        Console.WriteLine("Введите сумму кредита");
        enter = Console.ReadLine();
        credit.setSumCredit(Convert.ToInt32(enter));
        Console.WriteLine("Срок погашения (на кол-во месяцев) : ");
        enter = Console.ReadLine();
        credit.setTimeEnd(DateTime.Today.Day.ToString() + "." + Convert.ToString(DateTime.Today.Month + Convert.ToInt32(enter)) + "." + DateTime.Today.Year.ToString());
        credit.history = записатьВисторию(credit,0);
        return "INSERT INTO Credits values (" + credit.getId() + ",'" + credit.getNameCredit() + "','" + credit.getTimeStart()+ "','" + credit.getTimeEnd() + "'," + credit.getSumCredit() + "," + credit.getStatusCredit() + ",'" + credit.history + "'," + credit.getPercent() + "," + credit.getId_client() + ");";
    }

    public string записатьВисторию(Credit credit, double cash)
    {
        if (credit.getStatusCredit() == 0)
            return "\nЗапись от " + DateTime.Today.Day.ToString() + "." + DateTime.Today.Month.ToString() + "." + DateTime.Today.Year.ToString() + ", запрос на открытие кредита : * " + credit.getNameCredit() + "*, на сумму = " + credit.getSumCredit() + ", на срок " + credit.getTimeEnd();
        else if (credit.getStatusCredit() == 1)
            return "\nЗапись от " + DateTime.Today.Day.ToString() + "." + DateTime.Today.Month.ToString() + "." + DateTime.Today.Year.ToString() + ", внесена сумма = " + cash;
        else
                return "\nЗапись от " + DateTime.Today.Day.ToString() + "." + DateTime.Today.Month.ToString() + "." + DateTime.Today.Year.ToString() + ", кредит погашен, крайний срок = "+credit.getTimeEnd();
    }


    public string ПогаситьКредит(Credit credit, double _cash)
    {
        if (credit.getStatusCredit() == 1)
        {
            cash -= _cash;
            credit.setSumCredit((credit.getSumCredit() - _cash));
            if (credit.getSumCredit() <= 0)
            {
                cash += -(credit.getSumCredit());
                credit.setSumCredit(0);
                credit.setStatusCredit(2);
                credit.history += записатьВисторию(credit, _cash);
            }

            if (credit.getSumCredit() > 0)
            {

                credit.history += "\n"+ записатьВисторию(credit, _cash);

            }
        }
        else { return "Кредит ещё не одобрен, обратитесь в другой раз"; }
        return "UPDATE Credits SET status = "+credit.getStatusCredit()+" , history = '"+credit.history + "' , sumCredit ="+credit.getSumCredit()+" WHERE _id = "+credit.getId()+";";
    }

    public string ОформитьКарту(int CardId, int ClientId)
    {
        Card card = new Card();
        card.setId((CardId + 1));
        card.setTimeStart(DateTime.Today.Day.ToString() + "." + DateTime.Today.Month.ToString() + "." + DateTime.Today.Year.ToString());
        card.setStatus(0);
        card.setId_client(ClientId);
        Console.WriteLine("Заявка на приобретение карты отправлена, ожидайте");
        return "INSERT INTO Cards (_id,timeStart,status,id_client) values (" + card.getId() + ",'" + card.getTimeStart() + "'," + card.getStatus() + "," + card.getId_client() + ");";
    }

    public string ЗакрытьКарту(Card card)
    {
        card.setStatus(2);
        return "UPDATE Cards SET status = " + card.getStatus() + " WHERE _id = " + card.getId() + ";";
    }

    public string КупитьВалюту(Currencies currencies,int id_client,int CurrenciesId,bool status)
    {
        double enter = 0;
        while ((enter <= 0)||(cash-enter* currencies.getCurrency()<0))
        {
            Console.WriteLine("Сколько валюты хотите купить?");
            enter = Convert.ToDouble(Console.ReadLine());
        }
        cash -= enter * currencies.getCurrency();

        Console.WriteLine("Оплачено " + enter * currencies.getCurrency() + " руб");
        Console.WriteLine("Остаток " + cash + " руб");
        Console.WriteLine("Покупка выполнена, ожидайте чека");
        Console.ReadKey();
        if (status)

            return "UPDATE Cashbox SET Purchased = (Purchased + " + enter + ") WHERE _id = " + currencies.getId() + ";";
        else return "INSERT INTO Cashbox (_id,Abr,currency,Purchased,id_client,id_value,date) values  ("+ CurrenciesId+1 + ",'" + currencies.getAbr() + "','" + currencies.getCurrency() + "', " + enter + ","+ id_client + "," + currencies.getId() + ",'" + DateTime.Today.Day.ToString() + "." + DateTime.Today.Month.ToString() + "." + DateTime.Today.Year.ToString() + "');";
    }

    public string ПродатьВалюту(Client client, Currencies currencies)
    {
        double enter = 0;
        while (enter <= 0)
        {
            Console.WriteLine("Сколько хотите продать?");
            enter = Convert.ToInt32(Console.ReadLine());
        }
        client.cash += enter*currencies.getCurrency();
        return "UPDATE Cashbox SET Purchased = (Purchased - " + enter + ") WHERE _id = " + currencies.getId() + ";";
    }

    /// <summary>
    /// @return
    /// </summary>
    public int getPriority()
    {
        // TODO implement here
        return priority;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setPriority(int value)
    {
        priority = value;
    }

    /// <summary>
    /// @return
    /// </summary>
    public double getCash()
    {
        // TODO implement here
        return cash;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setCash(double value)
    {
        cash = value;
    }

}