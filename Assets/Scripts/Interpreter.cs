    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class Interpreter : MonoBehaviour
{
    public List<string> GenerateMovesList()
    {
        // Return list of movements 
        List<string> moves = new List<string>();

        var sr = new StringReader(ButtonsActions.inputText);

        string line="";

        while ((line = sr.ReadLine()) != null)
        {
            moves.Add(line);
        }

        return moves;
    }
}
