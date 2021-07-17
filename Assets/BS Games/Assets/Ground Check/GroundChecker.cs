/*
Author: Sam Burford
Date: 11/04/21
Description: A base class for all ScriptableObjects that implement IGroundChecker. 
Usage: Add the GroundCheck component to your GameObject e.g. your player. Then select this ScriptableObject for the GroundChecker. 
*/

using UnityEngine;

namespace BSGames.Modules.GroundCheck
{
    
    [System.Serializable]
    public abstract class GroundChecker : ScriptableObject, IGroundChecker
    {
        
        ///<summary>The transform component attached to the Ground Check component. </summary>
        protected Transform transform = null;
        ///<summary>The GameObject that the Ground Check component is attached to. </summary>
        protected GameObject gameObject = null;
        
        public void SetTransform(Transform _transform)
        {
            transform = _transform;
            gameObject = transform.gameObject;
        }
        
        ///<summary>Called in OnEnable on the GameObject with the GroundCheck component. </summary>
        public virtual void OnGroundCheckerEnabled() {  }
        ///<summary>Called in OnDisable() on the GameObject with the GroundCheck component. </summary>
        public virtual void OnGroundCheckerDisabled() {  }
        ///<summary>Called in IsGrounded() on the GameObject with the GroundCheck component. </summary>
        public abstract bool IsGrounded();
        ///<summary>Called in OnDrawGizmos() on the GameObject with the GroundCheck component. </summary>
        public virtual void DrawGizmos() {  }
        ///<summary>Called in OnDrawGizmosSelected() on the GameObject with the GroundCheck component. </summary>
        public virtual void DrawGizmosSelected() {  }
        
        ///<summary>Called in OnValidate() on the GameObject with the GroundCheck component. Use OnValidate() for updates to the ScriptableObject only. </summary>
        public virtual void OnGroundCheckValidate() {  }
        #region Collider 3D Methods
        ///<summary>Called in OnCollisionEnter() on the GameObject with the GroundCheck component. </summary>
        public virtual void OnCollisionEnter(Collision other) {  }
        ///<summary>Called in OnCollisionStay() on the GameObject with the GroundCheck component. </summary>
        public virtual void OnCollisionStay(Collision other) {  }
        ///<summary>Called in OnCollisionExit() on the GameObject with the GroundCheck component. </summary>
        public virtual void OnCollisionExit(Collision other) {  }
        ///<summary>Called in OnTriggerEnter() on the GameObject with the GroundCheck component. </summary>
        public virtual void OnTriggerEnter(Collider other) {  }
        ///<summary>Called in OnTriggerStay() on the GameObject with the GroundCheck component. </summary>
        public virtual void OnTriggerStay(Collider other) {  }
        ///<summary>Called in OnTriggerExit() on the GameObject with the GroundCheck component. </summary>
        public virtual void OnTriggerExit(Collider other) {  }
        #endregion
        #region  Collider 2D Methods
        ///<summary>Called in OnCollisionEnter2D() on the GameObject with the GroundCheck component. </summary>
        public virtual void OnCollisionEnter2D(Collision2D other) {  }
        ///<summary>Called in OnCollisionStay2D() on the GameObject with the GroundCheck component. </summary>
        public virtual void OnCollisionStay2D(Collision2D other) {  }
        ///<summary>Called in OnCollisionExit2D() on the GameObject with the GroundCheck component. </summary>
        public virtual void OnCollisionExit2D(Collision2D other) {  }
        ///<summary>Called in OnTriggerEnter2D() on the GameObject with the GroundCheck component. </summary>
        public virtual void OnTriggerEnter2D(Collider2D other) {  }
        ///<summary>Called in OnTriggerStay2D() on the GameObject with the GroundCheck component. </summary>
        public virtual void OnTriggerStay2D(Collider2D other) {  }
        ///<summary>Called in OnTriggerExit2D() on the GameObject with the GroundCheck component. </summary>
        public virtual void OnTriggerExit2D(Collider2D other) {  }
        #endregion
        
    }

}