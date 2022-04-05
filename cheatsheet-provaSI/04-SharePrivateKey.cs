// CLIENT 	-- CLIENT -> SERVER

// Send key...
msg = protocol.Make(ProtocolSICmdType.SECRET_KEY, rsaServer.Encrypt(aes.Key, true));
netStream.Write(msg, 0, msg.Length);
Console.WriteLine("   Sent: " + ProtocolSI.ToHexString(aes.Key));

// Receive ack
Console.Write("waiting for ACK...");
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
Console.WriteLine("ok");


// Send iv...
msg = protocol.Make(ProtocolSICmdType.IV, rsaServer.Encrypt(aes.IV, true));
netStream.Write(msg, 0, msg.Length);
Console.WriteLine("   Sent: " + ProtocolSI.ToHexString(aes.IV));

// Receive ack
Console.Write("waiting for ACK...");
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
Console.WriteLine("ok");


// SERVER	-- CLIENT -> SERVER

// Receive key
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
aes.Key = rsaServer.Decrypt(protocol.GetData(), true);
Console.WriteLine("   Received: {0} ", ProtocolSI.ToHexString(aes.Key));

// Answer with a ACK
Console.Write("Sending a ACK... ");
msg = protocol.Make(ProtocolSICmdType.ACK);
netStream.Write(msg, 0, msg.Length);

// Receive iv
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
aes.IV = rsaServer.Decrypt(protocol.GetData(), true);
Console.WriteLine("   Received: {0} ", ProtocolSI.ToHexString(aes.IV));

// Answer with a ACK
Console.Write("Sending a ACK... ");
msg = protocol.Make(ProtocolSICmdType.ACK);
netStream.Write(msg, 0, msg.Length);


// SERVER	-- SERVER -> CLIENT

// Send key...
msg = protocol.Make(ProtocolSICmdType.SECRET_KEY, rsaClient.Encrypt(aes.Key, true));
netStream.Write(msg, 0, msg.Length);
Console.WriteLine("   Sent: " + ProtocolSI.ToHexString(aes.Key));

// Receive ack
Console.Write("waiting for ACK...");
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
Console.WriteLine("ok");


// Send iv...
msg = protocol.Make(ProtocolSICmdType.IV, rsaClient.Encrypt(aes.IV, true));
netStream.Write(msg, 0, msg.Length);
Console.WriteLine("   Sent: " + ProtocolSI.ToHexString(aes.IV));

// Receive ack
Console.Write("waiting for ACK...");
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
Console.WriteLine("ok");


// CLIENT 	-- SERVER -> CLIENT

// Receive key
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
aes.Key = rsaClient.Decrypt(protocol.GetData(), true);
Console.WriteLine("   Received: {0} ", ProtocolSI.ToHexString(aes.Key));

// Answer with a ACK
Console.Write("Sending a ACK... ");
msg = protocol.Make(ProtocolSICmdType.ACK);
netStream.Write(msg, 0, msg.Length);

// Receive iv
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
aes.IV = rsaClient.Decrypt(protocol.GetData(), true);
Console.WriteLine("   Received: {0} ", ProtocolSI.ToHexString(aes.IV));

// Answer with a ACK
Console.Write("Sending a ACK... ");
msg = protocol.Make(ProtocolSICmdType.ACK);
netStream.Write(msg, 0, msg.Length);