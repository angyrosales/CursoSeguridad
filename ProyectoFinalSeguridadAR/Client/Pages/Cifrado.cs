﻿using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace ProyectoFinalSeguridadAR.Client.Pages
{
    public static class Cifrado
    {
        public static byte[] GenerateRandomKey()
        {
            KeyGenerationParameters keyGenParam = new(new SecureRandom(), 256);
            CipherKeyGenerator generator = GeneratorUtilities.GetKeyGenerator("AES");
            generator.Init(keyGenParam);

            return generator.GenerateKey();
        }

        public static byte[] GenerateRandomIV()
        {
            SecureRandom random = new();
            byte[] iv = new byte[16];
            random.NextBytes(iv);

            return iv;
        }
        public static byte[] EncryptStringToBytes(string input, byte[] key, byte[] iv)
        {
            IBlockCipherPadding padding = new ZeroBytePadding();
            BufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new KCtrBlockCipher(new AesEngine()), padding);

            cipher.Init(true, new ParametersWithIV(new KeyParameter(key), iv));

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] outputBytes = new byte[cipher.GetOutputSize(inputBytes.Length)];
            int length = cipher.ProcessBytes(inputBytes, 0, inputBytes.Length, outputBytes, 0);
            length += cipher.DoFinal(outputBytes, length);

            // Asegúrate de devolver solo los bytes válidos
            byte[] result = new byte[length];
            Array.Copy(outputBytes, result, length);

            return result;
        }
        public static string DecryptBytesToString(byte[] inputBytes, byte[] key, byte[] iv)
        {
            IBlockCipherPadding padding = new ZeroBytePadding();
            BufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new KCtrBlockCipher(new AesEngine()), padding);

            cipher.Init(false, new ParametersWithIV(new KeyParameter(key), iv));

            byte[] outputBytes = new byte[cipher.GetOutputSize(inputBytes.Length)];
            int length = cipher.ProcessBytes(inputBytes, 0, inputBytes.Length, outputBytes, 0);
            length += cipher.DoFinal(outputBytes, length);

            return Encoding.UTF8.GetString(outputBytes, 0, length);
        }
    }
}
