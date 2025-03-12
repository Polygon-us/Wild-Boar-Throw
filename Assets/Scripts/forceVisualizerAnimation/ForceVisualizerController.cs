using UnityEngine.Playables;
using UnityEngine;

namespace ForceVisualizerAnimation
{
    public class ForceVisualizerController : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector playableDirector;

        private const float TimeForThrowAnimation = 1;

        public void MovePlayableDirector(float time)
        {
            playableDirector.time = time;
            playableDirector.Evaluate();
        }

        public void PlayThrowAnimation()
        {
            playableDirector.time = TimeForThrowAnimation;
            playableDirector.Play();
        }

    }
}
