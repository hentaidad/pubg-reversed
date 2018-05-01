using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace l00l
{
    public static class StringCipher
    {

        public static bool isDacia(int id)
        {
            bool a = false;

            switch (id)
            {
                case 104448:
                    a = true;
                    break;
                case 104451:
                    a = true;
                    break;
                case 104454:
                    a = true;
                    break;
                case 104457:
                    a = true;
                    break;
            }
            return a;
        }

        public static bool isUAZ(int id)
        {
            bool a = false;

            switch (id)
            {
                case 105573:
                    a = true;
                    break;
                case 105576:
                    a = true;
                    break;
                case 105579:
                    a = true;
                    break;
            }
            return a;
        }

        public static bool isBuggy(int id)
        {
            bool a = false;

            switch (id)
            {
                case 103929:
                    a = true;
                    break;
                case 103932:
                    a = true;
                    break;
                case 103935:
                    a = true;
                    break;
                case 103938:
                    a = true;
                    break;
                case 103943:
                    a = true;
                    break;
                case 103946:
                    a = true;
                    break;
            }
            return a;
        }

        public static bool isVan(int id)
        {
            bool a = false;

            switch (id)
            {
                case 104612:
                    a = true;
                    break;
                case 104615:
                    a = true;
                    break;
                case 104618:
                    a = true;
                    break;
            }
            return a;
        }

        public static bool isMotorbike(int id)
        {
            bool a = false;

            switch (id)
            {
                case 104824:
                    a = true;
                    break;
                case 104827:
                    a = true;
                    break;
                case 104830:
                    a = true;
                    break;
                case 104833:
                    a = true;
                    break;
                case 104836:
                    a = true;
                    break;
                case 104839:
                    a = true;
                    break;
                case 104842:
                    a = true;
                    break;
                case 104845:
                    a = true;
                    break;

            }
            return a;
        }

        public static bool isPickup(int id)
        {
            bool a = false;

            switch (id)
            {
                case 105340:
                    a = true;
                    break;
                case 105343:
                    a = true;
                    break;
                case 105346:
                    a = true;
                    break;
                case 105349:
                    a = true;
                    break;
                case 105352:
                    a = true;
                    break;
                case 105355:
                    a = true;
                    break;
                case 105358:
                    a = true;
                    break;
                case 105361:
                    a = true;
                    break;
                case 105364:
                    a = true;
                    break;
                case 105367:
                    a = true;
                    break;
                case 105370:
                    a = true;
                    break;
                case 105373:
                    a = true;
                    break;
                case 105469:
                    a = true;
                    break;
                case 105376:
                    a = true;
                    break;

            }
            return a;
        }




        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }


}