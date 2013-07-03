using System;
using System.IO;
using NUnit.Framework;
using Electrolyte.Messages;

namespace Electrolyte.Test.Messages {
	[TestFixture]
	public class TransactionTest {
		Tuple<byte[], uint, Tuple<uint>[], Tuple<ulong>[], ulong, ulong, string>[] Transactions = new Tuple<byte[], uint, Tuple<uint>[], Tuple<ulong>[], ulong, ulong, string>[] {
			Tuple.Create(
				new byte[] {
				0xF9, 0xBE, 0xB4, 0xD9,
				0x74, 0x78, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x02, 0x01, 0x00, 0x00,
				0xE2, 0x93, 0xCD, 0xBE,

				0x01, 0x00, 0x00, 0x00,

				0x01,

				0x6D, 0xBD, 0xDB, 0x08, 0x5B, 0x1D, 0x8A, 0xF7, 0x51, 0x84, 0xF0, 0xBC, 0x01, 0xFA, 0xD5, 0x8D,
				0x12, 0x66, 0xE9, 0xB6, 0x3B, 0x50, 0x88, 0x19, 0x90, 0xE4, 0xB4, 0x0D, 0x6A, 0xEE, 0x36, 0x29,
				0x00, 0x00, 0x00, 0x00,

				0x8B,

				0x48, 0x30, 0x45, 0x02, 0x21, 0x00, 0xF3, 0x58, 0x1E, 0x19, 0x72, 0xAE, 0x8A, 0xC7, 0xC7, 0x36,
				0x7A, 0x7A, 0x25, 0x3B, 0xC1, 0x13, 0x52, 0x23, 0xAD, 0xB9, 0xA4, 0x68, 0xBB, 0x3A, 0x59, 0x23,
				0x3F, 0x45, 0xBC, 0x57, 0x83, 0x80, 0x02, 0x20, 0x59, 0xAF, 0x01, 0xCA, 0x17, 0xD0, 0x0E, 0x41,
				0x83, 0x7A, 0x1D, 0x58, 0xE9, 0x7A, 0xA3, 0x1B, 0xAE, 0x58, 0x4E, 0xDE, 0xC2, 0x8D, 0x35, 0xBD,
				0x96, 0x92, 0x36, 0x90, 0x91, 0x3B, 0xAE, 0x9A, 0x01, 0x41, 0x04, 0x9C, 0x02, 0xBF, 0xC9, 0x7E,
				0xF2, 0x36, 0xCE, 0x6D, 0x8F, 0xE5, 0xD9, 0x40, 0x13, 0xC7, 0x21, 0xE9, 0x15, 0x98, 0x2A, 0xCD,
				0x2B, 0x12, 0xB6, 0x5D, 0x9B, 0x7D, 0x59, 0xE2, 0x0A, 0x84, 0x20, 0x05, 0xF8, 0xFC, 0x4E, 0x02,
				0x53, 0x2E, 0x87, 0x3D, 0x37, 0xB9, 0x6F, 0x09, 0xD6, 0xD4, 0x51, 0x1A, 0xDA, 0x8F, 0x14, 0x04,
				0x2F, 0x46, 0x61, 0x4A, 0x4C, 0x70, 0xC0, 0xF1, 0x4B, 0xEF, 0xF5,

				0xFF, 0xFF, 0xFF, 0xFF,

				0x02,

				0x40, 0x4B, 0x4C, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x19,

				0x76, 0xA9, 0x14, 0x1A, 0xA0, 0xCD, 0x1C, 0xBE, 0xA6, 0xE7, 0x45, 0x8A, 0x7A, 0xBA, 0xD5, 0x12,
				0xA9, 0xD9, 0xEA, 0x1A, 0xFB, 0x22, 0x5E, 0x88, 0xAC,

				0x80, 0xFA, 0xE9, 0xC7, 0x00, 0x00, 0x00, 0x00,
				0x19,

				0x76, 0xA9, 0x14, 0x0E, 0xAB, 0x5B, 0xEA, 0x43, 0x6A, 0x04, 0x84, 0xCF, 0xAB, 0x12, 0x48, 0x5E,
				0xFD, 0xA0, 0xB7, 0x8B, 0x4E, 0xCC, 0x52, 0x88, 0xAC,

				0x00, 0x00, 0x00, 0x00
			},
			1U, new Tuple<uint>[] {Tuple.Create(UInt32.MaxValue)}, new Tuple<ulong>[] {Tuple.Create(5000000UL), Tuple.Create(3354000000UL)}, 0UL, 3359000000UL,
			@"{
			  ""hash"":""d4a73f51ab7ee7acb4cf0505d1fab34661666c461488e58ec30281e2becd93e2"",
			  ""ver"":1,
			  ""vin_sz"":1,
			  ""vout_sz"":2,
			  ""lock_time"":0,
			  ""size"":258,
			  ""in"":[
			    {
			      ""prev_out"":{
			        ""hash"":""2936ee6a0db4e4901988503bb6e966128dd5fa01bcf08451f78a1d5b08dbbd6d"",
			        ""n"":0
			      },
			      ""scriptSig"":""3045022100f3581e1972ae8ac7c7367a7a253bc1135223adb9a468bb3a59233f45bc578380022059af01ca17d00e41837a1d58e97aa31bae584edec28d35bd96923690913bae9a01 049c02bfc97ef236ce6d8fe5d94013c721e915982acd2b12b65d9b7d59e20a842005f8fc4e02532e873d37b96f09d6d4511ada8f14042f46614a4c70c0f14beff5""
			    }
			  ],
			  ""out"":[
			    {
			      ""value"":""0.05000000"",
			      ""scriptPubKey"":""OP_DUP OP_HASH160 1aa0cd1cbea6e7458a7abad512a9d9ea1afb225e OP_EQUALVERIFY OP_CHECKSIG""
			    },
			    {
			      ""value"":""33.54000000"",
			      ""scriptPubKey"":""OP_DUP OP_HASH160 0eab5bea436a0484cfab12485efda0b78b4ecc52 OP_EQUALVERIFY OP_CHECKSIG""
			    }
			  ]
			}"
			)
		};

