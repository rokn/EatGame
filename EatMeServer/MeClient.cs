using System;
using System.Collections.Generic;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Networking;

namespace EatMeServer
{
	public class MeClient : Client
	{
		public delegate void FoodGenerateHandler(List<Vector2> food);

		public event FoodGenerateHandler FoodGenerateEvent;

		public MeClient(string hostIp, int port, string appName) : base(hostIp, port, appName)
		{
		}

		public override void Connect(NetOutgoingMessage outMsg)
		{
			NetPeer.Connect(HostIp, Port, outMsg);
		}

		public void Connect(string userName)
		{
			NetOutgoingMessage outMsg = NetPeer.CreateMessage();
			outMsg.Write(Packets.Connect);
			outMsg.Write(userName);
			Connect(outMsg);
		}

		protected override void ApproveConnection(NetIncomingMessage inc)
		{}

		protected override void HandleData(NetIncomingMessage inc)
		{
			switch(inc.ReadPacket())
			{
				case Packets.Approved:
					Console.WriteLine("Username: {0}", inc.ReadString());
					Connected = true;
					var food = ReadFoodInitialization(inc);
					OnFoodGenerateEvent(food);
					break;
			}
		}

		protected override void StatusChange(NetIncomingMessage inc)
		{}

		protected virtual void OnFoodGenerateEvent(List<Vector2> food)
		{
			FoodGenerateEvent?.Invoke(food);
		}

		private List<Vector2> ReadFoodInitialization(NetIncomingMessage inc)
		{
			var count = inc.ReadInt32();
			var food = new List<Vector2>(count);

			for (int i = 0; i < count; i++)
			{
				food.Add(inc.ReadVector());
			}

			return food;
		}
    }
}
