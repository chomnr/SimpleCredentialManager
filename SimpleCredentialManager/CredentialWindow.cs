using SimpleCredentialManager.Encryption.types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleCredentialManager
{
    internal class CredentialWindow
    {
        private string COMMAND_STARTER = ">";

        public enum WINDOW
        {
            NONE,
            MODIFY,
            CRED_ADD,
            CRED_UPDATE,
            CRED_SEARCH,
            CRED_DELETE
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

            for (;;)
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
                            if (LoadFileDialog())
                            {
                                Notify("Decrypted contents successfully.");
                                Thread.Sleep(1500);
                                ChangeWindow(WINDOW.MODIFY);
                            }
                            else
                            {
                                Notify("Failed to decrypt contents.");
                                Thread.Sleep(1500);
                                ChangeWindow(WINDOW.NONE);
                            }
                            break;
                    }
                }
            }

            if (RequestedWindow.Equals(WINDOW.MODIFY))
            {
                if (Int32.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 0:
                            var promptResult = observer.GetStore().GetGUI().CreateCreateCredentialPrompt();
                            //observer.GetStore().GetStore().Add(promptResult);
                            //Console.WriteLine(promptResult.Email);
                            observer.GetStore().AddItem(promptResult);
                            var encrypt = observer.GetEncryption().Encrypt(observer.GetStore().GetStore());
                            File.WriteAllText(observer.GetStore().path, Convert.ToBase64String(encrypt));
                            break;
                        case 1:
                             var list = observer.GetStore().GetStore();
                            if (list != null && list.Count != 0) {
                                for (int i = 0; i < list.Count; i++)
                                {
                                    Console.WriteLine("{");
                                    Console.WriteLine("     " + list[i].Username);
                                    Console.WriteLine("     " + list[i].Password);
                                    Console.WriteLine("     " + list[i].Email);
                                    Console.WriteLine("     " + list[i].Domain);
                                    Console.WriteLine("}");
                                }
                            }
                            break;
                    }
                }
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
        
        public bool LoadFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string key = null;
            string? store = null;

            string key_filter = "*.scrt|*.scrt";
            string store_filter = "*.scm|*.scm";

            if (key == null)
            {
                Notify("Waiting for .scrt...");
                openFileDialog.Filter = key_filter;
                openFileDialog.ShowDialog();
                key = File.ReadAllText(openFileDialog.FileName);
                observer.GetEncryption().Set(new AesInfo(key));
            }

            if (store == null && key != null)
            {
                Notify("Waiting for .scm...");
                openFileDialog.Filter = store_filter;
                openFileDialog.ShowDialog();
                observer.GetStore().SetPath(openFileDialog.FileName);
                try
                {
                    string contents = File.ReadAllText(openFileDialog.FileName);
                    if (!string.IsNullOrEmpty(contents))
                    {
                        store = observer.GetEncryption().Decrypt(File.ReadAllText(openFileDialog.FileName));
                        List<Credential> deserialize = JsonSerializer.Deserialize<List<Credential>>(store);
                        observer.GetStore().Load(deserialize);
                    } else
                    {
                        observer.GetStore().Load(new List<Credential>());
                    }
                } catch(Exception e)
                {
                    Notify(e.ToString());
                    Thread.Sleep(3000);
                    return false;
                }
            }
            return true;
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
              new CommandInstruction(1, CredentialWindow.WINDOW.MODIFY, "Read a credential (append 1 with an * to view all the data) ex: 1*."),
              new CommandInstruction(2, CredentialWindow.WINDOW.MODIFY, "Update a credential"),
              new CommandInstruction(3, CredentialWindow.WINDOW.MODIFY, "Delete a credential")
        };

        public  CommandInstruction[] GetInstructionsByWindow(CredentialWindow.WINDOW window)
        {
            return window.Equals(CredentialWindow.WINDOW.NONE) ? None : Modify;
        }
    };
}
