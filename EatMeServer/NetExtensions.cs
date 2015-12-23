using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace EatMeServer
{
	public static class NetExtensions
	{
		public static void Write(this NetOutgoingMessage outMsg, Packets packet)
		{
			outMsg.Write((byte)packet);
		}

		public static void Write(this NetOutgoingMessage outMsg, Vector2 vector)
		{
			outMsg.Write(vector.X);
			outMsg.Write(vector.Y);
		}

		public static Packets ReadPacket(this NetIncomingMessage incMsg)
		{
			return (Packets) incMsg.ReadByte();
		}

		public static Vector2 ReadVector(this NetIncomingMessage incMsg)
		{
			return new Vector2(incMsg.ReadFloat(),incMsg.ReadFloat());
		}
	}
}
