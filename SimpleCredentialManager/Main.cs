// See https://aka.ms/new-console-template for more information
using SimpleCredentialManager;
using SimpleCredentialManager.Encryption;
using SimpleCredentialManager.Encryption.types;
using System.Collections;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text.Json.Serialization;


class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        AesEncryption encryption = new AesEncryption();
        AesInfo? aesInfo = null;

        /* Credential */
        CredentialStore store = new CredentialStore();
        CredentialObserver observer = new CredentialObserver();
        observer.SetStore(store);
        observer.SetEncryption(encryption);
        CredentialWindow window = new CredentialWindow(observer);

        /* Creating Terminal GUI */
        window.CreateWindow(CredentialWindow.WINDOW.NONE);
        window.UpdateWindow();
    }
}


//Helper.PrintHeader(encryption.getName());




/*
Console.WriteLine("  ______  ______  __    __    ");
Console.WriteLine(" /\\  ___\\/\\  ___\\/\\ \"-./  \\   ");
Console.WriteLine(" \\ \\___  \\ \\ \\___\\ \\ \\-./\\ \\  ");
Console.WriteLine("  \\/\\_____\\ \\_____\\ \\_\\ \\ \\_\\ ");
Console.WriteLine("   \\/_____/\\/_____/\\/_/  \\/_/ V.1.0.0 (" + encryption.getName() + ")");
Console.WriteLine("\n    SimpleCredentialManager");
Console.WriteLine("\n    [1] Create new key & store");
Console.WriteLine("    [2] Import existing key & store\n");

CredentialObserver observer = new CredentialObserver();
observer.SetEncryption(encryption);

/*
while (store == null)
{
    Console.Write("> ");
    string? readSelection = Console.ReadLine();

    if (Int32.TryParse(readSelection, out int choice))
    {
        if (choice == 1)
        {
            aesInfo = encryption.Create();
            Console.WriteLine("\n    Created new key & store. Redirecting...");
            Console.Clear();
            store = new CredentialStore();
        }

        if (choice == 2)
        {

        }
    }
}

Console.WriteLine("  ______  ______  __    __    ");
Console.WriteLine(" /\\  ___\\/\\  ___\\/\\ \"-./  \\   ");
Console.WriteLine(" \\ \\___  \\ \\ \\___\\ \\ \\-./\\ \\  ");
Console.WriteLine("  \\/\\_____\\ \\_____\\ \\_\\ \\ \\_\\ ");
Console.WriteLine("   \\/_____/\\/_____/\\/_/  \\/_/ V.1.0.0 (" + encryption.getName() + ")");
Console.WriteLine("\n    SimpleCredentialManager");
Console.WriteLine("\n    [1] Create a new credential");
Console.WriteLine("    [2] Read a credential");
Console.WriteLine("    [3] Update a credential");
Console.WriteLine("    [4] Delete a credential");
while (store != null && aesInfo != null)
{
    //Console.WriteLine(aesInfo.full);
    //Console.WriteLine(aesInfo.Key);
    //Console.WriteLine(aesInfo.IV);
    Console.Write("> ");
    //string? username = Console.ReadLine();
    //var encrypted = encryption.Encrypt(new List<Credential>());
    //var decrypted = encryption.Decrypt(Convert.ToBase64String(encrypted));
    //Console.WriteLine(Convert.ToBase64String(encrypted));
    //Console.WriteLine(decrypted);
    break;
}

//observer.AesBuildAndSave();
//observer.DecryptThenConvert();
//observer.EncryptThenConvert();
*/



//CredentialStore store = new CredentialStore(); we call credential store AFTER the values are decrypted.



// The encryption we want to use.


//CredentialMediator test = new CredentialMediator();

//Credential credential = new Credential();

// create new key / storage
// import key & storage
// search functionality search for specific credential
// delete value
// values are in json format and then encrypted with aes ending with .scm (simple credential manager)

// First screen
// [1] = Create new key / storage (exports key after creation) & storage
// [2] = Import existing key / storage

// color highlighting filtration mode
// in order to filter by specific things u can do [u:hoar p:312 e:doggy@gmail.com]

// Second screen 
// [1] Create new Credential
// [2] Delete Credential
// [3] Update Credential 
// [4] Search Credential
// Changes are automatically saved after the selected action is finished
