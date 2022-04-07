// ----- Algoritmos simétricos e sem Cliente/Servidor

// ----- Cifrar
// Definir o algoritmo de acordo com o nome
SymmetricAlgorithm algorithm = null;
switch (algorithmName)
{
    case "AES":
        algorithm = new AesCryptoServiceProvider();
        break;
    default:
        algorithm = new TripleDESCryptoServiceProvider();
        break;
}

// Guardar a Key e o IV para a operação de decifragem 
this.key = algorithm.Key;
this.iv = algorithm.IV;

// Buscar o texto para cifrar
byte[] plainText = Encoding.UTF8.GetBytes(textBoxTextToEncrypt.Text);

// Precisamos de 2 streams (cryptoStream e memoryStream)
using (MemoryStream memoryStream = new MemoryStream())
{
    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
    {
        // Vamos assumir que a mensagem cabe no bloco do form -> sem ciclos
        // Cifrar a mensagem - escreve para o local onde a cryptoStream está a apontar
        cryptoStream.Write(plainText, 0, plainText.Length);
    }

    // Buscar o texto cifrado - shortcut
    byte[] cipherText = memoryStream.ToArray();
    
    // Apresentar o texto cifrado
    textboxEncryptedText.Text = Convert.ToBase64String(cipherText);
}


// ----- Decifrar
// Definir o algoritmo de acordo com o nome
SymmetricAlgorithm algorithm = null;
switch (algorithmName)
{
    case "AES":
        algorithm = new AesCryptoServiceProvider();
        break;
    default:
        algorithm = new TripleDESCryptoServiceProvider();
        break;
}

// Guardar a Key e o IV para a operação de decifragem 
this.key = algorithm.Key;
this.iv = algorithm.IV;

// Buscar o texto para decifrar
byte[] cipherText = Convert.FromBase64String(textboxEncryptedText.Text);

// Precisamos de 2 streams (cryptoStream e memoryStream)
using (MemoryStream memoryStream = new MemoryStream(cipherText))
{
    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
    {
        // Inicializar o espaço para guardar o plain text
        byte[] plainText = new byte[cipherText.Length];

        // A cryptoStream vai estar a apontar para o texto decifrado pelo Read
        int bytesRead = cryptoStream.Read(plainText, 0, plainText.Length);

        // Atribuir o tamanho adequado
        Array.Resize(ref plainText, bytesRead);

        // Apresentar o texto decifrado
        textBoxDecryptedText.Text = Encoding.UTF8.GetString(plainText);
    }
}