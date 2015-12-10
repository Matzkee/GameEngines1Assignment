using UnityEngine;
using System.Collections;

public class Rule : MonoBehaviour {

    char a;
    string b;

    public Rule(char _a, string _b)
    {
        a = _a;
        b = _b;
    }

    public char GetA()
    {
        return a;
    }
    public string GetB()
    {
        return b;
    }
}
