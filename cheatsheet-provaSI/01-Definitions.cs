    // ---- SERVER

byte[] msg;
IPEndPoint listenEndPoint;
TcpListener server = null;
TcpClient client = null;
NetworkStream netStream = null;
ProtocolSI protocol = null;
AesCryptoServiceProvider aes = null;
SymmetricsSI symmetricsSI = null;
RSACryptoServiceProvider rsaClient = null;
RSACryptoServiceProvider rsaServer = null;
SHA512CryptoServiceProvider sha512 = null;

// Assimetric Algorithm
rsaClient = new RSACryptoServiceProvider();
rsaServer = new RSACryptoServiceProvider();
// Assimetric Algorithm
aes = new AesCryptoServiceProvider();
symmetricsSI = new SymmetricsSI(aes);
// Binding IP/port
listenEndPoint = new IPEndPoint(IPAddress.Any, 13000);
// Client/Server Protocol to SI
protocol = new ProtocolSI();
// Hash Algorithm
sha512 = new SHA512CryptoServiceProvider();

try 
{

}
catch (Exception ex)
{
    Console.WriteLine(SEPARATOR);
    Console.WriteLine("Exception: {0}", ex.ToString());
}
finally
{
    // Close connections
    if (netStream != null)
        netStream.Dispose();
    if (client != null)
        client.Close();
    if (server != null)
        server.Stop();
    if (sha512 != null)
        sha512.Dispose();
    if (rsaClient != null)
        rsaClient.Dispose();
    if (rsaServer != null)
        rsaServer.Dispose();
    Console.WriteLine(SEPARATOR);
    Console.WriteLine("Connection with client was closed.");
}

Console.WriteLine(SEPARATOR);
Console.Write("End: Press a key...");
Console.ReadKey();


// ---- CLIENT

byte[] msg;
IPEndPoint serverEndPoint;
TcpClient client = null;
NetworkStream netStream = null;
ProtocolSI protocol = null;
AesCryptoServiceProvider aes = null;
SymmetricsSI symmetricsSI = null;
RSACryptoServiceProvider rsaClient = null;
RSACryptoServiceProvider rsaServer = null;
SHA512CryptoServiceProvider sha512 = null;

// Assimetric Algorithm
rsaClient = new RSACryptoServiceProvider();
rsaServer = new RSACryptoServiceProvider();
// Assimetric Algorithm
aes = new AesCryptoServiceProvider();
symmetricsSI = new SymmetricsSI(aes);
// Client/Server Protocol to SI
protocol = new ProtocolSI();
// Defenitions for TcpClient: IP:port (127.0.0.1:13000)
serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 13000);
// Hash Algorithm
sha512 = new SHA512CryptoServiceProvider();

try 
{

}
catch (Exception ex)
{
    Console.WriteLine(SEPARATOR);
    Console.WriteLine("Exception: {0}", ex.ToString());
}
finally
{
    // Close connections
    if (netStream != null)
        netStream.Dispose();
    if (client != null)
        client.Close();
    Console.WriteLine(SEPARATOR);
    Console.WriteLine("Connection with server was closed.");

    if (sha512 != null)
        sha512.Dispose();
    if (rsaClient != null)
        rsaClient.Dispose();
    if (rsaServer != null)
        rsaServer.Dispose();
}

Console.WriteLine(SEPARATOR);
Console.Write("End: Press a key...");
Console.ReadKey();