using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleCredentialManager.Encryption.types
{
    internal class AesEncryption : CredentialEncryption<AesInfo>
    {
        private Aes aes;
        private AesInfo aesInfo;


        public AesEncryption() : base("AES")
        {
        }

        public override AesInfo Create()
        {
            aes = Aes.Create();
            aesInfo = new AesInfo(Convert.ToBase64String(aes.Key), Convert.ToBase64String(aes.IV));
            return aesInfo;
        }

        public override byte[] Encrypt(List<Credential> credentials)
        {
            credentials.Add(new Credential("asddsasd", "dassad", "asddas@gmail.cokm", "doggy.com"));
            string json = JsonSerializer.Serialize(credentials);
            /*
             * https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-7.0 
             */
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encrypted;

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(json);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return encrypted;
        }

        public override string Decrypt(string credentials)
        {
            string plaintext = null;
            byte[] cipherText = Convert.FromBase64String(credentials);

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(aesInfo.Key) ;
                aesAlg.IV = Convert.FromBase64String(aesInfo.IV);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public override void SetKey(string key)
        {
            aesInfo.full = key;
        }
    }

    internal class AesInfo
    {
        public string Key { get; set; } = String.Empty;
        public string IV { get; set; } = String.Empty;
        public string full { get; set; } = String.Empty;

        public AesInfo(string key, string iv) {
            this.Key = key;
            this.IV = iv;
            this.full = iv + key;
        }
    }
}
