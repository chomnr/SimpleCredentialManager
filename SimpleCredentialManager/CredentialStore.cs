using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCredentialManager
{
    internal class CredentialStore
    {
        private List<Credential> credStore { get; set; }
        public string path { get; set; }
        
        public void Load(List<Credential> store)
        {
            this.credStore = store;
        }

        public List<Credential>? GetStore() {
            return credStore;
        }

        public void AddItem(Credential credential) {
            credStore.Add(credential);
        }

        public void DeleteItem(Credential credential)
        {
            credStore.IndexOf(credential);
        }

        public void SetPath(string path)
        {
            this.path = path;
        }

        public CredentialStoreGUI GetGUI()
        {
            return new CredentialStoreGUI(this); 
        }
    }

    internal class CredentialStoreGUI
    {
        private CredentialStore store { get; set; }

        public CredentialStoreGUI(CredentialStore store)
        {
            this.store = store;
        }

        public Credential CreateCreateCredentialPrompt()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Domain: ");
            string domain = Console.ReadLine();
            return new Credential(username, password, email, domain);
        }
    }
}
