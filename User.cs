
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class User {

    public User() {
    }

    protected int id;

    protected string firstName;

    protected string secondName;
    public abstract void show();
    /// <summary>
    /// @return
    /// </summary>
    public int getId() {
        // TODO implement here
        return id;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setId(int value) {
        // TODO implement here
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setFirstName(string value) {
        firstName = value;
    }

    /// <summary>
    /// @return
    /// </summary>
    public string getFirstName() {
        // TODO implement here
        return firstName;
    }

    /// <summary>
    /// @param value
    /// </summary>
    public void setSecondName(string value) {
        secondName = value;
    }

    /// <summary>
    /// @return
    /// </summary>
    public string getSecondName() {
        // TODO implement here
        return secondName;
    }

}