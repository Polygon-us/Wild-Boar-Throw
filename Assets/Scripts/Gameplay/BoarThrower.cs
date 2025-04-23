using System.Collections.Generic;
using System.Linq;
using Gameplay.Controllers;
using UnityEngine;

namespace Gameplay
{
    public class BoarThrower : MonoBehaviour
    {
        [SerializeField] private ForceController forceController;
        [SerializeField] private Transform startingPoint;
        [SerializeField] private Boar boar;
        [SerializeField] private ForceMode forceMode;
        [SerializeField] private float maxTorque;

        private List<Vector3> initialPositions;
        private List<Quaternion> initialRotations;

        [SerializeField] private Vector3 boarOffsetFromStartPoint = new Vector3(0f, 0f, 0f);

        public float BoarDistance => boar.transform.position.z - startingPoint.position.z;

        private void Awake()
        {
            initialPositions = boar.BoarRbs.Select(x => x.position - boar.Parent.position).ToList();
            initialRotations = boar.BoarRbs.Select(x => x.rotation).ToList();
        }

        public void ThrowBoar(float force, float angle)
        {
            boar.MainRb.isKinematic = false;

            Vector3 direction = Quaternion.Euler(-angle, 0f, 0f) * startingPoint.forward;

            Vector3 appliedForce = force / boar.BoarRbs.Count * direction;
            
            foreach (var rb in boar.BoarRbs)
            {
                rb.AddForce(appliedForce, forceMode);
            }
            
            boar.MainRb.AddTorque(Vector3.right * force / forceController.MaxForce * maxTorque, forceMode);
            AudioManager.Instance.PlaySFX("Launch", 1f);
        }

        public void MoveBoarWithStartingPosition()
        {
            MoveRbToStartingPosition(boar.BoarRbs[0], initialPositions[0], initialRotations[0]);
        }

        public void Reset()
        {
            for (int i = 0; i < boar.BoarRbs.Count; i++)
            {
                MoveRbToStartingPosition(boar.BoarRbs[i], initialPositions[i], initialRotations[i]);
            }
        }

        private void MoveRbToStartingPosition(Rigidbody rb, Vector3 moveTo, Quaternion rotation)
        {
            rb.isKinematic = true;

            rb.position = moveTo + startingPoint.position - boarOffsetFromStartPoint;
            rb.rotation = rotation * startingPoint.rotation;

            rb.isKinematic = rb == boar.MainRb;
        }
    }
}