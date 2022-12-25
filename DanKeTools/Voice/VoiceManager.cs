using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools;
using DanKeTools.IO;
using DanKeTools.Mono;
using UnityEngine.Events;

namespace DanKeTools.Voice
{


    ///<summary>
    ///脚本名称： VoiceManager.cs
    ///修改时间：2022/12/25
    ///脚本功能：声音管理器
    ///备注：
    ///</summary>

    public class VoiceManager : Singleton<VoiceManager>
    {
        private AudioSource bkMusic = null;
        private float bkVaule = 1;
        private float soundVaule = 1;
        private GameObject soundObj = null;
        private List<AudioSource> soundList = new List<AudioSource>();

        public VoiceManager()
        {
            MonoManager.Instance().AddUpdateListener(Update);
        }

        private void Update()
        {
            for (int i = soundList.Count - 1; i >= 0; i--)
            {
                if (!soundList[i].isPlaying)
                {
                    GameObject.Destroy(soundList[i]);
                    soundList.RemoveAt(i);
                }
            }
        }


        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="name">背景音乐</param>
        public void PlayBKMusic(string name)
        {
            if (bkMusic == null)
            {
                GameObject obj = new GameObject("BKMusic");
                bkMusic = obj.AddComponent<AudioSource>();
            }

            //异步加载背景音乐并且加载完成后播放
            FileManager.Instance().LoadAsync<AudioClip>("Music/bk/" + name, (clip) =>
            {
                bkMusic.clip = clip;
                bkMusic.loop = true;
                //调整大小 
                bkMusic.volume = bkVaule;
                bkMusic.Play();
            });
        }

        /// <summary>
        /// 改变背景声音大小
        /// </summary>
        /// <param name="v">大小</param>
        public void ChangeBKValue(float v)
        {
            bkVaule = v;
            if (bkMusic == null)
            {
                return;
            }

            bkMusic.volume = bkVaule;
        }

        /// <summary>
        /// 暂停背景音乐
        /// </summary>
        public void PauseBKMusic()
        {
            if (bkMusic == null)
            {
                return;
            }

            bkMusic.Pause();
        }

        /// <summary>
        /// 停止背景音乐
        /// </summary>
        public void StopBKMusic()
        {
            if (bkMusic == null)
            {
                return;
            }

            bkMusic.Stop();
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isLoop"></param>
        /// <param name="callback"></param>
        public void PlaySound(string name, bool isLoop, UnityAction<AudioSource> callback = null)
        {
            if (soundObj == null)
            {
                soundObj = new GameObject();
                soundObj.name = "Sounds";
            }

            AudioSource source = soundObj.AddComponent<AudioSource>();
            FileManager.Instance().LoadAsync<AudioClip>("Music/Sounds/" + name, (clip) =>
            {
                source.clip = clip;
                source.loop = isLoop;
                //调整大小 
                source.volume = soundVaule;
                source.Play();
                //音效资源异步加载结束后，将这个音效组件加入集合中
                soundList.Add(source);
                if (callback != null)
                {
                    callback(source);
                }
            });
        }

        /// <summary>
        /// 改变音效声音大小
        /// </summary>
        /// <param name="value"></param>
        public void ChangeSoundValue(float value)
        {
            soundVaule = value;
            for (int i = 0; i < soundList.Count; ++i)
            {
                soundList[i].volume = value;
            }
        }

        /// <summary>
        /// 停止音效
        /// </summary>
        /// <param name="source"></param>
        public void StopSound(AudioSource source)
        {
            if (soundList.Contains(source))
            {
                soundList.Remove(source);
                source.Stop();
                GameObject.Destroy(source);
            }
        }
    }

}