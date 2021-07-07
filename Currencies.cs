
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Currencies {

    public Currencies() {
    }

    public Currencies(int id, string abr, double currency)
    {
        this.id = id;
        this.abr = abr;
        this.currency = currency;
    }

    protected int id;

    protected string abr;

    protected double currency;

    /// <summary>
    /// @return
    /// </summary>
    public double getCurrency()
    {
        // TODO implement here
        return currency;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setCurrency(double value)
    {
        currency = value;
    }

    /// <summary>
    /// @return
    /// </summary>
    public string getAbr()
    {
        // TODO implement here
        return abr;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setAbr(string value)
    {
        abr = value;
    }

    /// <summary>
    /// @return
    /// </summary>
    public int getId()
    {
        return id;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setId(int value)
    {
        id = value;
    }

}