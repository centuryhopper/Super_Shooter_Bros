/*
Author: Sam Burford
Date: 11/04/21
Description: This class simply allows easy access to the methods on the selected GroundChecker. 
Usage: Add the GroundCheck component to your GameObject e.g. your player. Then select any ScriptableObject for the GroundChecker. 
*/

using UnityEngine;

namespace BSGames.Modules.GroundCheck
{
    
    public class GroundCheck : MonoBehaviour
    {
        
        public GroundChecker GroundChecker { get => m_groundChecker; }
        
        [SerializeField] private GroundChecker m_groundChecker = null;
        
        private void Awake()
        {
            m_groundChecker?.SetTransform(transform);
        }
        
        private void OnValidate()
        {
            m_groundChecker?.SetTransform(transform);
        }
        
        private void OnEnable()
        {
            m_groundChecker?.OnGroundCheckerEnabled();
        }
        
        private void OnDisable()
        {
            m_groundChecker?.OnGroundCheckerDisabled();
        }
        
        public bool IsGrounded()
        {
            if (m_groundChecker == null)
            {
                Debug.LogWarning("No Ground Checker has been assigned!");
                return false;
            }
            
            return m_groundChecker.IsGrounded();
        }
        
        private void OnDrawGizmos()
        {
            m_groundChecker?.DrawGizmos();
        }
        
        private void OnDrawGizmosSelected()
        {
            m_groundChecker?.DrawGizmosSelected();
        }
        
        #region Collider 3D Methods
        private void OnCollisionEnter(Collision other)
        {
            m_groundChecker?.OnCollisionEnter(other);
        }
        
        private void OnCollisionStay(Collision other)
        {
            m_groundChecker?.OnCollisionStay(other);
        }
        
        private void OnCollisionExit(Collision other)
        {
            m_groundChecker?.OnCollisionExit(other);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            m_groundChecker?.OnTriggerEnter(other);
        }
        
        private void OnTriggerStay(Collider other)
        {
            m_groundChecker?.OnTriggerStay(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            m_groundChecker?.OnTriggerExit(other);
        }
        #endregion
        
        #region  Collider 2D Methods
        private void OnCollisionEnter2D(Collision2D other)
        {
            m_groundChecker?.OnCollisionEnter2D(other);
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            m_groundChecker?.OnCollisionStay2D(other);
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            m_groundChecker?.OnCollisionExit2D(other);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            m_groundChecker?.OnTriggerEnter2D(other);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            m_groundChecker?.OnTriggerStay2D(other);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            m_groundChecker?.OnTriggerExit2D(other);
        }
        #endregion
        
    }
    
}