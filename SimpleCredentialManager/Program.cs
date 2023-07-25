// See https://aka.ms/new-console-template for more information
using SimpleCredentialManager;
using SimpleCredentialManager.Encryption;
using SimpleCredentialManager.Encryption.types;
using System.Runtime.Intrinsics.X86;


EncryptionType aes = new AESEncryption();
CredentialStore store = new CredentialStore();

Console.WriteLine("  ______  ______  __    __    ");
Console.WriteLine(" /\\  ___\\/\\  ___\\/\\ \"-./  \\   ");
Console.WriteLine(" \\ \\___  \\ \\ \\___\\ \\ \\-./\\ \\  ");
Console.WriteLine("  \\/\\_____\\ \\_____\\ \\_\\ \\ \\_\\ ");
Console.WriteLine("   \\/_____/\\/_____/\\/_/  \\/_/ V.1.0.0 (" + aes.getName() + ")");
Console.WriteLine("\n    SimpleCredentialManager");
Console.WriteLine("\n    [1] Create new key & store.");
Console.WriteLine("    [2] Import existing key & store.");


// Credential Observer instance.
CredentialObserver observer = new CredentialObserver();
observer.SetEncryption(aes);
observer.SetStore(store);


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
