using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using SimpleCredentialManager.Encryption;

namespace SimpleCredentialManager
{
    internal class CredentialObserver
    {
        private CredentialStore? store = null;

        public CredentialObserver() {}

        public CredentialObserver(CredentialStore store) { 
            this.store = store;
        }

        public void SetStore(CredentialStore store)
        {
            this.store = store;
        }
    }
}
