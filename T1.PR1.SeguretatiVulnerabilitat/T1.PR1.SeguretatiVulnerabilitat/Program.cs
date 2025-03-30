using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using T1.PR1.SeguretatiVulnerabilitat;
namespace Program
{
    public class Program()
    {
        private static RSAParameters publicKey;
        private static RSAParameters privateKey;
        public static void Main()
        {
            //Constantes
            const string 
                Menu = "Escull una opció:\n" +
                "1. Registre\n" +
                "2. Verificació de dades\n" +
                "3. Encriptació\n" +
                "4. Sortir",
                MsgNoRegistrered = "Encara no t'has registrat!",
                MsgNotValidOption = "Aquesta opció no es válida!",
                MsgEncript = "La teva encriptacia encriptada es: ";
            //variables
            bool menu = true;
            User user = new User (null, null);

            //claves
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);
            }


            //Menu
            while (menu)
            {
                Console.WriteLine(Menu);
                switch (Int32.Parse(Console.ReadLine()))
                {
                    case 1: user = Registre(); break;
                    case 2:
                    if (user.Password == null)
                    {
                        Console.WriteLine(MsgNoRegistrered);
                    }
                    else
                    {
                        Verification(user);
                    }
                    break;
                    case 3: EncryptAndDesen(publicKey, privateKey); break;
                    case 4: menu = false; break;
                    default: Console.WriteLine(MsgNotValidOption); break;
                }
            }
              
        }
        public static User Registre()
        {
            const string
                MsgName = "Escriu el teu nombre d'usuari: ",
                MsgPassword = "Escriu la teva contraseña: ";

            using var sha256 = SHA256.Create();

            Console.WriteLine(MsgName);
            string name = Console.ReadLine();
            Console.WriteLine(MsgPassword);
            var password = sha256.ComputeHash(Encoding.UTF8.GetBytes(Console.ReadLine()));
            Console.WriteLine(BitConverter.ToString(password).Replace("-", "").ToLower());
            return new User(name, BitConverter.ToString(password).Replace("-", "").ToLower());
        }
        public static void Verification(User user)
        {
            const string Msg = "Les dades són correctes";
            User newUser = Registre();
            if(user.Password == newUser.Password && user.Name == newUser.Name)
            {
                Console.WriteLine(Msg);
            }
        }

        public static void EncryptAndDesen(RSAParameters publicKey, RSAParameters privateKey)
        {
            const string MsgEncryp = "El text encriptat es: {0}",
                MsgDesen = "El text desencriptat es: {0}",
                Msg = "Escriu el teu missatge:";
            Console.WriteLine(Msg);
            string text = Console.ReadLine();
            Console.WriteLine(MsgEncryp, Convert.ToBase64String(Encriptation(text, publicKey)));
            Console.WriteLine(MsgDesen, Desencryptation(Encriptation(text, publicKey), privateKey));
        }
        public static byte[] Encriptation(string text, RSAParameters publicKey)
        {
            byte[] encryptedData;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(publicKey);
                byte[] dataToEncrypt = Encoding.UTF8.GetBytes(text);
                encryptedData = rsa.Encrypt(dataToEncrypt, false);
            }

            return encryptedData;
        }

        public static string Desencryptation(byte[] encryptedData, RSAParameters privateKey)
        {
            
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(privateKey);
                byte[] decry = rsa.Decrypt(encryptedData, false);
                return Encoding.UTF8.GetString(decry);
            }

        }
    }
}
