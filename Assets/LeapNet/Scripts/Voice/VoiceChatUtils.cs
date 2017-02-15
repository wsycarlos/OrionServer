using System;
using UnityEngine;

namespace VoiceChat
{
    public static class VoiceChatUtils
    {
        static void ToShortArray(this float[] input, short[] output)
        {
            if (output.Length < input.Length)
            {
                throw new System.ArgumentException("in: " + input.Length + ", out: " + output.Length);
            }

            for (int i = 0; i < input.Length; ++i)
            {
                output[i] = (short)Mathf.Clamp((int)(input[i] * 32767.0f), short.MinValue, short.MaxValue);
            }
        }

        static void ToFloatArray(this short[] input, float[] output, int length)
        {
            if (output.Length < length || input.Length < length)
            {
                throw new System.ArgumentException();
            }

            for (int i = 0; i < length; ++i)
            {
                output[i] = input[i] / (float)short.MaxValue;
            }
        }
        
        static byte[] ALawCompress(float[] input)
        {
            byte[] output = VoiceChatBytePool.Instance.Get();

            for (int i = 0; i < input.Length; ++i)
            {
                int scaled = (int)(input[i] * 32767.0f);
                short clamped = (short)Mathf.Clamp(scaled, short.MinValue, short.MaxValue);
                output[i] = NAudio.Codecs.ALawEncoder.LinearToALawSample(clamped);
            }

            return output;
        }

        static float[] ALawDecompress(byte[] input, int length)
        {
            float[] output = VoiceChatFloatPool.Instance.Get();

            for (int i = 0; i < length; ++i)
            {
                short alaw = NAudio.Codecs.ALawDecoder.ALawToLinearSample(input[i]);
                output[i] = alaw / (float)short.MaxValue;
            }

            return output;
        }
        
        public static VoiceChatPacket Compress(float[] sample)
        {
            VoiceChatPacket packet = new VoiceChatPacket();
            packet.Length = sample.Length;
            packet.Data = ALawCompress(sample);
            return packet;
        }

        public static int Decompress(VoiceChatPacket packet, out float[] data)
        {
            data = ALawDecompress(packet.Data, packet.Length);
            return packet.Length;
        }

        public static int ClosestPowerOfTwo(int value)
        {
            int i = 1;

            while (i < value)
            {
                i <<= 1;
            }
            return i;
        }
    } 
}