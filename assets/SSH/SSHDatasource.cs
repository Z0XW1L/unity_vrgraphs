/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Renci.SshNet;
using TMPro;
using UnityEditor;
using Assets.SSH;
using System.IO;
using Newtonsoft.Json;
using Assets.SSH.Interfaces;
using Assets.SSH.Datatypes;

/// <summary>
/// Connector for using SSH within any given scene.
/// </summary>
public class SSHDatasource : AbstractDatasource<TreeItem>
{
    #region Private attributes
    private ConnectionInfo connectionInfo;
    private SshClient sshClient;
    [SerializeField] private string ssh_userFile = "./ssh.password.json";
    [SerializeField] private int ssh_fileLimit = 30;
    [SerializeField] private int ssh_explorationDepth = 2;
    [SerializeField] private bool ssh_connectOnStart = true;
    private UserInfo userInfo;
    #endregion

    #region Public attributes
    public override bool IsReady => sshClient != null && sshClient.IsConnected;
    #endregion

    #region Constructor
    public SSHDatasource()
    {
        
    }
    #endregion

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        if(ssh_connectOnStart) Connect();
    }
    // Update is called once per frame
    void Update()
    {
        // Empty
    }
    #endregion

    #region Public members

    void OnDestroy()
    {
        if(this.sshClient != null)
        {
            this.sshClient.Dispose();
        }
    }
    /// <summary>
    /// Sets new credentials and connects to the given SSH client.
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <param name="user"></param>
    /// <param name="password"></param>
    public void Connect(string url, string username, string password)
    {
        Connect(new UserInfo()
        {
            Url = url,
            Password = password,
            Username = username
        });
    }
    /// <summary>
    /// Sets new credentials and connects to the given SSH client.
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <param name="user"></param>
    /// <param name="password"></param>
    public void Connect(UserInfo userInfo)
    {
        this.userInfo = userInfo;
        this.Connect();
    }
    /// <summary>
    /// Connects to the given SSH client.
    /// </summary>
    public void Connect()
    {
        if(this.userInfo == null) this.userInfo = JsonConvert.DeserializeObject<UserInfo>(File.ReadAllText(ssh_userFile)); // Please create a file accordingly in case you get an exception!
        this.connectionInfo = new ConnectionInfo(this.userInfo.Url, this.userInfo.Username,
        new PasswordAuthenticationMethod(this.userInfo.Username, this.userInfo.Password),
        new PrivateKeyAuthenticationMethod("rsa.key"));
        this.sshClient = new SshClient(this.connectionInfo);
        this.sshClient.Connect();
    }
    /// <summary>
    /// Applies any given ConnnectionInfo to this object.
    /// </summary>
    /// <param name="connectionInfo"></param>
    public void Connect(ConnectionInfo connectionInfo)
    {
        this.connectionInfo = connectionInfo;
    }
    /// <summary>
    /// Executes the given command within the opened SSH connection.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public string Cmd(string command)
    {
        return this.sshClient.CreateCommand(command).Execute();
    }
    /// <summary>
    /// Executes the command and parses it into the specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="command"></param>
    /// <returns></returns>
    public T Cmd<T>(string command)
    {
        return JsonConvert.DeserializeObject<T>(this.Cmd(command));
    }

    public override T GetData<T>(TreeItem arguments)
    {
        string fileLimit = ssh_fileLimit > 0 ? "--filelimit " + ssh_fileLimit : string.Empty;
        string level = ssh_explorationDepth > 0 ? "-L " + ssh_explorationDepth : string.Empty;
        if (arguments == null)
        {
            return JsonConvert.DeserializeObject<T>(Cmd(string.Format("tree -d -J {0} {1} --noreport", fileLimit, level)));
        }
        else
        {
            string path = arguments.Name;
            TreeItem it = arguments;
            while((it = it.Parent) != null)
            {
                path = it.Name + "/" + path;
            }
            return JsonConvert.DeserializeObject<T>(Cmd(string.Format("tree {0} -J {1} -L 1 --noreport", path, fileLimit, level)));
        }
    }
    #endregion

    #region Private members
    #endregion
}
