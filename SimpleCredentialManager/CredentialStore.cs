using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCredentialManager
{
    internal class CredentialStore
    {
        private List<Credential> credStore { get; set; }
        
        public void Load(List<Credential> store)
        {
            this.credStore = store;
        }

        public List<Credential>? GetStore() {
            return credStore;
        }

        public void AddItem(Credential credential) {
            if (StrictSearch(credential))
            {
                credStore.Add(credential);
            }
        }

        public void DeleteItem(Credential credential)
        {
            credStore.IndexOf(credential);
        }

        private bool StrictSearch(Credential credential) { 
            return credStore.Find(x => x.Equals(credential)) != null ? true : false;
        }
    }
}
