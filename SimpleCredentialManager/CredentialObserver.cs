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
        private EncryptionType type;
        private CredentialStore store;

        public CredentialObserver() {}

        public CredentialObserver(EncryptionType type, CredentialStore store) { 
            this.type = type;
            this.store = store;
        }

        public void SetEncryption(EncryptionType type) {
            this.type = type;
        }

        public void SetStore(CredentialStore store)
        {
            this.store = store;
        }
    }
}
