// convenience interface for transports which use a port.
// useful for cases where someone wants to 'just set the port' independent of
// which transport it is.
//
// note that not all transports have ports, but most do.

namespace  Mirror
{
    public interface IPortTransport
    {
        ushort Port { get; set; }
    }
}
