using System;
using System.Net;
using System.Linq;
using Electrolyte.Helpers;

namespace Electrolyte.Extensions {
	public static class IPAddressExtensions {
		static readonly byte[] IPv6Wrapper = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF };
		public static IPAddress WrapToIPv6(this IPAddress address) {
			switch(address.AddressFamily) {
			case System.Net.Sockets.AddressFamily.InterNetworkV6:
				return address;
			case System.Net.Sockets.AddressFamily.InterNetwork:
				return new IPAddress(ArrayHelpers.ConcatArrays(IPv6Wrapper, address.GetAddressBytes()));
			default:
				throw new FormatException(String.Format("Unsupported address family {0}", address.AddressFamily));
			}
		}

		public static IPAddress UnwrapFromIPv6(this IPAddress address) {
			byte[] bytes = address.GetAddressBytes();

			switch(address.AddressFamily) {
			case System.Net.Sockets.AddressFamily.InterNetworkV6:
				if(bytes.Take(IPv6Wrapper.Length).SequenceEqual(IPv6Wrapper))
					return new IPAddress(ArrayHelpers.SubArray(bytes, IPv6Wrapper.Length));
				return address;
			case System.Net.Sockets.AddressFamily.InterNetwork:
				return address;
			default:
				throw new FormatException(String.Format("Unsupported address family {0}", address.AddressFamily));
			}
		}
	}
}

