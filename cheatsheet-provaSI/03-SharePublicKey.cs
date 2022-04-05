// CLIENT 	-- CLIENT -> SERVER

// Send public key...
byte[] msg = protocol.Make(ProtocolSICmdType.PUBLIC_KEY, rsaClient.ToXmlString(false));
netStream.Write(msg, 0, msg.Length);
// Receive server public key
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
rsaServer.FromXmlString(protocol.GetStringFromData());

// SERVER	-- CLIENT -> SERVER

// Receive client public key
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
rsaClient.FromXmlString(protocol.GetStringFromData());
// Send public key...
byte[] msg = protocol.Make(ProtocolSICmdType.PUBLIC_KEY, rsaServer.ToXmlString(false));
netStream.Write(msg, 0, msg.Length);

// SERVER	-- SERVER -> CLIENT

// Send public key...
msg = protocol.Make(ProtocolSICmdType.PUBLIC_KEY, rsaServer.ToXmlString(false));
netStream.Write(msg, 0, msg.Length);
// Receive client public key
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
rsaClient.FromXmlString(protocol.GetStringFromData());

// CLIENT 	-- SERVER -> CLIENT

// Receive server public key
netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
rsaServer.FromXmlString(protocol.GetStringFromData());
// Send public key...
byte[] msg = protocol.Make(ProtocolSICmdType.PUBLIC_KEY, rsaClient.ToXmlString(false));
netStream.Write(msg, 0, msg.Length);