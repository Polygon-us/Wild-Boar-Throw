using UnityEngine.Playables;
using UnityEngine;

namespace ForceVisualizerAnimation
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
