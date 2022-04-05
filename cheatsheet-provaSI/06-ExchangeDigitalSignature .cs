// ---- CLIENT 	-- CLIENT -> SERVER

// Send data...
byte[] signature = rsaClient.SignData(encryptedData, sha512);
msg = protocol.Make(ProtocolSICmdType.DIGITAL_SIGNATURE, signature);
netStream.Write(msg, 0, msg.Length);
Console.WriteLine("   Data: {0}", ProtocolSI.ToHexString(signature));

// Receive answer from server
Console.Write("waiting for ACK / NACK...");
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
if (protocol.GetCmdType() == ProtocolSICmdType.ACK)
{
    Console.WriteLine("ok -- Valid Signature");
} else
{
    Console.WriteLine("NOT ok -- Invalid Signature");
}

// ---- SERVER	-- CLIENT -> SERVER

// Receive the cipher
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
byte[] signature = protocol.GetData();
Console.WriteLine("   Signature: {0}", ProtocolSI.ToHexString(signature));

if (rsaClient.VerifyData(encryptedData,sha512,signature))
{
    // Answer with a ACK
    Console.Write("Signature Valid -- Sending a ACK... ");
    msg = protocol.Make(ProtocolSICmdType.ACK);
} else
{
    // Answer with a NACK
    Console.Write("Signature Invalid -- Sending a NACK... ");
    msg = protocol.Make(ProtocolSICmdType.NACK);
}
netStream.Write(msg, 0, msg.Length);
Console.WriteLine("ok");

// ---- SERVER	-- SERVER -> CLIENT

// Send data...
byte[] signature = rsaServer.SignData(encryptedData, sha512);
msg = protocol.Make(ProtocolSICmdType.DIGITAL_SIGNATURE, signature);
netStream.Write(msg, 0, msg.Length);
Console.WriteLine("   Data: {0}", ProtocolSI.ToHexString(signature));

// Receive answer from server
Console.Write("waiting for ACK / NACK...");
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
if (protocol.GetCmdType() == ProtocolSICmdType.ACK)
{
    Console.WriteLine("ok -- Valid Signature");
} else
{
    Console.WriteLine("NOT ok -- Invalid Signature");
}

// ---- CLIENT 	-- SERVER -> CLIENT

// Receive the cipher
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
byte[] signature = protocol.GetData();
Console.WriteLine("   Signature: {0}", ProtocolSI.ToHexString(signature));

if (rsaServer.VerifyData(encryptedData,sha512,signature))
{
    // Answer with a ACK
    Console.Write("Signature Valid -- Sending a ACK... ");
    msg = protocol.Make(ProtocolSICmdType.ACK);
} else
{
    // Answer with a NACK
    Console.Write("Signature Invalid -- Sending a NACK... ");
    msg = protocol.Make(ProtocolSICmdType.NACK);
}
netStream.Write(msg, 0, msg.Length);
Console.WriteLine("ok");
