
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class SqlHelper
    {
        public static bool IsSafeInput(string data)
        {

            if (string.IsNullOrWhiteSpace(data))
                return false; // Input is empty or contains only whitespace
        
            // Check for dangerous patterns or characters commonly used in SQL Injection
            string[] dangerousPatterns = new string[]
            {
                "--",         // SQL comment
                ";",          // Command terminator
                "'",          // Single quote
                "\"",         // Double quote
                "/*", "*/",   // Multi-line comment
                "xp_",        // Dangerous stored procedures
                "exec",       // Execute commands
                "select",     // SQL SELECT statements
                "insert",     // SQL INSERT statements
                "update",     // SQL UPDATE statements
                "delete",     // SQL DELETE statements
                "drop",       // Drop tables or databases
                "create",     // Create tables or databases
                "alter"       // Alter tables
            };
        
            // Convert input to lowercase for case-insensitive checks
            string lowerData = data.ToLower();
        
            // Check if any dangerous pattern exists in the input
            foreach (string pattern in dangerousPatterns)
            {
                if (lowerData.Contains(pattern))
                {
                    return false; // Input is unsafe
                }
            }
        
            // Ensure input contains only allowed characters (e.g., alphanumeric, underscores, spaces)
            string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_ ";
            foreach (char c in data)
            {
                if (!allowedCharacters.Contains(c.ToString()))
                {
                    return false; // Input contains disallowed characters
                }
            }
        
            return true; // Input is safe
        }
    }
}