		[Test]
		public void Read() {
			foreach(var transaction in Transactions) {
				using(BinaryReader reader = new BinaryReader(new MemoryStream(transaction.Item1))) {
					Transaction tx = Transaction.Read(reader);

					Assert.AreEqual(transaction.Item2, tx.Version);

					Assert.AreEqual(transaction.Item3.Length, tx.Inputs.Count);
					for(int i = 0; i < tx.Inputs.Count; i++) {
						Assert.AreEqual(transaction.Item3[i].Item1, tx.Inputs[i].Sequence);
					}

					
					Assert.AreEqual(transaction.Item4.Length, tx.Outputs.Count);
					for(int i = 0; i < tx.Outputs.Count; i++) {
						Assert.AreEqual(transaction.Item4[i].Item1, tx.Outputs[i].Value);
					}

					Assert.AreEqual(transaction.Item5, tx.LockTime);
					Assert.AreEqual(transaction.Item6, tx.Value);
				}
			}
		}

		[Test]
		public void Write() {
			foreach(var transaction in Transactions) {
				Transaction tx;
				using(MemoryStream stream = new MemoryStream(transaction.Item1)) {
					using(BinaryReader reader = new BinaryReader(stream))
						tx = Transaction.Read(reader);
				}

				using(MemoryStream stream = new MemoryStream()) {
					using(BinaryWriter writer = new BinaryWriter(stream))
						tx.Write(writer);

					Assert.AreEqual(transaction.Item1, stream.ToArray());
				}
			}
		}

