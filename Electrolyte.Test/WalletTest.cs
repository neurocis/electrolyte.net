using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Electrolyte.Primitives;
using NUnit.Framework;

namespace Electrolyte.Test {
	[TestFixture]
	public class WalletTest {
		[Test]
		public void ImportKeys() {
			Wallet wallet = new Wallet();
			wallet.ImportKey("5Kb8kLf9zgWQnogidDA76MzPL6TsZZY36hWXMssSzNydYXYB9KF");

			Assert.Contains(new byte[] { 0xE9, 0x87, 0x3D, 0x79, 0xC6, 0xD8, 0x7D, 0xC0, 0xFB, 0x6A, 0x57, 0x78, 0x63, 0x33, 0x89, 0xF4, 0x45, 0x32, 0x13, 0x30, 0x3D, 0xA6, 0x1F, 0x20, 0xBD, 0x67, 0xFC, 0x23, 0x3A, 0xA3, 0x32, 0x62 }, wallet.PrivateKeys.Values);
			Assert.Contains("1CC3X2gu58d6wXUWMffpuzN9JAfTUWu4Kj", wallet.PrivateKeys.Keys);
			Assert.Contains("1CC3X2gu58d6wXUWMffpuzN9JAfTUWu4Kj", wallet.Addresses);
		}

		[Test]
		public void WatchAddress() {
			Wallet wallet = new Wallet();

			wallet.WatchAddress("1CC3X2gu58d6wXUWMffpuzN9JAfTUWu4Kj");
			wallet.WatchAddress("1ky1eHUrRR1kxKTbfiCptao9V25W97gDm");

			Assert.Contains("1CC3X2gu58d6wXUWMffpuzN9JAfTUWu4Kj", wallet.Addresses);
			Assert.Contains("1CC3X2gu58d6wXUWMffpuzN9JAfTUWu4Kj", wallet.WatchAddresses);

			Assert.Contains("1ky1eHUrRR1kxKTbfiCptao9V25W97gDm", wallet.Addresses);
			Assert.Contains("1ky1eHUrRR1kxKTbfiCptao9V25W97gDm", wallet.WatchAddresses);
		}

		[Test]
		public void ReadWritePrivate() {
			Wallet writeWallet = new Wallet();
			writeWallet.ImportKey("5Kb8kLf9zgWQnogidDA76MzPL6TsZZY36hWXMssSzNydYXYB9KF");

			byte[] output;
			using(MemoryStream stream = new MemoryStream()) {
				using(StreamWriter writer = new StreamWriter(stream))
					writeWallet.WritePrivate(writer);
				output = stream.ToArray();
			}
			
			Wallet readWallet = new Wallet();
			using(MemoryStream stream = new MemoryStream(output)) {
				using(StreamReader reader = new StreamReader(stream))
					readWallet.ReadPrivate(reader);
			}

			Assert.AreEqual(writeWallet.Addresses, readWallet.Addresses);
			Assert.AreEqual(writeWallet.PrivateKeys, readWallet.PrivateKeys);
		}

		[Test]
		public void EncryptDecrypt() {
			Wallet encryptWallet = new Wallet();
			encryptWallet.ImportKey("5Kb8kLf9zgWQnogidDA76MzPL6TsZZY36hWXMssSzNydYXYB9KF");

			byte[] output;
			using(MemoryStream stream = new MemoryStream()) {
				using(StreamWriter writer = new StreamWriter(stream))
					encryptWallet.Encrypt(writer, "1234");
				output = stream.ToArray();
			}

			Wallet decryptWallet = new Wallet();
			using(MemoryStream stream = new MemoryStream(output)) {
				using(StreamReader reader = new StreamReader(stream))
					decryptWallet.Decrypt(reader, "1234");
			}

			Assert.AreEqual(encryptWallet.Addresses, decryptWallet.Addresses);
			Assert.AreEqual(encryptWallet.PrivateKeys, decryptWallet.PrivateKeys);
		}

