/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details).
*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.SSH.Datatypes
{
    /// <summary>
    /// Class for externalizing user information (user, password).
    /// TODO: Password should not be stored locally on the disk!
    /// </summary>
    [JsonObject]
    public class UserInfo
    {
        #region Public attributes
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
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
