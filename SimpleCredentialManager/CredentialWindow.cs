using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCredentialManager
{
    internal class CredentialWindow
    {
        private string COMMAND_STARTER = ">";

        public enum WINDOW
        {
            NONE,
            MODIFY
        }

        private WINDOW RequestedWindow { get; set; }
        private bool IsHeaderThere { get; set; } = false;

        public void ChangeWindow(WINDOW windowTarget) {
            RequestedWindow = windowTarget;
            WipeAndUpdateHeader();
            UpdateWindow();
        }

        public void UpdateWindow() {
            WINDOW currentWindow = WINDOW.NONE;

            if (!currentWindow.Equals(RequestedWindow))
            {
                currentWindow = RequestedWindow;
            }

            for (; ; )
            {
                CreateWindow(currentWindow);
            }
        }

        public void CreateWindow(WINDOW currentWindow) {
            if (!IsHeaderThere)
            {
                Helper.PrintHeader();
                CommandInstruction.GetInstructions(currentWindow);
                IsHeaderThere = true;
            }

            AddCommandStarter();

            if (RequestedWindow == WINDOW.NONE)
            {
                if (Int32.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 0:
                            Notify("Successfully created this.");
                            Thread.Sleep(1500);
                            ChangeWindow(WINDOW.MODIFY);
                            break;
                    }
                }
            }

            if (RequestedWindow.Equals(WINDOW.MODIFY))
            {
                Console.ReadLine();
            }
        }

        public void Notify(string message) {
            Console.Clear();
            Console.WriteLine($"{message}");
        }

        public void WipeAndUpdateHeader()
        {
            Console.Clear();
            IsHeaderThere = false;
        }

        public void AddCommandStarter()
        {
            Console.Write(COMMAND_STARTER + " ");
        }
    }

    internal class CommandInstruction
    {
        public int CommandId;
        public CredentialWindow.WINDOW Window;
        public string Description;

        public CommandInstruction(int commandId, CredentialWindow.WINDOW window, string description)
        {
            this.CommandId = commandId;
            this.Window = window;
            this.Description = description;
        }

        public static void GetInstructions(CredentialWindow.WINDOW window)
        {
            AvailableInstructions avail = new AvailableInstructions();

            CommandInstruction[] instructions = avail.GetInstructionsByWindow(window);

            foreach (CommandInstruction instruction in instructions)
            {
                if (instruction.CommandId == 0)
                {
                    Console.WriteLine("\n" + FormatInstruction(instruction));
                }
                else
                {
                    Console.WriteLine(FormatInstruction(instruction));
                }
            }
            Console.WriteLine("");
        }
         
        private static string FormatInstruction(CommandInstruction instruction)
        {
            return "    [" + instruction.CommandId + "] " + instruction.Description;
        }
    }
    internal class AvailableInstructions
    {
        private CommandInstruction[] None = {
              new CommandInstruction(0, CredentialWindow.WINDOW.NONE, "Create new key & store"),
              new CommandInstruction(1, CredentialWindow.WINDOW.NONE, "Import existing key & store"),
        };

        private CommandInstruction[] Modify = {
              new CommandInstruction(0, CredentialWindow.WINDOW.MODIFY, "Create a new credential"),
              new CommandInstruction(1, CredentialWindow.WINDOW.MODIFY, "Read a credential"),
              new CommandInstruction(2, CredentialWindow.WINDOW.MODIFY, "Update a credential"),
              new CommandInstruction(3, CredentialWindow.WINDOW.MODIFY, "Delete a credential")
        };

        public  CommandInstruction[] GetInstructionsByWindow(CredentialWindow.WINDOW window)
        {
            return window.Equals(CredentialWindow.WINDOW.NONE) ? None : Modify;
        }
    };
}