		[Test]
		public void ReadFromJson() {
			foreach(var transaction in Transactions) {
				Transaction tx = Transaction.FromJson(transaction.Item7);

				Assert.AreEqual(transaction.Item2, tx.Version);

				Assert.AreEqual(transaction.Item3.Length, tx.Inputs.Count);
//				for(int i = 0; i < tx.Inputs.Count; i++) {
//					Assert.AreEqual(transaction.Item3[i].Item1, tx.Inputs[i].Sequence);
//					Assert.AreEqual(transaction.Item3[i].Item2, tx.Inputs[i].ScriptSig); // TODO: Assert script equality
//				}


				Assert.AreEqual(transaction.Item4.Length, tx.Outputs.Count);
				for(int i = 0; i < tx.Outputs.Count; i++) {
					Assert.AreEqual(transaction.Item4[i].Item1, tx.Outputs[i].Value);
//					Assert.AreEqual(transaction.Item4[i].Item2, tx.Outputs[i].ScriptPubKey)
				}

				Assert.AreEqual(transaction.Item5, tx.LockTime);
				Assert.AreEqual(transaction.Item6, tx.Value);
			}
		}

//		[Test]
//		public void InputHash() {
//			// TODO: Refactor this spec to use a Tuple array
//
//			// http://blockchain.info/tx/ec8775eab96692a59a32556669faf305ecc0b45064c6d6259a8097767f65fcf8
//			byte[] raw = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x01, 0xE4, 0x1C, 0x5B, 0x7D, 0x6C, 0xF9, 0x67, 0x47, 0x45, 0x0F, 0xAB, 0xC7, 0x31, 0xEE, 0x2B, 0x7A, 0x91, 0x6B, 0x37, 0x1C, 0xDF, 0xC1, 0x9C, 0x9C, 0x6A, 0xB1, 0xBC, 0xC4, 0x1E, 0xB1, 0xE2, 0x7C, 0x06, 0x00, 0x00, 0x00, 0x8A, 0x47, 0x30, 0x44, 0x02, 0x20, 0x56, 0x37, 0x2E, 0x90, 0x4B, 0xE1, 0x15, 0xD2, 0xFC, 0x1E, 0x7C, 0xDC, 0xBC, 0x2A, 0x89, 0xB7, 0xC5, 0xC8, 0xC4, 0x3F, 0x79, 0x23, 0x12, 0x1F, 0x1D, 0xF6, 0xA8, 0xB0, 0x2C, 0xE9, 0xC3, 0xEF, 0x02, 0x20, 0x4A, 0x68, 0xD3, 0x96, 0x72, 0xA0, 0xEB, 0xB2, 0xB4, 0x65, 0x8B, 0x96, 0xB9, 0xC6, 0x2E, 0x8E, 0x25, 0x37, 0xCD, 0x98, 0x1E, 0x59, 0xE0, 0x85, 0xE6, 0x21, 0x0F, 0xC7, 0xB6, 0xDB, 0xF8, 0x3C, 0x01, 0x41, 0x04, 0xEF, 0x96, 0xE3, 0xBC, 0xCC, 0x8F, 0xFF, 0x6B, 0x21, 0xD2, 0x8E, 0x81, 0xF6, 0x1C, 0x4A, 0x93, 0xCF, 0xE0, 0xF1, 0x33, 0x21, 0x4C, 0x95, 0x47, 0xC0, 0xD6, 0x83, 0xA9, 0xFC, 0x12, 0xF5, 0x29, 0x22, 0x9C, 0x8D, 0x1A, 0xB2, 0x00, 0x04, 0xC0, 0xF7, 0xF1, 0x39, 0x61, 0x56, 0x6B, 0x65, 0x49, 0x2C, 0x62, 0x67, 0xFA, 0x45, 0x27, 0x84, 0xC0, 0x72, 0x4B, 0x4F, 0x54, 0x2E, 0x40, 0x01, 0xF1, 0xFF, 0xFF, 0xFF, 0xFF, 0x02, 0xA0, 0x86, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x19, 0x76, 0xA9, 0x14, 0xC2, 0xA3, 0xB7, 0x39, 0xCF, 0x9B, 0x8D, 0x9A, 0xC3, 0xF9, 0xE9, 0xC7, 0xCD, 0x01, 0xB0, 0xDF, 0xC0, 0xA6, 0xF3, 0xE3, 0x88, 0xAC, 0x00, 0x35, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x19, 0x76, 0xA9, 0x14, 0x06, 0xF1, 0xC4, 0xA7, 0xCD, 0x50, 0x29, 0x84, 0xB7, 0xC6, 0xEA, 0xE9, 0x5D, 0xB1, 0x76, 0xB9, 0xAE, 0x4A, 0x6E, 0x08, 0x88, 0xAC, 0x00, 0x00, 0x00, 0x00 };
//
//			// http://blockchain.info/tx-index/78799607/6
//			Script pubKeyScript = Script.FromString("OP_DUP OP_HASH160 06f1c4a7cd502984b7c6eae95db176b9ae4a6e08 OP_EQUALVERIFY OP_CHECKSIG");
//
//			byte[] hash = new byte[] { 0xBE, 0x39, 0x6E, 0xE1, 0x6A, 0x3A, 0x3E, 0xBE, 0x60, 0x1D, 0x40, 0xAC, 0x25, 0x94, 0x4D, 0x25, 0xE2, 0xEA, 0x18, 0xB3, 0x2B, 0x73, 0x0D, 0x29, 0xD7, 0x67, 0x05, 0x8C, 0x84, 0x24, 0x6E, 0xBD };
//
//			using(BinaryReader reader = new BinaryReader(new MemoryStream(raw))) {
//				Transaction tx = new Transaction();
//				tx.ReadPayload(reader);
//				Assert.AreEqual(hash, tx.InputHash(SigHash.All, pubKeyScript, 0)); // TODO: Get sig from transaction (use console; Script s = new Script(new byte[] { <copy/paste script> }); s.ToString();) Manually parse for string
//			}
//		}

