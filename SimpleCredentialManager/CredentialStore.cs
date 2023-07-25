using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCredentialManager
{
    internal class CredentialStore
    {

        private string entries { get; set; } = String.Empty;
            
        public string getEncryptedEntries() {
            return entries;
        }
        
        //import
        //export
        //create
    }
}
