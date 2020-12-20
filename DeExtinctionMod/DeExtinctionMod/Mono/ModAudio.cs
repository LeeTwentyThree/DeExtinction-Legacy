using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DeExtinctionMod
{
    public class ModAudio
    {
        private AudioClip[] allClips;

        public void Init(AssetBundle assetBundle)
        {
            allClips = assetBundle.LoadAllAssets<AudioClip>();
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
