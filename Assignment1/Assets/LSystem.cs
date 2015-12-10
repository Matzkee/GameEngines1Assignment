﻿using UnityEngine;
using System.Text;

public class LSystem{

    Rule[] rules;
    string alphabet;
    int generation;

    // Constructor
    public LSystem(string _axiom, Rule[] ruleset)
    {
        rules = ruleset;
        generation = 0;

        alphabet = _axiom;
    }
    public string GetAlphabet()
    {
        return alphabet;
    }
    public int GetGeneration()
    {
        return generation;
    }
    // Class that generates the alphabet for the Lsystem
    public void Generate()
    {
        StringBuilder next = new StringBuilder();

        for (int i = 0; i < alphabet.Length; i++)
        {
            // String to append the alphabet
            string toReplace = "";
            // Get the next character from alphabet
            char current = alphabet[i];

            // Iterate through the rules and append the toReplace string
            // if match is not found, add the current character to string
            // at the end append the string
            for (int j = 0; j< rules.Length; j++)
            {
                char a = rules[j].GetA();
                if (a == current)
                {
                    toReplace = rules[j].GetB();
                }
                else
                {
                    toReplace += current;
                }
            }
            next.Append(toReplace);
        }
        // replace the old alphabet with a new one and increase generation number
        alphabet = next.ToString();
        generation++;

        Debug.Log("Current Generation: "+generation+" alphabet: "+alphabet);
    }
}
