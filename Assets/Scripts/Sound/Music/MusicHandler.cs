using UnityEngine;

namespace Sounds
{
    public class MusicHandler
    {
        private readonly EventManager eventManager;
        private readonly IMusicPlayer musicPlayer;

        public MusicHandler(EventManager eventManager, IMusicPlayer musicPlayer)
        {
            this.eventManager = eventManager;
            this.musicPlayer = musicPlayer;

            SubscribeToEvents();
        }

        public void OnEvent()
        {
            musicPlayer.PlaySound(SoundType.GamePlayMusic);
        }




        private void SubscribeToEvents()
        {

        }
    }
}
