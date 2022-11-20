/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace W1LGraph.ForceDirected
{
    /// <summary>
    /// Contains settings that can be applied to the Force-directed algorithm.
    /// </summary>
    public class FGSettings
    {
        #region Public attributes
        public float Stiffness { get; set; } = 81.76f;

        public float Repulsion { get; set; } = 40000f;

        public float Damping { get; set; } = 0.5f;

        public float Threadshold { get; set; } = 0.1f;
        #endregion

        #region Private attributes

        #endregion

        #region Constructors

        #endregion

        #region Public members

        #endregion

        #region Private members

        #endregion
    }
}
