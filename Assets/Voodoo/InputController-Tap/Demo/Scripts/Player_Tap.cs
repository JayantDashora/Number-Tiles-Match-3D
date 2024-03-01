using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voodoo.Controls
{

    public class Player_Tap : MonoBehaviour, ITapController
    {
        private Transform m_Transform;

        void Awake()
        {
            m_Transform = transform;
        }

        #region ISwipe impl

        public void OnTap(Vector3 _Pos)
        {
            m_Transform.position = _Pos;
        }

        #endregion
    }
}