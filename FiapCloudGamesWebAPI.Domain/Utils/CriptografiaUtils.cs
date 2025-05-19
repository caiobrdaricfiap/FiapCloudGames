using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGameWebAPI.Domain.Utils
{
    public  class CriptografiaUtils
    {

        #region Métodos


        //Gera um salt
        public static string GeraSalt(int lenght)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[lenght];
                rng.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }

        //Função para gerar o hash da senha
        public static string HashPassWord(string password, string salt)
        {

            using (var sha256 = SHA256.Create())
            {
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] passWordBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] combined = new byte[saltBytes.Length + passWordBytes.Length];

                Array.Copy(saltBytes, combined, saltBytes.Length);
                Array.Copy(passWordBytes, 0, combined, saltBytes.Length, passWordBytes.Length);

                byte[] hashBytes = sha256.ComputeHash(combined);

                return Convert.ToBase64String(hashBytes);

            }
        }


        public static String CriptografarSenha(string passWord)
        {


            //gerar um salt
            string salt = GeraSalt(12);

            //gera o Hash da senha
            string strCriptografada = HashPassWord(passWord, salt);

            return strCriptografada;
        }

        #endregion
    }
}
