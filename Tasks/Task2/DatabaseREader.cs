namespace Task2;

class DatabaseReader
{ 
    public int? numencValue = null;

    public bool? boolValue = true;
 
    public int? GetIntFromDatabase()
    {
        return numencValue;
    }
 
    public bool? GetBoolFromDatabase()
        { return boolValue; }
}