using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameUI
{
    public class UIHealthBar : MonoBehaviour
    {
        public Transform target = null;
        public Image foregroundImage = null, backgroundImage = null;
        public Vector3 offset;
        private Camera mainCamera = null;
        private RectTransform rectTransform = null;

        void Awake()
        {
            mainCamera = Camera.main;
            rectTransform = GetComponent<RectTransform>();
        }

        void LateUpdate()
        {
            transform.position = mainCamera.WorldToScreenPoint(target.position + offset);
        }

        /// <summary>
        /// updates the health bar according to the health of the character
        /// </summary>
        /// <param name="percentage"></param>
        public void setHealthBarPercentage(float percentage)
        {
            float parentWidth = rectTransform.rect.width;
            float width = parentWidth * percentage;
            foregroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }
    }
}
