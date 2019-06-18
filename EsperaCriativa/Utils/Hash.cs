using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EsperaCriativa.Utils
{
    public class Hash
    {
        public static string GeraHash(string texto)
        {
            SHA256 sha256 = SHA256Managed.Create(); // recebe as classes necessárias
            byte[] bytes = Encoding.UTF8.GetBytes(texto); // converte em bytes
            byte[] hash = sha256.ComputeHash(bytes); // converte os bytes em hash
            StringBuilder result = new StringBuilder(); // instancia a classe para trabalhar separadamente com os caracteres
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X")); // adiciona alfanuméricos a senha
            }
            return result.ToString();
        }
    }
}