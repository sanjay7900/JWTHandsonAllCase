using System.Security.Cryptography;
using System.Text;

namespace JWTHandsonAllCase.Common
{
    public static class EncryptionDecryption
    {

        public static string Encrypt(this string plainText, string key)
        {

            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                Array.Resize(ref keyBytes, 32);

                byte[] initializationVector = new byte[16];
               
                using (Aes aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = initializationVector;
                    var symmetricEncryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream as Stream, symmetricEncryptor, CryptoStreamMode.Write))
                        {
                            using (var streamWriter = new StreamWriter(cryptoStream as Stream))
                            {
                                streamWriter.Write(plainText);
                            }
                            return Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                }

            }
            catch(Exception ex)
            {

                throw new Exception("Unable encrypt ");
            }


            
        }

        public static string Decrypt(this string cipherText, string key)
        {
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                Array.Resize(ref keyBytes, 32);
                byte[] initializationVector = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);
                using (Aes aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = initializationVector;
                    var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (var memoryStream = new MemoryStream(buffer))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream as Stream,decryptor, CryptoStreamMode.Read))
                        {
                            using (var streamReader = new StreamReader(cryptoStream as Stream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Unable to decrypt");
            }
        }
        public static string EncryptAsync(this string plainText, string key)
        {

            try
            {
                byte[] initializationVector = Encoding.ASCII.GetBytes(key);
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = initializationVector;
                    var symmetricEncryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream as Stream, symmetricEncryptor, CryptoStreamMode.Write))
                        {
                            using (var streamWriter = new StreamWriter(cryptoStream as Stream))
                            {
                                streamWriter.Write(plainText);
                            }
                            return Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Unable decrypt ");
            }



        }

        public static string DecryptAsync(this string cipherText, string key)
        {
            try
            {
                byte[] initializationVector = Encoding.ASCII.GetBytes(key);
                byte[] buffer = Convert.FromBase64String(cipherText);
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = initializationVector;
                    var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (var memoryStream = new MemoryStream(buffer))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream as Stream, decryptor, CryptoStreamMode.Read))
                        {
                            using (var streamReader = new StreamReader(cryptoStream as Stream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                throw new Exception("Unable to decrypt");
            }
        }

    }

}
