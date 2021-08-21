using System;
using System.Collections.Generic;
using System.Text;

namespace type_to_win.Levels
{
    /// <summary>
    /// Generic logging class
    /// Allows for global logging functions to make it easy to implement on the fly where needed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Logger<T>
    {
        /// <summary>
        /// Logs the current class as well as the current method that the code is currently executing within
        /// This is useful for quickly logging when the game code has reached a specific state, useful for debugging
        /// </summary>
        /// <param name="logObject">object to log</param>
        /// <param name="method">method to log</param>
        public static void Log(T logObject, Action method)
        {
            Console.WriteLine(String.Format("Class: {0}\nMethod: {1}\n", logObject.GetType().ToString(), method.Method.Name));
        }
    }
}
