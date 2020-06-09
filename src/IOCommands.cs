using System;
using System.IO;
using System.Collections.Generic;

namespace zoom {
    public static class IOCommands {
        // adopted from: https://stackoverflow.com/questions/1655318/how-to-set-default-input-value-in-net-console-app
        public static string ReadInputWithDefault(string promptText, string defaultValue) {
            List<char> buffer = new List<char>(defaultValue.ToCharArray());
            Console.Write(promptText);
            Console.Write(buffer.ToArray());
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter) {
                switch (keyInfo.Key) {
                    case ConsoleKey.LeftArrow:
                        if (Console.CursorLeft > promptText.Length)
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        break;

                    case ConsoleKey.RightArrow:
                        if (Console.CursorLeft < promptText.Length + buffer.Count)
                            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                        break;

                    case ConsoleKey.Backspace:
                        if (Console.CursorLeft <= promptText.Length) {
                            break;
                        }

                        buffer.RemoveAt(Console.CursorLeft - promptText.Length - 1);
                        rewriteLine(promptText, buffer);
                        break;

                    default:
                        var character = keyInfo.KeyChar;
                        if (character < 32) // not a printable chars
                            break;

                        if (Console.CursorLeft + 1 > Console.WindowWidth || buffer.Count >= Console.WindowWidth - 1) {
                            break; // currently only one line of input is supported
                        }

                        buffer.Insert(Console.CursorLeft - promptText.Length, character);
                        rewriteLine(promptText, buffer);
                        break;
                }

                keyInfo = Console.ReadKey(true);
            }

            Console.Write(Environment.NewLine);
            return new string(buffer.ToArray());
        }

        // adopted from: https://stackoverflow.com/questions/1655318/how-to-set-default-input-value-in-net-console-app
        private static void rewriteLine(string promptText, List<char> buffer) {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(promptText);
            Console.Write(buffer.ToArray());
        }
    }
}