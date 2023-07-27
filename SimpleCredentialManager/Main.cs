// See https://aka.ms/new-console-template for more information
using SimpleCredentialManager;
using SimpleCredentialManager.Encryption.types;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        AesEncryption encryption = new AesEncryption();

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