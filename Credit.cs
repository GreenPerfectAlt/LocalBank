
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Credit
{


    public Credit() { }

    public Credit(int id, string nameCredit, string timeStart, string timeEnd, double sumCredit, int statusCredit, string history, double percent, int id_client)
    {
        this.id = id;
        this.nameCredit = nameCredit;
        this.timeStart = timeStart;
        this.timeEnd = timeEnd;
        this.sumCredit = sumCredit;
        this.statusCredit = statusCredit;
        this.history = history;
        this.percent = percent;
        this.id_client = id_client;
    }

    protected int id;

    protected string nameCredit;

    protected string timeStart;

    protected string timeEnd;

    protected double sumCredit;

    protected int statusCredit;

    public string history;

    protected double percent;

    protected int id_client;


    public int getId_client()
    {
        return id_client;
    }
    public void setId_client(int value)
    {
        id_client = value;
    }

    public double getPercent()
    {
        return percent;
    }
    public void setPercent(double value)
    {
        percent = value;
    }

    public int getStatusCredit()
    {
        return statusCredit;
    }
    public void setStatusCredit(int value)
    {
        statusCredit = value; 
    }
  
    public int getId()
    {
        // TODO implement here
        return id;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setId(int value)
    {
        id = value;
    }

    /// <summary>
    /// @return
    /// </summary>
    public string getNameCredit()
    {
        // TODO implement here
        return nameCredit;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setNameCredit(string value)
    {
        nameCredit = value;
        // TODO implement here
    }

    /// <summary>
    /// @return
    /// </summary>
    public string getTimeStart()
    {
        // TODO implement here
        return timeStart;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setTimeStart(string value)
    {
        timeStart = value;
    }

    /// <summary>
    /// @return
    /// </summary>
    public string getTimeEnd()
    {
        // TODO implement here
        return timeEnd;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setTimeEnd(string value)
    {
        timeEnd = value;
        // TODO implement here
    }

    /// <summary>
    /// @return
    /// </summary>
    public double getSumCredit()
    {
        // TODO implement here
        return sumCredit;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setSumCredit(double value)
    {
        sumCredit = value;
        // TODO implement here
    }

  

}