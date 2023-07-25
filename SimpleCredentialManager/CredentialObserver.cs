using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using SimpleCredentialManager.Encryption;
using SimpleCredentialManager.Encryption.types;

namespace SimpleCredentialManager
{
    internal class CredentialObserver
    {
        private CredentialEncryption<AesInfo>? encryption = null;
        private CredentialStore? store = null;

        public CredentialObserver() {}

        public CredentialObserver(CredentialStore store) { 
            this.store = store;
        }

        public void SetEncryption(CredentialEncryption<AesInfo> encryption) { 
            this.encryption = encryption; 
        }

        public void SetStore(CredentialStore store)
        {
            this.store = store;
        }
    }
}
