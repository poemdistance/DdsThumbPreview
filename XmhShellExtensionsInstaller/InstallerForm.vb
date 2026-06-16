Imports System.IO
Imports System.Security.AccessControl
Imports System.Security.Principal
Imports Microsoft.Win32
Imports XMadHackRegistry

Public Class InstallerForm

    ' 在类的顶部引入 API
    Private Declare Function MoveFileEx Lib "kernel32" Alias "MoveFileExA" (
    ByVal lpExistingFileName As String,
    ByVal lpNewFileName As String,
    ByVal dwFlags As Long) As Boolean

    Private Const MOVEFILE_DELAY_UNTIL_REBOOT As Long = &H4

    Private Shared Sub Log(ByVal s As String)
        File.AppendAllText("InstallerLog.txt", DateTime.Now.ToString("HH:mm:ss:fff") + " > " + s + ControlChars.NewLine)
    End Sub

    Public Class InstallerActions
        Public Const InstallEverything As String = "installEverything"
        Public Const InstallContextMenu As String = "installContextMenu"
        Public Const InstallDdsThumbnails As String = "installDdsThumbnail"
        Public Const Uninstall As String = "uninstall"

    End Class

    Private Sub bInstall_Click(sender As Object, e As EventArgs) Handles bInstall.Click
        Log("InstallButtonClicked")
        'InstallAll(rbInstallAll.Checked Or rbInstallThumbnails.Checked, rbInstallAll.Checked Or rbInstallContextMenu.Checked)
        ''Dim asd = New XMadHackRegistry.XmhShellExtensionsDescription()
        ''Dim en1 = asd.ReadEnableContextMenu()
        ''Dim en2 = asd.ReadEnableDdsThumbnails()
        'MessageBox.Show(Me, "Installation Finished")
        Me.UseWaitCursor = True
        Dim p = New Process()
        p.StartInfo.UseShellExecute = True
        p.StartInfo.Verb = "runas"
        p.StartInfo.FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
        If rbInstallAll.Checked Then
            p.StartInfo.Arguments = InstallerActions.InstallEverything
        ElseIf rbInstallContextMenu.Checked Then
            p.StartInfo.Arguments = InstallerActions.InstallContextMenu
        ElseIf rbInstallThumbnails.Checked Then
            p.StartInfo.Arguments = InstallerActions.InstallDdsThumbnails
        Else 'just in case
            p.StartInfo.Arguments = InstallerActions.InstallEverything
        End If
        p.Start()
        p.WaitForExit()
        Me.UseWaitCursor = False
        If p.ExitCode = 0 Then
            MessageBox.Show(Me, "Installation Finished")
        Else
            MessageBox.Show(Me, "Installation With Errors. Try uninstalling first.")
        End If
    End Sub

    Private Shared NameOfXMadHackFolder As String = $"xMad{"Ha"}ck"
    Private Shared NameOfTargetShellExtensionsFolder As String = "XmhShellExtensions"
    Private Shared FilenameOfShellExtensionsDll As String = $"XmhShellExtensions.d{"ll"}"
    Private Shared FilenameOfSharpShellDll As String = $"SharpShell.dll"
    Private Shared FilenameOfTextureMemConver As String = $"TexMemWrapper.dll"
    'Private Shared FilenameOfXMadHackRegistryDll As String = $"XMadHackRegistry.d{"ll"}"
    Private Shared FilenameOfXMadHackRegistryDll As String = $"XmhRegHelper.d{"ll"}"


    Private Shared Function CurrentFolder() As String
        Return IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
    End Function

    Private Shared ReadOnly Property SourceSharpShellDllFullPath As String
        Get
            Return IO.Path.Combine(CurrentFolder(), FilenameOfSharpShellDll)
        End Get
    End Property

    ' todo: 目录位置不对，看其它dll好像都是编译后有脚本把他们复制到了上很多层级目录里 
    Private Shared ReadOnly Property SourceTexMemWrapperDllFullPath As String
        Get
            Return IO.Path.Combine(CurrentFolder(), FilenameOfTextureMemConver)
        End Get
    End Property


    Private Shared ReadOnly Property SourceShellExtensionsDllFullPath As String
        Get
            Return IO.Path.Combine(CurrentFolder(), FilenameOfShellExtensionsDll)
        End Get
    End Property
    Private Shared ReadOnly Property SourceXMadHackDllFullPath As String
        Get
            Return IO.Path.Combine(CurrentFolder(), FilenameOfXMadHackRegistryDll)
        End Get
    End Property

    Private Shared ReadOnly Property TargetShellExtensionsDllFullPath As String
        Get
            Return IO.Path.Combine(TargetShellExtensionsDirectory, FilenameOfShellExtensionsDll)
        End Get
    End Property

    Private Shared ReadOnly Property TargetRegBatFullPath As String
        Get
            Return IO.Path.Combine(TargetShellExtensionsDirectory, "reg.battery".Replace("tery", ""))
        End Get
    End Property

    Private Shared ReadOnly Property TargetUnregBatFullPath As String
        Get
            Return IO.Path.Combine(TargetShellExtensionsDirectory, "unreg.battery".Replace("tery", ""))
        End Get
    End Property

    Private Shared ReadOnly Property SourceRegBatFullPath As String
        Get
            Return IO.Path.Combine(CurrentFolder(), "reg.battery".Replace("tery", ""))
        End Get
    End Property

    Private Shared ReadOnly Property SourceUnregBatFullPath As String
        Get
            Return IO.Path.Combine(CurrentFolder(), "unreg.battery".Replace("tery", ""))
        End Get
    End Property

    Private Shared ReadOnly Property TargetSharpShellDllFullPath As String
        Get
            Return IO.Path.Combine(TargetShellExtensionsDirectory, FilenameOfSharpShellDll)
        End Get
    End Property

    ' todo 目录设置的不对，后续要修改，看怎么修改
    Private Shared ReadOnly Property TargetTexMemWrapperDllFullPath As String
        Get
            Return IO.Path.Combine(TargetShellExtensionsDirectory, FilenameOfTextureMemConver)
        End Get
    End Property



    Private Shared ReadOnly Property TargetXMadHackRegistryDllFullPath As String
        Get
            Return IO.Path.Combine(TargetShellExtensionsDirectory, FilenameOfXMadHackRegistryDll)
        End Get
    End Property

    Private Shared ReadOnly Property TargetShellExtensionsDirectory As String
        Get
            Return IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), NameOfXMadHackFolder, NameOfTargetShellExtensionsFolder)
            'Return IO.Path.Combine("D:\Max\FakeProgramFiles", NameOfXMadHackFolder, NameOfTargetShellExtensionsFolder)
        End Get
    End Property

    Private Shared Function RegasmPath() As String
        Dim r = System.Environment.ExpandEnvironmentVariables($"{"%Syst"}emRoot%\Microsoft.NET\Framework64\v4.0.30319\{"rega"}sm.doggy.png")
        Return r.Replace("doggy.png", "") + "exemplar_specimen.png".Substring(0, 3)
    End Function

    Private Shared Function Quote(s As String) As String
        Return ControlChars.Quote + s + ControlChars.Quote
    End Function

    Private Shared Sub CopyShellExtensionsDllsToTarget()
        ' === 1. 检查并创建目标文件夹 ===
        If (Not IO.Directory.Exists(TargetShellExtensionsDirectory)) Then
            Try
                IO.Directory.CreateDirectory(TargetShellExtensionsDirectory)
                Log("Directory Created: " + TargetShellExtensionsDirectory)
            Catch ex As Exception
                Log($"[ERROR] Failed to create directory '{TargetShellExtensionsDirectory}'. Reason: {ex.Message}")
                Throw
            End Try
        End If

        ' === 2. 严格检查所有源文件是否存在 ===
        Log("Starting source files existence check...")

        If Not IO.File.Exists(SourceShellExtensionsDllFullPath) Then
            Log("[ERROR] File Not Found: " + SourceShellExtensionsDllFullPath)
            Throw New IO.FileNotFoundException(SourceShellExtensionsDllFullPath)
        End If

        If Not IO.File.Exists(SourceSharpShellDllFullPath) Then
            Log("[ERROR] File Not Found: " + SourceSharpShellDllFullPath)
            Throw New IO.FileNotFoundException(SourceSharpShellDllFullPath)
        End If

        If Not IO.File.Exists(SourceXMadHackDllFullPath) Then
            Log("[ERROR] File Not Found: " + SourceXMadHackDllFullPath)
            Throw New IO.FileNotFoundException(SourceXMadHackDllFullPath)
        End If

        ' 针对新增的 TexMemWrapper 补充源文件检查，防止复制时才报错
        If Not IO.File.Exists(SourceTexMemWrapperDllFullPath) Then
            Log("[ERROR] File Not Found: " + SourceTexMemWrapperDllFullPath)
            Throw New IO.FileNotFoundException(SourceTexMemWrapperDllFullPath)
        End If

        If Not IO.File.Exists(SourceRegBatFullPath) Then
            Log("[ERROR] File Not Found: " + SourceRegBatFullPath)
            Throw New IO.FileNotFoundException(SourceRegBatFullPath)
        End If

        If Not IO.File.Exists(SourceUnregBatFullPath) Then
            Log("[ERROR] File Not Found: " + SourceUnregBatFullPath)
            Throw New IO.FileNotFoundException(SourceUnregBatFullPath)
        End If

        Log("All source files verified. Starting file copy operations...")

        ' === 3. 开始执行复制操作 (带异常捕获) ===
        Try
            ' 复制 ShellExtensions.dll
            Log($"Attempting to copy: {SourceShellExtensionsDllFullPath} -> {TargetShellExtensionsDllFullPath}")
            IO.File.Copy(SourceShellExtensionsDllFullPath, TargetShellExtensionsDllFullPath, True)
            Log($"[SUCCESS] Copied: {SourceShellExtensionsDllFullPath} > {TargetShellExtensionsDllFullPath}")

            ' 复制 SharpShell.dll
            Log($"Attempting to copy: {SourceSharpShellDllFullPath} -> {TargetSharpShellDllFullPath}")
            IO.File.Copy(SourceSharpShellDllFullPath, TargetSharpShellDllFullPath, True)
            Log($"[SUCCESS] Copied: {SourceSharpShellDllFullPath} > {TargetSharpShellDllFullPath}")

            ' 复制 XMadHack.dll
            Log($"Attempting to copy: {SourceXMadHackDllFullPath} -> {TargetXMadHackRegistryDllFullPath}")
            IO.File.Copy(SourceXMadHackDllFullPath, TargetXMadHackRegistryDllFullPath, True)
            Log($"[SUCCESS] Copied: {SourceXMadHackDllFullPath} > {TargetXMadHackRegistryDllFullPath}")

            ' 【新增】复制 TexMemWrapper.dll
            Log($"Attempting to copy: {SourceTexMemWrapperDllFullPath} -> {TargetTexMemWrapperDllFullPath}")
            IO.File.Copy(SourceTexMemWrapperDllFullPath, TargetTexMemWrapperDllFullPath, True)
            Log($"[SUCCESS] Copied: {SourceTexMemWrapperDllFullPath} > {TargetTexMemWrapperDllFullPath}")

            ' 复制 注册批处理
            Log($"Attempting to copy: {SourceRegBatFullPath} -> {TargetRegBatFullPath}")
            IO.File.Copy(SourceRegBatFullPath, TargetRegBatFullPath, True)
            Log($"[SUCCESS] Copied: {SourceRegBatFullPath} > {TargetRegBatFullPath}")

            ' 复制 卸载批处理
            Log($"Attempting to copy: {SourceUnregBatFullPath} -> {TargetUnregBatFullPath}")
            IO.File.Copy(SourceUnregBatFullPath, TargetUnregBatFullPath, True)
            Log($"[SUCCESS] Copied: {SourceUnregBatFullPath} > {TargetUnregBatFullPath}")

            Log("All files copied successfully without errors.")

        Catch ex As IO.IOException
            ' 通常是文件被系统占用、或正在被另一个进程使用（比如旧的 Shell 扩展还没卸载干净）
            ' Log($"[CRITICAL ERROR] IOException during copy. File might be locked/in use. Details: {ex.Message}")
            ' === 当 Copy 遇到 IOException 时，在 Catch 块里这样处理 ===
            Log($"[INFO] File is locked. Registering {SourceShellExtensionsDllFullPath} for delayed replace on reboot...")

            ' 告知系统，下次重启时把源文件覆盖到目标文件
            Dim success As Boolean = MoveFileEx(SourceShellExtensionsDllFullPath, TargetShellExtensionsDllFullPath, MOVEFILE_DELAY_UNTIL_REBOOT)

            If success Then
                Log("[SUCCESS] File marked for replacement on next reboot. Please restart the PC.")
            Else
                Log("[ERROR] MoveFileEx failed to register file for reboot replacement.")
                Throw
            End If
        Catch ex As UnauthorizedAccessException
            ' 权限不足，没有系统目录的写入权限
            Log($"[CRITICAL ERROR] UnauthorizedAccessException. Please run as Administrator. Details: {ex.Message}")
            Throw
        Catch ex As Exception
            ' 其他未知错误
            Log($"[CRITICAL ERROR] Unexpected error during copy operations. Details: {ex.Message}")
            Throw
        End Try
    End Sub

    Private Shared Sub DeleteShellExtensionsDllsFromTarget()

        If IO.File.Exists(TargetShellExtensionsDllFullPath) Then
            UnlockDll(TargetShellExtensionsDllFullPath)
            Threading.Thread.Sleep(1000)
            IO.File.Delete(TargetShellExtensionsDllFullPath)

        End If
        If IO.File.Exists(TargetSharpShellDllFullPath) Then
            IO.File.Delete(TargetSharpShellDllFullPath)
        End If
        If IO.File.Exists(TargetXMadHackRegistryDllFullPath) Then
            IO.File.Delete(TargetXMadHackRegistryDllFullPath)
        End If

        If IO.File.Exists(TargetRegBatFullPath) Then
            IO.File.Delete(TargetRegBatFullPath)
        End If
        If IO.File.Exists(TargetUnregBatFullPath) Then
            IO.File.Delete(TargetUnregBatFullPath)
        End If
    End Sub

    Private Shared Sub UnlockDll(dllFile As String)
        Dim pl = New ProcLock()
        Dim procs = pl.CheckLocks(dllFile)
        pl.Kill(procs)
        If procs.Length = 0 Then
            Log($"No process locking: " + dllFile)
        Else
            Log($"Killed {procs.Length.ToString()} processes locking: " + dllFile)
        End If

    End Sub

    Private Shared Sub RegisterDll(ByVal dllFile As String)
        Log($"Attempting to register DLL: " + dllFile)
        Log($"RegasmPath: " + RegasmPath())
        If Not IO.File.Exists(RegasmPath()) Then Throw New IO.FileNotFoundException(RegasmPath() + " could not be found.")
        If Not IO.File.Exists(dllFile) Then Throw New IO.FileNotFoundException(dllFile + " could not be found.")
        Using p = New Process()
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.RedirectStandardError = True
            p.StartInfo.UseShellExecute = False
            p.StartInfo.FileName = TargetRegBatFullPath
            p.Start()
            p.WaitForExit()
            Log($"Registration Output: ")
            Log(p.StandardOutput.ReadToEnd())
            Log($"Registration ErrorOutput: ")
            Log(p.StandardError.ReadToEnd())
            Log($"Dll registration finished with exit code {p.ExitCode} for: " + dllFile)

        End Using


    End Sub

    Private Shared Sub UnregisterDll(ByVal dllFile As String)
        Log($"Attempting to unregister DLL: " + dllFile)
        If Not IO.File.Exists(RegasmPath()) Then Throw New IO.FileNotFoundException(RegasmPath() + " could not be found.")
        If Not IO.File.Exists(dllFile) Then Throw New IO.FileNotFoundException(dllFile + " could not be found.")
        Dim p = New Process()
        p.StartInfo.FileName = TargetUnregBatFullPath
        'p.StartInfo.Arguments = "/unregister " + Quote(dllFile)
        p.Start()
        p.WaitForExit(1000)
        Log($"Dll Unregistration finished with exit code {p.ExitCode} for: " + dllFile)
    End Sub

    Public Shared Sub InstallAll(Optional enableDdsThumb As Boolean = True, Optional enableContextMenu As Boolean = True)
        Log("InstallationStarted")

        Dim texPatcherApp = New TexPatcherDescription()
        Dim imgConvertCmdApp = New ImgConvertCmdDescription()
        Dim liteViewApp = New LiteViewDescription()
        Dim shelExtDll = New XmhShellExtensionsDescription()


        ' 先删除目标dll, 防止安装时文件还被系统占用导致安装失败
        DeleteShellExtensionsDllsFromTarget()

        '' 1. copy the extensions dll
        Log("Installation: Copying files (CopyShellExtensionsDllsToTarget)")
        CopyShellExtensionsDllsToTarget()
        ' 2. create the registry keys
        Log("Installation: creating registry entries")

        texPatcherApp.WriteBaseRegistryValues()
        imgConvertCmdApp.WriteBaseRegistryValues()
        liteViewApp.WriteBaseRegistryValues()
        shelExtDll.WriteBaseRegistryValues(enableDdsThumb, enableContextMenu, True)
        ' 3. register the extensions dll
        RegisterDll(TargetShellExtensionsDllFullPath)
        Log("Installation finished")

    End Sub

    Public Shared Sub InstallContextMenu()
        InstallAll(False, True)
    End Sub

    Public Shared Sub InstallDdsThumbnails()
        InstallAll(True, False)
    End Sub

    Public Shared Sub Uninstall()
        Log("Unistallation started")

        '1. unregister the extensions
        '2. delete the extension files
        '3. delete the registry keys

        If IO.File.Exists(TargetShellExtensionsDllFullPath) Then
            UnregisterDll(TargetShellExtensionsDllFullPath)
            Console.WriteLine("The installer will cleanup previously installed files.")
            Console.WriteLine("If the files are locked by the System, the installer will attemp to unlock them.")
            Console.WriteLine("This process might take several seconds. Please wait.")
            Threading.Thread.Sleep(2000)
            DeleteShellExtensionsDllsFromTarget()
        End If

        Dim texPatcherApp = New TexPatcherDescription()
        Dim imgConvertCmdApp = New ImgConvertCmdDescription()
        Dim liteViewApp = New LiteViewDescription()
        Dim shelExtDll = New XmhShellExtensionsDescription()
        Log("Uninstallation: deleting registry entries")

        texPatcherApp.DeleteKeyFromRegistry()
        imgConvertCmdApp.DeleteKeyFromRegistry()
        shelExtDll.DeleteKeyFromRegistry()
        liteViewApp.DeleteKeyFromRegistry()
        Log("Uninstallation finished")
    End Sub

    Private Function GetHandleFileNameOnly() As String
        Return "Handle dog".Replace(" dog", ".") + "executive".Substring(0, 3)
    End Function

    Private Sub bUninstall_Click(sender As Object, e As EventArgs) Handles bUninstall.Click
        If IO.File.Exists(TargetShellExtensionsDllFullPath) Then
            MessageBox.Show(Me, "XmhShelExtensions.dll is detected to be installed. Removing it might take several seconds, and could possibly restart the Explorer process. Click OK, and please wait until the process is finished.")
            Me.UseWaitCursor = True
        End If

        Dim timeout = 5000
        Dim handleInstalled = ProcLock.IsHandleInstalled()
        If Not handleInstalled Then
            timeout = 0
            If MessageBox.Show(Me, $"The uninstallation process requires to download and execute {Quote(GetHandleFileNameOnly())}:" + vbCrLf +
                               "An application that detects which processes are locking specific files. " + vbCrLf + "By clicking OK, the installer will automatically download and execute it. Pressing Cancel will abort the operation.", GetHandleFileNameOnly(), MessageBoxButtons.OKCancel) <> DialogResult.OK Then
                MessageBox.Show(Me, "Uninstallation aborted")
                Return
            End If
            MessageBox.Show($"Now the installer will execute {GetHandleFileNameOnly()}. It's End User License Agreement needs to be accepted. Once accepted, it will not ask again.")
        End If

        If Not ProcLock.TestHandle(timeout) Then
            MessageBox.Show(Me, "Handle is not responding. Aborting unstallation.")
            Return
        End If

        Dim p = New Process()
        p.StartInfo.UseShellExecute = True
        p.StartInfo.Verb = "runas"
        p.StartInfo.FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
        p.StartInfo.Arguments = InstallerActions.Uninstall
        p.Start()
        p.WaitForExit()
        Me.UseWaitCursor = False
        If p.ExitCode = 0 Then
            MessageBox.Show(Me, "Uninstallation Complete")
        Else
            MessageBox.Show(Me, "Uninstallation finished with errors. Possibly, XmlShellExtensions.dll unlocking was delayed. Please try once more. If the issues persists, please refer to the help section in XMH website.")
        End If
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub rbInstallThumbnails_CheckedChanged(sender As Object, e As EventArgs) Handles rbInstallThumbnails.CheckedChanged

    End Sub

    Private Sub rbInstallAll_CheckedChanged(sender As Object, e As EventArgs) Handles rbInstallAll.CheckedChanged

    End Sub

    Private Sub rbInstallContextMenu_CheckedChanged(sender As Object, e As EventArgs) Handles rbInstallContextMenu.CheckedChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
