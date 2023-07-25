using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCredentialManager.Encryption.types
{
    internal class AesEncryption : CredentialEncryption<AesInfo>
    {
        private Aes aes;

        public AesEncryption() : base("AES")
        {
        }

        public override AesInfo Create()
        {
            aes = Aes.Create();
            return new AesInfo(Convert.ToBase64String(aes.Key), Convert.ToBase64String(aes.IV));
        }

        public override string Encrypt(List<Credential> credentials)
        {
            throw new NotImplementedException();
        }

        public override List<Credential> Decrypt(string key, string credentials)
        {
            throw new NotImplementedException();
        }

        public override void SetKey(string key)
        {
            throw new NotImplementedException();
        }
    }

    internal class AesInfo
    {
        public string Key { get; set; } = String.Empty;
        public string IV { get; set; } = String.Empty;

        public AesInfo(string key, string iv) {
            this.Key = key;
            this.IV = iv;
        }
    }
}
