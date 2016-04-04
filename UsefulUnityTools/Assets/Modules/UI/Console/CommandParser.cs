using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Author: Gordon MacPherson
// Created: 28/04/2013

// License: 
// This work is licensed under the Creative Commons Attribution-ShareAlike 3.0 Unported License. 
// To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/3.0/.

namespace Utility
{
	//  _________                                           .___ __________                     .__                
	//  \_   ___ \  ____   _____   _____ _____    ____    __| _/ \______   \_____ _______  _____|__| ____    ____  
	//  /    \  \/ /  _ \ /     \ /     \\__  \  /    \  / __ |   |     ___/\__  \\_  __ \/  ___/  |/    \  / ___\ 
	//  \     \___(  <_> )  Y Y  \  Y Y  \/ __ \|   |  \/ /_/ |   |    |     / __ \|  | \/\___ \|  |   |  \/ /_/  >
	//   \______  /\____/|__|_|  /__|_|  (____  /___|  /\____ |   |____|    (____  /__|  /____  >__|___|  /\___  / 
	//          \/             \/      \/     \/     \/      \/                  \/           \/        \//_____/  
	
	public class CommandParser
	{
		public CommandParser()
		{
			
		}
		
		//  ________          _____.__       .__  __  .__                      
		//  \______ \   _____/ ____\__| ____ |__|/  |_|__| ____   ____   ______
		//   |    |  \_/ __ \   __\|  |/    \|  \   __\  |/  _ \ /    \ /  ___/
		//   |    `   \  ___/|  |  |  |   |  \  ||  | |  (  <_> )   |  \\___ \ 
		//  /_______  /\___  >__|  |__|___|  /__||__| |__|\____/|___|  /____  >
		//          \/     \/              \/                        \/     \/ 
		// Variables, Internal Types, Containers and Delegates.
		
		//
		// Container for a generic delegate command.
		// - Allows for easy string command to function interface.
		public delegate bool CommandFunction( string command, string arguments );
		
		// Struct so you can iterate through the list for a specific command.
		// I used this as opposed to KeyValuePair. Its more obvious whats going on and tider.
		
		protected class BaseCommand
		{
			public BaseCommand( string name, CommandFunction function )
			{
				m_name = name;
				m_commandFunction = function;
			}
			
			public string m_name;
			public CommandFunction m_commandFunction;
		}
		
		// Current commands
		private List<BaseCommand> commands = new List<BaseCommand>();
		
		
		//  _________                                           .___  ____ ___   __  .__.__  .__  __  .__               
		//  \_   ___ \  ____   _____   _____ _____    ____    __| _/ |    |   \_/  |_|__|  | |__|/  |_|__| ____   ______
		//  /    \  \/ /  _ \ /     \ /     \\__  \  /    \  / __ |  |    |   /\   __\  |  | |  \   __\  |/ __ \ /  ___/
		//  \     \___(  <_> )  Y Y  \  Y Y  \/ __ \|   |  \/ /_/ |  |    |  /  |  | |  |  |_|  ||  | |  \  ___/ \___ \ 
		//   \______  /\____/|__|_|  /__|_|  (____  /___|  /\____ |  |______/   |__| |__|____/__||__| |__|\___  >____  >
		//          \/             \/      \/     \/     \/      \/                                           \/     \/ 
		
		// Add a command by name and function.
		public bool CreateCommand( string name, CommandFunction function )
		{
			// Duplicate name checking.
			if( commands.Count > 0 )
			{
				// Check for duplicate names.
				foreach( BaseCommand Value in commands )
				{
					if( Value.m_name == name )
					{
						return false;
					}
				}
			}
			
			// No checking required.
			commands.Add( new BaseCommand( name, function ) );
			return true;
			
		}
		
		// Remove a command by name.
		// Returns true on success.
		public bool RemoveCommand( string name )
		{
			if( commands.Count > 0 )
			{
				// Find the command if it exists!
				foreach( BaseCommand Value in commands )
				{
					if( Value.m_name == name )
					{
						commands.Remove( Value );
						return true; // Found and has been removed.
					}
				}
			}
			
			return false; // Couldn't find it?
		}
		
		// Find a command by name.
		// Returns the command if it exists, null if it does not.
		public CommandFunction FindCommand( string name )
		{
			if( commands.Count > 0 )
			{
				// Find the command if it exists!
				foreach( BaseCommand Value in commands )
				{
					if( Value.m_name == name )
					{
						return Value.m_commandFunction;
					}
				}
			}
			
			return null;
		}
		
		//  ___________                            __  .__               
		//  \_   _____/__  ___ ____   ____  __ ___/  |_|__| ____   ____  
		//   |    __)_\  \/  // __ \_/ ___\|  |  \   __\  |/  _ \ /    \ 
		//   |        \>    <\  ___/\  \___|  |  /|  | |  (  <_> )   |  \
		//  /_______  /__/\_ \\___  >\___  >____/ |__| |__|\____/|___|  /
		//          \/      \/    \/     \/                           \/ 
		
		public bool ExecuteCommand( string name, string arguments )
		{
			// Check the string for command syntax
			if( commands.Count > 0 )
			{
				// Find the command if it exists!
				foreach( BaseCommand Value in commands )
				{
					if( Value.m_name == name )
					{
						return Value.m_commandFunction( name, arguments ); // Return the command state.
					}
				}
			}
			return false; // Return false because it didn't exist.
		}

        /// <summary>
        /// Parse the command supplied and execute it if found, automatically seperates arguments.
        /// </summary>
        /// <param name="fullstring"></param>
        /// <returns></returns>
        public bool ParseCommand( string fullstring )
        {
            // Command string
            string commandString = fullstring.Trim();

            // Match the command name.
            Match command = Regex.Match(commandString, @"^([^\s]+)");

            // If the match succeeded then continue.
            if (command.Success)
            {
                // Remove the command name from the command string.
                // So that this only contains the arguments.
                commandString = commandString.Remove(command.Index, command.Index + command.Length);                

                // Execute the specified command in the command parser.
                if (ExecuteCommand(command.Value, commandString))
                {
                    return true;
                }
            }

            return false;
        }
	}
}
