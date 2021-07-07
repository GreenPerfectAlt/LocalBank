
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Card
{

    public Card() { }

    public Card(int id, string numberCredit, string timeStart, string timeEnd, int status, int id_client)
    {
        this.id = id;
        this.numberCredit = numberCredit;
        this.timeStart = timeStart;
        this.timeEnd = timeEnd;
        this.status = status;
        this.id_client = id_client;
    }

    protected int id;

    protected string timeStart;

    protected string timeEnd;

    protected string numberCredit;

    protected int summary;

    protected int status = 0;

    public int id_client;

    public int getId_client()
    {
        return id_client;
    }

    public void setId_client(int value)
    {
        id_client = value;
    }
    /// <summary>
    /// @return
    /// </summary>
    /// 

    public int getStatus()
    {
        // TODO implement here
        return status;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setStatus(int value)
    {
        status = value;
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
        return timeEnd ;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setTimeEnd(string value)
    {
        timeEnd = value;
    }

    /// <summary>
    /// @return
    /// </summary>
    public string getNumberCredit()
    {
        // TODO implement here
        return numberCredit;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setNumberCredit(string value)
    {
        numberCredit = value;
    }

    /// <summary>
    /// @return
    /// </summary>
    public int getSummary()
    {
        // TODO implement here
        return summary;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setSummary(int value)
    {
        value = summary;
    }

}