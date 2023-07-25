using SimpleCredentialManager.Encryption.types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCredentialManager.Encryption
{
    abstract class CredentialEncryption<T>
    {
        private string nameOfEncryption;

        public CredentialEncryption(string nameOfEncryption) { 
            this.nameOfEncryption = nameOfEncryption; 
        }

        public abstract T Create();

        public abstract string Encrypt(List<Credential> credentials);

        public abstract List<Credential> Decrypt(string key, string credentials);

        public abstract void SetKey(string key);

        public string getName()
        {
            return nameOfEncryption;
        }
    }
}
