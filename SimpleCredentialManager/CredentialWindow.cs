using SimpleCredentialManager.Encryption.types;
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

        private CredentialObserver observer { get; set; }
        private WINDOW RequestedWindow { get; set; }
        private bool IsHeaderThere { get; set; } = false;

        public CredentialWindow(CredentialObserver observer)
        {
            this.observer = observer;
        }

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
                /*          CHOICES 
                     0 = Create new key & store
                     1 = Load key & store
                 */
                if (Int32.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 0:
                            string[] fileDialog = CreateFileDialog();
                            AesInfo aesInfo = observer.GetEncryption().Create();
                            Helper.CreateKeyFile(fileDialog[0], aesInfo.Key, aesInfo.IV);
                            Helper.CreateStoreFile(fileDialog[1]);
                            observer.GetStore().Load(new List<Credential>());
                            Notify("Successful.");
                            Thread.Sleep(1500);
                            ChangeWindow(WINDOW.MODIFY);
                            break;
                        case 1:
                            Notify("Failed.");
                            Thread.Sleep(1500);
                            ChangeWindow(WINDOW.NONE);
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
            Console.WriteLine($"\n\n    {message}");
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

        public string[] CreateFileDialog()
        {
            WipeAndUpdateHeader();
            string keyName = "";
            string storeName = "";

            Console.Write("key file name: ");
            keyName = Console.ReadLine();

            Console.Write("store file name: ");
            storeName = Console.ReadLine();

            return new string[] { keyName, storeName };
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
