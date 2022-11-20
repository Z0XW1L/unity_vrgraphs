/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Graph
{
    /// <summary>
    /// Contains common utility functions for graphs.
    /// </summary>
    internal class GraphUtils
    {
        #region Public attributes

        #endregion

        #region Private attributes

        #endregion

        #region Constructors

        #endregion

        #region Public members
        public static Vector3 v2u(System.Numerics.Vector3 vec)
        {
            return new Vector3(vec.X, vec.Y, vec.Z);
        }
        public static System.Numerics.Vector3 u2v(Vector3 vec)
        {
            return new System.Numerics.Vector3(vec.x, vec.y, vec.z);
        }
        #endregion

        #region Private members

        #endregion
    }
}
