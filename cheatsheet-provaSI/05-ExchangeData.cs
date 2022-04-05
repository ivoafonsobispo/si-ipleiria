// ---- CLIENT 	-- CLIENT -> SERVER

// Send data...
byte[] clearData = Encoding.UTF8.GetBytes("hello world!!!") OR BitConverter.GetBytes(accountID);
byte[] encryptedData = symmetricsSI.Encrypt(clearData);
msg = protocol.Make(ProtocolSICmdType.DATA, encryptedData);
netStream.Write(msg, 0, msg.Length);
Console.WriteLine("   Data: {0} = {1}", ProtocolSI.ToString(clearData), ProtocolSI.ToHexString(clearData));
Console.WriteLine("   Encrypted: {0}", ProtocolSI.ToHexString(encryptedData));

// Receive answer from server
Console.Write("waiting for ACK...");
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
Console.WriteLine("ok");


// ---- SERVER	-- CLIENT -> SERVER

// Receive the cipher
Console.Write("waiting for data...");
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
byte[] encryptedData = protocol.GetData();
byte[] data = symmetricsSI.Decrypt(encryptedData);
Console.WriteLine("   Encrypted: {0}", ProtocolSI.ToHexString(encryptedData));
Console.WriteLine("   Data: {0} = {1}", ProtocolSI.ToString(data), ProtocolSI.ToHexString(data));

// Answer with a ACK
Console.Write("Sending a ACK... ");
msg = protocol.Make(ProtocolSICmdType.ACK);
netStream.Write(msg, 0, msg.Length);


// ---- SERVER	-- SERVER -> CLIENT

// Send data...
byte[] clearData = Encoding.UTF8.GetBytes("hello world!!!") OR BitConverter.GetBytes(accountID);
byte[] encryptedData = symmetricsSI.Encrypt(clearData);
msg = protocol.Make(ProtocolSICmdType.DATA, encryptedData);
netStream.Write(msg, 0, msg.Length);
Console.WriteLine("   Data: {0} = {1}", ProtocolSI.ToString(clearData), ProtocolSI.ToHexString(clearData));
Console.WriteLine("   Encrypted: {0}", ProtocolSI.ToHexString(encryptedData));

// Receive answer from server
Console.Write("waiting for ACK...");
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
Console.WriteLine("ok");


// ---- CLIENT 	-- SERVER -> CLIENT

// Receive the cipher
Console.Write("waiting for data...");
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
byte[] encryptedData = protocol.GetData();
byte[] data = symmetricsSI.Decrypt(encryptedData);
Console.WriteLine("   Encrypted: {0}", ProtocolSI.ToHexString(encryptedData));
Console.WriteLine("   Data: {0} = {1}", ProtocolSI.ToString(data), ProtocolSI.ToHexString(data));

// Answer with a ACK
Console.Write("Sending a ACK... ");
msg = protocol.Make(ProtocolSICmdType.ACK);
netStream.Write(msg, 0, msg.Length);