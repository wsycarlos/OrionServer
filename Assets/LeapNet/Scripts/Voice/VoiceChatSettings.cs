using System.Linq;
using UnityEngine;

namespace VoiceChat
{
    public class VoiceChatSettings : MonoBehaviour
    {
        #region Instance

        static VoiceChatSettings instance;

        public static VoiceChatSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(VoiceChatSettings)) as VoiceChatSettings;
                }

                return instance;
            }
        }

        #endregion

        [SerializeField]
        int frequency = 16000;

        [SerializeField]
        int sampleSize = 640;
        
        [SerializeField]
        VoiceChatPreset preset = VoiceChatPreset.Alaw_16k;

        [SerializeField]
        bool localDebug = false;

        public int Frequency
        {
            get { return frequency; }
            private set { frequency = value; }
        }

        public bool LocalDebug
        {
            get { return localDebug; }
            set { localDebug = value; }
        }

        public VoiceChatPreset Preset
        {
            get { return preset; }
            set
            {
                preset = value;

                switch (preset)
                {
                    case VoiceChatPreset.Alaw_4k:
                        Frequency = 4096;
                        SampleSize = 128;
                        break;

                    case VoiceChatPreset.Alaw_8k:
                        Frequency = 8192;
                        SampleSize = 256;
                        break;

                    case VoiceChatPreset.Alaw_16k:
                        Frequency = 16384;
                        SampleSize = 512;
                        break;
                }
            }
        }
        
        public int SampleSize
        {
            get { return sampleSize; }
            private set { sampleSize = value; }
        }

        public double SampleTime
        {
            get { return (double)SampleSize / (double)Frequency; }
        }
    } 
}