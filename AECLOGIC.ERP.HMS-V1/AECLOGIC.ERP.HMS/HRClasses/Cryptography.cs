using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections;

    public class CrypHelper
    {
        #region Fields

        private static string password ="cigolcea";
        private static string salt = "zaqxswcde";

        #endregion

        #region Public Methods

        public static string Encode( string plainText)
        {
            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);

            //Rfc2898DeriveBytes: Used to Generate Strong Keys
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password,  Encoding.ASCII.GetBytes(salt));//Non-English Alfhabets Will not Work on ASCII Encoding

            SymmetricAlgorithm Alg = Alg = new DESCryptoServiceProvider();

            Alg.Key = rfc.GetBytes(Alg.KeySize / 8);
            Alg.IV = rfc.GetBytes(Alg.BlockSize / 8);

            MemoryStream strCiphered = new MemoryStream();//To Store Encrypted Data

            CryptoStream strCrypto = new CryptoStream(strCiphered, Alg.CreateEncryptor(), CryptoStreamMode.Write);

            strCrypto.Write(plainBytes, 0, plainBytes.Length);
            strCrypto.Close();
            string textBoxCiphered = Convert.ToBase64String(strCiphered.ToArray());
            strCiphered.Close();
            return textBoxCiphered;
        }

        public static string Decode(string cipheredText)
        {
            byte[] cipheredBytes = Convert.FromBase64String(cipheredText.Replace(" ", "+"));
            
            //Rfc2898DeriveBytes: Used to Generate Strong Keys
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password,Encoding.ASCII.GetBytes(salt));//Non-English Alfhabets Will not Work on ASCII Encoding

            SymmetricAlgorithm Alg = new DESCryptoServiceProvider();

            Alg.Key = rfc.GetBytes(Alg.KeySize / 8);
            Alg.IV = rfc.GetBytes(Alg.BlockSize / 8);

            MemoryStream strDeciphered = new MemoryStream();//To Store Decrypted Data

            CryptoStream strCrypto = new CryptoStream(strDeciphered, Alg.CreateDecryptor(), CryptoStreamMode.Write);

            strCrypto.Write(cipheredBytes, 0, cipheredBytes.Length);
            strCrypto.Close();
            string textBoxCiphered = Encoding.ASCII.GetString(strDeciphered.ToArray());
            strDeciphered.Close();

            return textBoxCiphered;

        }

        #endregion

       

        public static string EncodeQueryString(string QueryString)
        {
            return "?" + CrypHelper.Encode(QueryString);
        }

        public static Hashtable DecodeQueryString(string Query)
        {
            Hashtable QueryString = new Hashtable();
            try
            {
                Query = CrypHelper.Decode(Query.Substring(1,Query.Length-1));
                String[] splitQueries = Query.Split(new char[] { '&' });
                String[] splitQuery = null;
                foreach (string sQuery in splitQueries)
                {
                    try
                    {
                        splitQuery = sQuery.Split(new char[] { '=' });
                        QueryString.Add(splitQuery[0], splitQuery[1]);
                    }
                    catch
                    { }
                }
            }
            catch
            {
            }
            return QueryString;
        }

    }

