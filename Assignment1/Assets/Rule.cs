/*
    This class is sued to store any rules applied to an L-System
*/
public class Rule{

    char a;
    string b;

    // Constructor
    public Rule(char _a, string _b)
    {
        a = _a;
        b = _b;
    }

    // Getters & Setters
    public char GetA()
    {
        return a;
    }
    public string GetB()
    {
        return b;
    }
}
