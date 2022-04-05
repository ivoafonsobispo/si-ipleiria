// ---- SERVER

// Start TcpListener
server = new TcpListener(listenEndPoint);
server.Start();
// Waits for a client connection (bloqueant wait)
client = server.AcceptTcpClient();
netStream = client.GetStream();


// ---- CLIENT

// Connects to Server ...
client = new TcpClient();
client.Connect(serverEndPoint);
netStream = client.GetStream();
