using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Electrolyte.Messages;
using Electrolyte.Helpers;

namespace Electrolyte.Networking {
	public abstract class NetworkProtocol {
		public class NotConnectedException : InvalidOperationException {
			public NotConnectedException() { }
			public NotConnectedException(string message) : base(message) { }
			public NotConnectedException(string message, Exception inner) : base(message, inner) { }
		}

		public bool IsConnected { get; private set; }

		public NetworkProtocol NextProtocol {
			get {
				int nextIndex = Network.Protocols.LastIndexOf(this) + 1;
				if(nextIndex >= Network.Protocols.Count)
					throw new InvalidOperationException();
				NetworkProtocol next = Network.Protocols[nextIndex];
				if(!next.IsConnected)
					next.Connect();
				return next;
			}
		}

		public virtual void Connect() {
			IsConnected = true;
		}

		public virtual void Disconnect() {
			IsConnected = false;
		}
		
		public async virtual Task<Transaction> GetTransactionAsync(Transaction.Info info) {
			return await NextProtocol.GetTransactionAsync(info);
		}

		public async virtual Task<List<Task<Transaction>>> GetAddressHistoryListAsync(Address address, ulong startHeight = 0) {
			return await NextProtocol.GetAddressHistoryListAsync(address, startHeight);
		}

		public async Task<List<Transaction>> GetAddressHistoryAsync(Address address, ulong startHeight = 0) {
			List<Task<Transaction>> historyList = await GetAddressHistoryListAsync(address, startHeight);
			if(historyList.Count <= 0)
				return new List<Transaction>();

			return (await Task.WhenAll(historyList)).ToList();
		}

		public async Task<List<Transaction.Output>> GetUnspentOutputsAsync(Address address, ulong startHeight = 0) {
			List<Transaction> addressHistory = await GetAddressHistoryAsync(address, startHeight);
			var unspentOutputs = new Dictionary<Tuple<string, uint>, Transaction.Output>();

			// TODO: Merge `foreach` loops
			foreach(var tx in addressHistory) {
				foreach(var output in tx.Outputs) {
					if(output.Recipient == address)
						unspentOutputs.Add(Tuple.Create(output.Transaction.Hash, output.Index), output);
				}
			}

			foreach(var tx in addressHistory) {
				foreach(var input in tx.Inputs) {
					if(input.Sender == address) {
						// TODO: Provide some means of an input returning an equivalent (so we can use a `List` rather than a `Dictionary`
						if(unspentOutputs.ContainsKey(Tuple.Create(input.PrevTransactionHash, input.OutpointIndex)))
							unspentOutputs.Remove(Tuple.Create(input.PrevTransactionHash, input.OutpointIndex));
					}
				}
			}

			return unspentOutputs.Values.ToList();
		}

		public async virtual Task<Money> GetAddressBalanceAsync(Address address, ulong startHeight = 0) {
			return await NextProtocol.GetAddressBalanceAsync(address, startHeight);
		}



		public async virtual Task BroadcastTransactionAsync(Transaction tx) {
			await NextProtocol.BroadcastTransactionAsync(tx);
		}

		

		public virtual async Task<decimal> GetExchangeRateAsync(Money.CurrencyType c1, Money.CurrencyType c2) {
			return await NextProtocol.GetExchangeRateAsync(c1, c2);
		}
	}
}
