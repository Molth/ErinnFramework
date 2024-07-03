A high-performance, thread-safe, customizable polling delay, multi-threaded (optional), garbage collection stress-free, and easy-to-use lightweight C# RPC framework.

Supports various protocols (LAN, public network): Udp, Tcp, WebSocket

Supports .NET (Asp.netcore)

Supports Unity

Supports communication between .NET console applications and Unity.

Thread-safe, interior events (connect, disconnect, receive) handling using polling mode, and the exterior does not need to be locked.

Uses hash code-based event listening (header command) obtained from data type names, eliminating the need to manually specify event codes.

    /// <summary>
    /// Network data packet
    /// </summary>
    public struct NetworkPacket
    {
        /// <summary>
        /// Command
        /// </summary>
        public uint Command;

        /// <summary>
        /// Value
        /// </summary>
        public byte[] Payload;
    }


Compatible with Mirror: https://github.com/MirrorNetworking/Mirror

Compatible with Netcode for GameObjects: https://github.com/Unity-Technologies/com.unity.netcode.gameobjects

Network layer uses MemoryPack: https://github.com/Cysharp/MemoryPack

Event listening uses XXHash.

Unity Example:

Unity has two libraries, Mirror and Netcode for GameObjects, both with Relay Transport (create a room, .NET server must be running first). There are two Tank example scenes in Assets/Scenes/Mirror.

First, compile the entire .NET solution, run the Relay method in App/Program.cs, choose a protocol. The Unity protocol chosen must match the .NET console.

Then open Unity.

Relay Transport: High-performance, simple relay transport for Mirror (Netcode for GameObjects). The original Unity Server does not connect directly to the Unity Client but relays through the .NET application, making it easy to implement lobbies and rooms. Convenient for deploying .NET servers to Alibaba Cloud, Tencent Cloud, rather than deploying Unity-packed servers, reducing server costs. Similar to Unity's official MLAPI Relay but simpler.

Players can create their own rooms, and all logic runs on the host. The .NET server only forwards data.

Common usage: Run TestMagicOnion in .NET first, then open Assets/Scenes/Rpc, run it. Press A to connect to the server, press B to send test data packets.

.NET Example:

In App/Program.cs, there are some usage examples.

First, define a struct

    [MemoryPackable]
    public partial struct Sb : INetworkMessage
    {
        public string Nt;

        public Sb(string nt) => Nt = nt;
    }

Then define the listening method

    [ServerCallback]
    private static void OnSb(uint id, Sb sb) => Console.WriteLine(id + " " + sb.Nt);

    [ClientCallback]
    private static void OnSb(Sb sb) => Console.WriteLine(sb.Nt);

Call to register listening methods

    server.RegisterHandlers(typeof(Program));
    client.RegisterHandlers(typeof(Program));

You can also use specific instances to register instance methods

    [ServerCallback]
    private void OnSb(uint id, Sb sb) => Console.WriteLine(id + " " + sb.Nt);

    [ClientCallback]
    private void OnSb(Sb sb) => Console.WriteLine(sb.Nt);

Register all methods with the specified attribute

    server.RegisterHandlers();
    client.RegisterHandlers();

Manually specify registering specific methods

    server.RegisterHandler<Sb>(OnSb);
    client.RegisterHandler<Sb>(OnSb);

Then when the specified information is received, the registered methods will be called.