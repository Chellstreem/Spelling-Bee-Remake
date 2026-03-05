using UnityEngine;
using Zenject;

namespace InteractableObjects
{
    public class CorrectOrbLetter : OrbLetter
    {
        private ICurrentWordGetter currentWord;

        [Inject]
        public void Construct(ICurrentWordGetter currentWord)
        {
            this.currentWord = currentWord;
        }

        protected override string SetRandomLetter()
        {
            int length = currentWord.CurrentWord.Length;
            return currentWord.CurrentWord[Random.Range(0, length)].ToString();
        }

        protected override void ReturnToOriginalState()
        {
            gameObject.SetActive(false);
        }
    }
}
            
    

