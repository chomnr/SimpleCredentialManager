// See https://aka.ms/new-console-template for more information
using SimpleCredentialManager;
using SimpleCredentialManager.Encryption;
using SimpleCredentialManager.Encryption.types;
using System.Collections;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text.Json.Serialization;


int stage = 0;

AesEncryption encryption = new AesEncryption();
CredentialStore? store = null;

Console.WriteLine("  ______  ______  __    __    ");
Console.WriteLine(" /\\  ___\\/\\  ___\\/\\ \"-./  \\   ");
Console.WriteLine(" \\ \\___  \\ \\ \\___\\ \\ \\-./\\ \\  ");
Console.WriteLine("  \\/\\_____\\ \\_____\\ \\_\\ \\ \\_\\ ");
Console.WriteLine("   \\/_____/\\/_____/\\/_/  \\/_/ V.1.0.0 (" + encryption.getName() + ")");
Console.WriteLine("\n    SimpleCredentialManager");
Console.WriteLine("\n    [1] Create new key & store.");
Console.WriteLine("    [2] Import existing key & store.\n");

CredentialObserver observer = new CredentialObserver();
//observer.SetEncryption(encryption);


while (store == null)
{
    Console.Write("> ");
    string? readSelection = Console.ReadLine();

    if (Int32.TryParse(readSelection, out int choice))
    {
        if (choice == 1)
        {
            AesInfo test = encryption.Create();

            Console.WriteLine("Generating key.rsa & store.scm...");
            Console.WriteLine("key: " + test.Key);
            Console.WriteLine("iv: " + test.IV);

            /*
            System.Security.Cryptography.Aes test = System.Security.Cryptography.Aes.Create();
            test.GenerateKey();
            test.GenerateIV();
            Console.WriteLine("Generating key.rsa & store.scm...");
            Console.WriteLine("key: " + Convert.ToBase64String(test.Key) );
            Console.WriteLine("iv: " + Convert.ToBase64String(test.IV));
            */
            break;
        }

        if (choice == 2)
        {

        }
    }
}


//observer.AesBuildAndSave();
//observer.DecryptThenConvert();
//observer.EncryptThenConvert();



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

// Second screen 
// [1] Create new Credential
// [2] Delete Credential
// [3] Update Credential 
// [4] Search Credential
// Changes are automatically saved after the selected action is finished
