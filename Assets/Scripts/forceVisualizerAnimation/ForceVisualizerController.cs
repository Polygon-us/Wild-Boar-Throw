using UnityEngine;
using UnityEngine.Playables;

namespace forceVisualizerAnimation
{
    public class ForceVisualizerController : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector playableDirector;
    
        public void MovePlayableDirector(float time)
        {
            playableDirector.time = time;
            playableDirector.Evaluate();
        }

    }
}
