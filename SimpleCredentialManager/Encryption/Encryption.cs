using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCredentialManager.Encryption
{
    abstract class EncryptionType
    {
        public string nameOfEncryption;

        public EncryptionType(string nameOfEncryption) { 
            this.nameOfEncryption = nameOfEncryption; 
        }

        protected abstract void Encrypt(List<Credential> credentials);

        protected abstract void Decrypt(string key, List<Credential> credentials);

        public abstract void SetKey(string key);

        public string getName()
        {
            return nameOfEncryption;
        }
    }
}
