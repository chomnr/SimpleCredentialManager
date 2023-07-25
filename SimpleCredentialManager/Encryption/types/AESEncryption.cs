using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCredentialManager.Encryption.types
{
    internal class AESEncryption : EncryptionType
    {
        public AESEncryption() : base("AES")
        {
        }

        protected override void Encrypt(List<Credential> credentials)
        {
            throw new NotImplementedException();
        }

        protected override void Decrypt(string key, List<Credential> credentials)
        {
            throw new NotImplementedException();
        }

        public override void SetKey(string keyt)
        {
            throw new NotImplementedException();
        }
    }
}
