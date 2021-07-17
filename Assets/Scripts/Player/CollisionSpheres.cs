using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace Game.PlayerCharacter
{
    public class CollisionSpheres : MonoBehaviour
    {
        public PlayerMovement owner;
        public List<GameObject> frontSphereGroundCheckers { get; private set; }
        public List<GameObject> bottomSphereGroundCheckers { get; private set; }
        public List<GameObject> backSphereGroundCheckers { get; private set; }
        [SerializeField] int horizontalSections = 5;
        [SerializeField] int verticalSections = 10;
        [SerializeField] GameObject groundCheckingSphere = null;

        void Awake()
        {
            bottomSphereGroundCheckers = new List<GameObject>(horizontalSections + 2);
            frontSphereGroundCheckers = new List<GameObject>(verticalSections + 2); backSphereGroundCheckers = new List<GameObject>(verticalSections + 2);
        }

        public (float a, float b, float c, float d) GetTopBottomFrontBackDimensions()
        {
            // y-z plane in this case
            float top = owner.boxCollider.bounds.center.y + owner.boxCollider.bounds.extents.y;
            float bottom = owner.boxCollider.bounds.center.y - owner.boxCollider.bounds.extents.y;
            float front = owner.boxCollider.bounds.center.z + owner.boxCollider.bounds.extents.z;
            float back = owner.boxCollider.bounds.center.z - owner.boxCollider.bounds.extents.z;

            return (top, bottom, front, back);
        }

        /// <summary>
        /// credit goes to roundbeargames for setting up these spheres
        /// https://www.youtube.com/channel/UCAoJgVzDHnFDOQwC42raByg
        /// </summary>
        public void SetColliderSpheres()
        {
            // y-z plane in this case

            // populate BottomSpheres list
            for (var i = 0; i < horizontalSections; i++)
            {
                GameObject obj = CreateGroundCheckingSphere(Vector3.zero);
                obj.transform.parent = this.transform.Find("BottomSpheres");
                obj.name = $"bottomSphere{i}";
                bottomSphereGroundCheckers.Add(obj);
            }

            // populate FrontSpheres list
            for (var i = 0; i < verticalSections; i++)
            {
                GameObject obj = CreateGroundCheckingSphere(Vector3.zero);
                obj.transform.parent = this.transform.Find("FrontSpheres");
                obj.name = $"frontSphere{i}";
                frontSphereGroundCheckers.Add(obj);
            }

            // populate BackSpheres list
            for (var i = 0; i < verticalSections; i++)
            {
                GameObject obj = CreateGroundCheckingSphere(Vector3.zero);
                obj.transform.parent = this.transform.Find("BackSpheres");
                obj.name = $"backSphere{i}";
                backSphereGroundCheckers.Add(obj);
            }

            RepositionBottomSpheres();
            RepositionFrontSpheres();
            RepositionBackSpheres();

        }

        public GameObject CreateGroundCheckingSphere(Vector3 position) => Instantiate<GameObject>(groundCheckingSphere, position, Quaternion.identity);

        public void RepositionBackSpheres()
        {
            // TODO
            (float top, float bottom, float front, float back) dimensions = GetTopBottomFrontBackDimensions();

            backSphereGroundCheckers[0].transform.localPosition = new Vector3(0, dimensions.bottom + 0.05f, dimensions.back) - transform.position;
            backSphereGroundCheckers[1].transform.localPosition = new Vector3(0, dimensions.top, dimensions.back) - transform.position;

            float interval = (dimensions.top - dimensions.bottom + 0.05f) / (verticalSections - 1);

            for (int i = 2; i < backSphereGroundCheckers.Count; i++)
            {
                backSphereGroundCheckers[i].transform.localPosition =
                    new Vector3(0, dimensions.bottom + (interval * (i - 1)), dimensions.back) - transform.position;
            }
        }

        public void RepositionFrontSpheres()
        {
            (float top, float bottom, float front, float back) dimensions = GetTopBottomFrontBackDimensions();

            frontSphereGroundCheckers[0].transform.localPosition = new Vector3(0, dimensions.bottom + 0.05f, dimensions.front) - transform.position;
            frontSphereGroundCheckers[1].transform.localPosition = new Vector3(0, dimensions.top, dimensions.front) - transform.position;

            float interval = (dimensions.top - dimensions.bottom + 0.05f) / (verticalSections - 1);

            for (int i = 2; i < frontSphereGroundCheckers.Count; i++)
            {
                frontSphereGroundCheckers[i].transform.localPosition =
                    new Vector3(0, dimensions.bottom + (interval * (i - 1)), dimensions.front) - transform.position;
            }
        }

        public void RepositionBottomSpheres()
        {
            (float top, float bottom, float front, float back) dimensions = GetTopBottomFrontBackDimensions();

            bottomSphereGroundCheckers[0].transform.localPosition = new Vector3(0, dimensions.bottom, dimensions.back) - transform.position;
            bottomSphereGroundCheckers[1].transform.localPosition = new Vector3(0, dimensions.bottom, dimensions.front) - transform.position;

            float interval = (dimensions.front - dimensions.back) / (horizontalSections - 1);

            for (int i = 2; i < bottomSphereGroundCheckers.Count; i++)
            {
                bottomSphereGroundCheckers[i].transform.localPosition =
                    new Vector3(0, dimensions.bottom, dimensions.back + (interval * (i - 1))) - transform.position;
            }
        }


    }
}

