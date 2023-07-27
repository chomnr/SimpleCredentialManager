using SimpleCredentialManager.Encryption.types;
using System.Text.Json;

namespace SimpleCredentialManager
{
    internal class CredentialWindow
    {
        private string COMMAND_STARTER = ">";

        public enum WINDOW
        {
            NONE,
            MODIFY,
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
                            observer.GetStore().SetPath(fileDialog[1] + ".scm");
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
                /*          CHOICES 
                     0 = Create Credential
                     1 = Read Credentials
                     2 = Update Credential
                     3 = Delete Credential
                 */
                if (Int32.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        Credential createPrompt = observer.GetStore().GetGUI().CreateCreateCredentialPrompt();
                        //observer.GetStore().GetStore().Add(promptResult);
                        //Console.WriteLine(promptResult.Email);
                        observer.GetStore().AddItem(createPrompt);
                        var encrypt = observer.GetEncryption().Encrypt(observer.GetStore().GetStore());
                        File.WriteAllText(observer.GetStore().path, Convert.ToBase64String(encrypt));
                    }

                    if (choice == 1)
                    {
                        var list = observer.GetStore().GetStore();
                        if (list != null && list.Count != 0)
                        {
                            for (int i = 0; i < list.Count; i++)
                            {
                                var serialize = JsonSerializer.Serialize(list[i]);
                                Console.WriteLine("ID: " + i + " " + serialize);
                            }
                        }
                    }

                    if (choice == 2) {
                        string[] updatePrompt = observer.GetStore().GetGUI().CreateUpdateCredentialPrompt();
                        if (updatePrompt == null)
                        {
                            Console.WriteLine("Invalid credential id");
                        } else
                        {
                            observer.GetStore().GetStore()[Int32.Parse(updatePrompt[0])].Username = updatePrompt[1];
                            observer.GetStore().GetStore()[Int32.Parse(updatePrompt[0])].Password = updatePrompt[2];
                            observer.GetStore().GetStore()[Int32.Parse(updatePrompt[0])].Email = updatePrompt[3];
                            observer.GetStore().GetStore()[Int32.Parse(updatePrompt[0])].Domain = updatePrompt[4];
                            var encrypt = observer.GetEncryption().Encrypt(observer.GetStore().GetStore());
                            File.WriteAllText(observer.GetStore().path, Convert.ToBase64String(encrypt));
                        }
                    }

                    if (choice == 3)
                    {
                        int deletePrompt = observer.GetStore().GetGUI().CreateDeleteCredentialPrompt();
                        if (deletePrompt != -1)
                        {
                            observer.GetStore().DeleteItem(deletePrompt);
                            var encrypt = observer.GetEncryption().Encrypt(observer.GetStore().GetStore());
                            File.WriteAllText(observer.GetStore().path, Convert.ToBase64String(encrypt));
                        }
                        else
                        {
                            Console.WriteLine("Invalid credential id.");
                        }
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