		[Test]
		public void ValidateSig() {
			// TODO: Refactor this spec to use a Tuple array

			// http://blockchain.info/tx/ec8775eab96692a59a32556669faf305ecc0b45064c6d6259a8097767f65fcf8
			byte[] raw = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x01, 0xE4, 0x1C, 0x5B, 0x7D, 0x6C, 0xF9, 0x67, 0x47, 0x45, 0x0F, 0xAB, 0xC7, 0x31, 0xEE, 0x2B, 0x7A, 0x91, 0x6B, 0x37, 0x1C, 0xDF, 0xC1, 0x9C, 0x9C, 0x6A, 0xB1, 0xBC, 0xC4, 0x1E, 0xB1, 0xE2, 0x7C, 0x06, 0x00, 0x00, 0x00, 0x8A, 0x47, 0x30, 0x44, 0x02, 0x20, 0x56, 0x37, 0x2E, 0x90, 0x4B, 0xE1, 0x15, 0xD2, 0xFC, 0x1E, 0x7C, 0xDC, 0xBC, 0x2A, 0x89, 0xB7, 0xC5, 0xC8, 0xC4, 0x3F, 0x79, 0x23, 0x12, 0x1F, 0x1D, 0xF6, 0xA8, 0xB0, 0x2C, 0xE9, 0xC3, 0xEF, 0x02, 0x20, 0x4A, 0x68, 0xD3, 0x96, 0x72, 0xA0, 0xEB, 0xB2, 0xB4, 0x65, 0x8B, 0x96, 0xB9, 0xC6, 0x2E, 0x8E, 0x25, 0x37, 0xCD, 0x98, 0x1E, 0x59, 0xE0, 0x85, 0xE6, 0x21, 0x0F, 0xC7, 0xB6, 0xDB, 0xF8, 0x3C, 0x01, 0x41, 0x04, 0xEF, 0x96, 0xE3, 0xBC, 0xCC, 0x8F, 0xFF, 0x6B, 0x21, 0xD2, 0x8E, 0x81, 0xF6, 0x1C, 0x4A, 0x93, 0xCF, 0xE0, 0xF1, 0x33, 0x21, 0x4C, 0x95, 0x47, 0xC0, 0xD6, 0x83, 0xA9, 0xFC, 0x12, 0xF5, 0x29, 0x22, 0x9C, 0x8D, 0x1A, 0xB2, 0x00, 0x04, 0xC0, 0xF7, 0xF1, 0x39, 0x61, 0x56, 0x6B, 0x65, 0x49, 0x2C, 0x62, 0x67, 0xFA, 0x45, 0x27, 0x84, 0xC0, 0x72, 0x4B, 0x4F, 0x54, 0x2E, 0x40, 0x01, 0xF1, 0xFF, 0xFF, 0xFF, 0xFF, 0x02, 0xA0, 0x86, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x19, 0x76, 0xA9, 0x14, 0xC2, 0xA3, 0xB7, 0x39, 0xCF, 0x9B, 0x8D, 0x9A, 0xC3, 0xF9, 0xE9, 0xC7, 0xCD, 0x01, 0xB0, 0xDF, 0xC0, 0xA6, 0xF3, 0xE3, 0x88, 0xAC, 0x00, 0x35, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x19, 0x76, 0xA9, 0x14, 0x06, 0xF1, 0xC4, 0xA7, 0xCD, 0x50, 0x29, 0x84, 0xB7, 0xC6, 0xEA, 0xE9, 0x5D, 0xB1, 0x76, 0xB9, 0xAE, 0x4A, 0x6E, 0x08, 0x88, 0xAC, 0x00, 0x00, 0x00, 0x00 };

