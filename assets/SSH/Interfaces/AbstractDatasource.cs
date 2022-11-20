/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details).
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.SSH.Interfaces
{
    /// <summary>
    /// Generic data source that con return data in a specific format.
    /// </summary>
    /// <typeparam name="U">Type of data arguments</typeparam>
    public abstract class AbstractDatasource<U> : MonoBehaviour
    {
        /// <summary>
        /// If true, the data source is ready to output data.
        /// </summary>
        public abstract bool IsReady { get; }
        /// <summary>
        /// Returns data in the specified format.
        /// </summary>
        /// <typeparam name="T">Object type to be returned</typeparam>
        /// <param name="arguments">Arguments to configure the data that should be returned</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public abstract T GetData<T>(U arguments);
    }
}
