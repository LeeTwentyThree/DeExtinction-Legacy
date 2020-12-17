using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod.Mono
{
    public class ModAudio
    {
        private AudioClip[] allClips;
        public AudioClipPool roars;

        public void Init()
        {
            allClips = QPatch.assetBundle.LoadAllAssets<AudioClip>();
            roars = CreateClipPool("Roar");
        }

        static AudioClip LoadSound(string soundName)
        {
            return QPatch.assetBundle.LoadAsset<AudioClip>(soundName);
        }

        private AudioClipPool CreateClipPool(string[] soundNames)
        {
            AudioClip[] clips = new AudioClip[soundNames.Length];
            for(int i = 0; i < soundNames.Length; i++)
            {
                clips[i] = LoadSound(soundNames[i]);
            }
            return new AudioClipPool(clips);
        }

        public AudioClipPool CreateClipPool(string startingLetters)
        {
            List<AudioClip> clips = new List<AudioClip>();
            for (int i = 0; i < allClips.Length; i++)
            {
                if (allClips[i].name.StartsWith(startingLetters))
                {
                    clips.Add(allClips[i]);
                }
            }
            return new AudioClipPool(clips.ToArray());
        }


        public class AudioClipPool
        {
            public AudioClip[] clips;

            public AudioClipPool(AudioClip[] clips)
            {
                this.clips = clips;
            }

            public AudioClip GetRandomClip()
            {
                return clips[UnityEngine.Random.Range(0, clips.Length)];
            }
        }
    }
}