			byte[] sig = new byte[] { 0x30, 0x44, 0x02, 0x20, 0x56, 0x37, 0x2E, 0x90, 0x4B, 0xE1, 0x15, 0xD2, 0xFC, 0x1E, 0x7C, 0xDC, 0xBC, 0x2A, 0x89, 0xB7, 0xC5, 0xC8, 0xC4, 0x3F, 0x79, 0x23, 0x12, 0x1F, 0x1D, 0xF6, 0xA8, 0xB0, 0x2C, 0xE9, 0xC3, 0xEF, 0x02, 0x20, 0x4A, 0x68, 0xD3, 0x96, 0x72, 0xA0, 0xEB, 0xB2, 0xB4, 0x65, 0x8B, 0x96, 0xB9, 0xC6, 0x2E, 0x8E, 0x25, 0x37, 0xCD, 0x98, 0x1E, 0x59, 0xE0, 0x85, 0xE6, 0x21, 0x0F, 0xC7, 0xB6, 0xDB, 0xF8, 0x3C, 0x01 };
			byte[] pubKey = new byte[] { 0x04, 0xEF, 0x96, 0xE3, 0xBC, 0xCC, 0x8F, 0xFF, 0x6B, 0x21, 0xD2, 0x8E, 0x81, 0xF6, 0x1C, 0x4A, 0x93, 0xCF, 0xE0, 0xF1, 0x33, 0x21, 0x4C, 0x95, 0x47, 0xC0, 0xD6, 0x83, 0xA9, 0xFC, 0x12, 0xF5, 0x29, 0x22, 0x9C, 0x8D, 0x1A, 0xB2, 0x00, 0x04, 0xC0, 0xF7, 0xF1, 0x39, 0x61, 0x56, 0x6B, 0x65, 0x49, 0x2C, 0x62, 0x67, 0xFA, 0x45, 0x27, 0x84, 0xC0, 0x72, 0x4B, 0x4F, 0x54, 0x2E, 0x40, 0x01, 0xF1 };

			// http://blockchain.info/tx-index/78799607/6
			Script pubKeyScript = Script.FromString("OP_DUP OP_HASH160 06f1c4a7cd502984b7c6eae95db176b9ae4a6e08 OP_EQUALVERIFY OP_CHECKSIG");

			using(BinaryReader reader = new BinaryReader(new MemoryStream(raw))) {
				Transaction tx = new Transaction();
				tx.ReadPayload(reader);
				Assert.IsTrue(tx.SigIsValid(pubKey, sig, pubKeyScript, 0)); // TODO: Get sig from transaction (use console; Script s = new Script(new byte[] { <copy/paste script> }); s.ToString();) Manually parse for string
			}
		}
	}
}

