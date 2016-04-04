using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Utility;

/// <summary>
/// Class for simplifying the console logging functions use.
/// </summary>
public class Console
{
    public static void Log( string data )
    {
        ConsoleInterface.ConsoleInstance.Log(data);
    }
    
    public static void Output( string data )
    {
        ConsoleInterface.ConsoleInstance.Output(data);
    }

    public static void OutputRaw( string data )
    {
        ConsoleInterface.ConsoleInstance.OutputRaw(data);
    }

    public static void LogError( string data )
    {
        ConsoleInterface.ConsoleInstance.Log("<color=red>" + data + "</color>");
        Debug.LogError(data);
    }
}

/// <summary>
/// This is the new console inteface, designed to replace the old GUI console.
/// </summary>
public class ConsoleInterface : MonoBehaviour {
    public Text mConsoleArea;
    public Text mCommandEntry;

    // The command parser.
    public CommandParser MainCommandParser;

    // Register the console instance so that we can easily use it within any of our scripts, providing it has been instantiated within our scripts.
    public static ConsoleInterface ConsoleInstance;

    /// <summary>
    /// Event that is called when the button is pressed to submit the command.
    /// </summary>
    public void SubmitCommand()
    {
        // Make sure we've got some text in our command before actually submitting.
        if (mCommandEntry.text.Length > 0)
        {            
            Output("<color=blue>Executing... " + mCommandEntry.text + "</color>");
            // Validate the command was found and executed..
            if(!MainCommandParser.ParseCommand(mCommandEntry.text))
            {
                Output("<color=red>Failed to find command</color>");
            }
        }

        mCommandEntry.text = "";
    }


    /// <summary>
    /// Log the specified data.
    /// </summary>
    /// <param name="data">Data.</param>
    public void Log(string data)
    {
        if (data == null) return;

        //float days = ( Time.time / ( 60 * 60 * 12 ));
        System.DateTime Timestamp = System.DateTime.Now;

        // Set up the data string.
        data = "<b>[" + Timestamp.ToString("HH:mm:ss") + "]</b> " + data + "\n";

        // Append the data to the console
        mConsoleArea.text += data;

        // Replace this in future with own logger.
        Debug.Log(data);
    }

    /// <summary>
    /// Output without timestamps, completely clean.
    /// </summary>
    /// <param name="data">Data.</param>
    public void Output(string data)
    {
        if (data == null) return;
        mConsoleArea.text += "> " + data + "\n";
    }
    

    public void OutputRaw(string data)
    {
        if (data == null) return;
        mConsoleArea.text += data + "\n";
    }

    /// <summary>
    /// Clear the console window of any data.
    /// </summary>
    public void Clear()
    {
        mConsoleArea.text = "";
    }

    /// <summary>
    /// Output the information to the console with a timestamp this will also be written to the logfile.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    public bool ConsoleEcho( string name, string arguments )
    {
        Log(arguments);
        return true;
    }

    /// <summary>
    /// When you write "clear" in the console the console will be emptied.
    /// A message will then be written to the console informing you that it has been cleared.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    public bool ConsoleClear(string name, string arguments)
    {
        Clear();
        Log("The console has been cleared.");
        return true;
    }

    /// <summary>
    /// Add a console command with help information associated with it.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="func"></param>
    /// <param name="helpInfo"></param>
    public void AddConsoleCommand( string command, CommandParser.CommandFunction func, string helpInfo )
    {
        // Create the parser command
        MainCommandParser.CreateCommand(command, func);
        // Add the help information to the help command. format: command - description\n.
        mHelpCommands.Add(command + " - " + helpInfo);
    }

    // Use this for initialization
    void Awake () {
        MainCommandParser = new CommandParser();
        mHelpCommands = new List<string>();
        // Register the standard console commands. These are used on the client and the server instance.
        AddConsoleCommand("echo", ConsoleEcho, "echo information to the console and write it to the game logfile.");
        AddConsoleCommand("clear", ConsoleClear, "clears the console information, keeps logfile.");
        AddConsoleCommand("help", ConsoleHelp, "displays this help information.");

        // Example data is present from the UI design, showing console content accurately in the game editor.
        Clear(); // Clear the example content from the console.

        // Update the static instance value to this console, meaning we don't have to have the original object to use this.
        ConsoleInstance = this;
	}


    /// <summary>
    /// Displays the help in the console when you type in "help".
    /// </summary>
    /// <param name="name"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    private bool ConsoleHelp(string name, string arguments)
    {
        // Display all the help information from the 'help' console command.
        foreach (string commandinfo in mHelpCommands) 
        {
            // Output the console command at this index.
            Console.Log(commandinfo);
        }

        // No errors which should shut down the game instance.
        return true;
    }

    /// <summary>
    /// All the help commands for the console as these are dynamically generated based upon if this is the client or server instance of the game.
    /// </summary>
    protected List<string> mHelpCommands;


    // Update is called once per frame
    void Update () {
	    if(Input.GetButtonDown("Submit"))
        {
            // Submit the command once.
            SubmitCommand();
        }
	}
}
