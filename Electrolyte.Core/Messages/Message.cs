using System;
using System.IO;

namespace Electrolyte.Messages {
	public abstract class Message<T> where T : Message<T>, new() {
		protected MessageHeader _header;
		public MessageHeader Header {
			get { return _header; }
			set {
				if(!CommandIsValid(value.Command))
					_header = value;
			}
		}

		public virtual string ExpectedCommand { get { return String.Empty; } }

		protected Message() { }

		public virtual bool CommandIsValid(string command) { return ExpectedCommand == command; }

		public static T Read(BinaryReader reader) {
			T t = new T();

			t.Header = new MessageHeader();
			t.Header.ReadPayload(reader);

			t.ReadPayload(reader);

			return t;
		}

		public void Write(BinaryWriter writer) {
			using(MemoryStream stream = new MemoryStream())
			using(BinaryWriter payloadWriter = new BinaryWriter(stream)) {
				WritePayload(payloadWriter);

				MessageHeader header = new MessageHeader(ExpectedCommand, stream.ToArray());

				header.WritePayload(writer);
				WritePayload(writer);
			}
		}

		protected abstract void ReadPayload(BinaryReader reader);

		public abstract void WritePayload(BinaryWriter writer); // TODO: Make this protected
	}
}