		[Test]
		public void InvalidPasscode() {
			Wallet encryptWallet = new Wallet();
			encryptWallet.GenerateKey();

			byte[] output;
			using(MemoryStream stream = new MemoryStream()) {
				using(StreamWriter writer = new StreamWriter(stream))
					encryptWallet.Encrypt(writer, "1234");
				output = stream.ToArray();
			}

			using(MemoryStream stream = new MemoryStream(output)) {
				using(StreamReader reader = new StreamReader(stream))
					Assert.Throws<CryptographicException>(() => new Wallet().Decrypt(reader, "2345"));
			}
		}

		[Test]
		public void LockUnlock() {
			Wallet wallet = new Wallet();
			wallet.ImportKey("5Kb8kLf9zgWQnogidDA76MzPL6TsZZY36hWXMssSzNydYXYB9KF");

			wallet.Lock("1234");
			wallet.Unlock("1234");

			wallet.Lock();
			wallet.Unlock("1234");

			wallet.Lock();
			Assert.Throws<CryptographicException>(() => wallet.Unlock("2345"));
			wallet.Unlock("1234");

			wallet.Lock("2345");
			Assert.Throws<CryptographicException>(() => wallet.Unlock("1234"));
			wallet.Unlock("2345");
			Assert.Throws<InvalidOperationException>(() => wallet.Unlock("2345"));

			wallet.Lock();
			Assert.Throws<InvalidOperationException>(wallet.Lock);
			wallet.Unlock("2345");

			wallet.Lock();
			wallet.Unlock("2345");
		}

		[Test]
		public void UnlockTimeout() {
			Wallet wallet = new Wallet();
			wallet.ImportKey("5Kb8kLf9zgWQnogidDA76MzPL6TsZZY36hWXMssSzNydYXYB9KF");

			wallet.Lock("1234");
			Assert.IsTrue(wallet.IsLocked);

			wallet.Unlock("1234", 500);
			Assert.IsFalse(wallet.IsLocked);

			Thread.Sleep(1000);

			Assert.IsTrue(wallet.IsLocked);

		}

		[Test]
		public void ImportPrivateKey() {
			Wallet wallet = new Wallet();
			wallet.ImportKey("5Kb8kLf9zgWQnogidDA76MzPL6TsZZY36hWXMssSzNydYXYB9KF");

			ECKey key = new ECKey(wallet.PrivateKeys.Values.ToArray()[0]);
			Assert.AreEqual("5Kb8kLf9zgWQnogidDA76MzPL6TsZZY36hWXMssSzNydYXYB9KF", key.ToWalletImportFormat());

			Assert.Contains("1CC3X2gu58d6wXUWMffpuzN9JAfTUWu4Kj", wallet.PrivateKeys.Keys);
		}

		[Test]
		public void AccessLimitation() {
			Wallet wallet = new Wallet();
			wallet.ImportKey("5Kb8kLf9zgWQnogidDA76MzPL6TsZZY36hWXMssSzNydYXYB9KF");
			wallet.WatchAddress("1ky1eHUrRR1kxKTbfiCptao9V25W97gDm");

			Assert.Contains("1CC3X2gu58d6wXUWMffpuzN9JAfTUWu4Kj", wallet.Addresses);
			Assert.Contains("1ky1eHUrRR1kxKTbfiCptao9V25W97gDm", wallet.Addresses);

			wallet.Lock("1234");

			Assert.Contains("1ky1eHUrRR1kxKTbfiCptao9V25W97gDm", wallet.Addresses);
			Assert.IsFalse(wallet.Addresses.Contains("1CC3X2gu58d6wXUWMffpuzN9JAfTUWu4Kj"));

			wallet.Unlock("1234");

			Assert.Contains("1CC3X2gu58d6wXUWMffpuzN9JAfTUWu4Kj", wallet.Addresses);
			Assert.Contains("1ky1eHUrRR1kxKTbfiCptao9V25W97gDm", wallet.Addresses);
		}
	}
}

