<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class InstallerForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(InstallerForm))
        bInstall = New Button()
        bUninstall = New Button()
        GroupBox1 = New GroupBox()
        rbInstallAll = New RadioButton()
        rbInstallThumbnails = New RadioButton()
        rbInstallContextMenu = New RadioButton()
        Label1 = New Label()
        GroupBox2 = New GroupBox()
        Label2 = New Label()
        Label3 = New Label()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' bInstall
        ' 
        bInstall.Location = New Point(370, 51)
        bInstall.Name = "bInstall"
        bInstall.Size = New Size(75, 26)
        bInstall.TabIndex = 0
        bInstall.Text = "Install"
        bInstall.UseVisualStyleBackColor = True
        ' 
        ' bUninstall
        ' 
        bUninstall.Location = New Point(6, 37)
        bUninstall.Name = "bUninstall"
        bUninstall.Size = New Size(172, 26)
        bUninstall.TabIndex = 1
        bUninstall.Text = "Uninstall Everything"
        bUninstall.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(rbInstallAll)
        GroupBox1.Controls.Add(rbInstallThumbnails)
        GroupBox1.Controls.Add(rbInstallContextMenu)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(bInstall)
        GroupBox1.Location = New Point(12, 14)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(461, 212)
        GroupBox1.TabIndex = 2
        GroupBox1.TabStop = False
        GroupBox1.Text = "Installation options"
        ' 
        ' rbInstallAll
        ' 
        rbInstallAll.AutoSize = True
        rbInstallAll.Location = New Point(6, 80)
        rbInstallAll.Name = "rbInstallAll"
        rbInstallAll.Size = New Size(127, 21)
        rbInstallAll.TabIndex = 4
        rbInstallAll.Text = "Install everything."
        rbInstallAll.UseVisualStyleBackColor = True
        ' 
        ' rbInstallThumbnails
        ' 
        rbInstallThumbnails.AutoSize = True
        rbInstallThumbnails.Checked = True
        rbInstallThumbnails.Location = New Point(6, 22)
        rbInstallThumbnails.Name = "rbInstallThumbnails"
        rbInstallThumbnails.Size = New Size(190, 21)
        rbInstallThumbnails.TabIndex = 3
        rbInstallThumbnails.TabStop = True
        rbInstallThumbnails.Text = "Install only DDS Thumbnails."
        rbInstallThumbnails.UseVisualStyleBackColor = True
        ' 
        ' rbInstallContextMenu
        ' 
        rbInstallContextMenu.AutoSize = True
        rbInstallContextMenu.Location = New Point(6, 53)
        rbInstallContextMenu.Name = "rbInstallContextMenu"
        rbInstallContextMenu.Size = New Size(307, 21)
        rbInstallContextMenu.TabIndex = 2
        rbInstallContextMenu.Text = "Install only Context Menu for DDS and Png Files."
        rbInstallContextMenu.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(6, 113)
        Label1.Name = "Label1"
        Label1.Size = New Size(449, 95)
        Label1.TabIndex = 1
        Label1.Text = "The installation process will ask for Administrator privileges." & vbCrLf & "This will happen:" & vbCrLf & "- Copy dlls to xMadHack folder in Program Files." & vbCrLf & "- Register the dlls." & vbCrLf & "- Add entries to the Windows Registry."
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(Label2)
        GroupBox2.Controls.Add(bUninstall)
        GroupBox2.Location = New Point(12, 286)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(461, 186)
        GroupBox2.TabIndex = 5
        GroupBox2.TabStop = False
        GroupBox2.Text = "Uninstallation"
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(6, 82)
        Label2.Name = "Label2"
        Label2.Size = New Size(449, 95)
        Label2.TabIndex = 1
        Label2.Text = resources.GetString("Label2.Text")
        ' 
        ' Label3
        ' 
        Label3.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point)
        Label3.ForeColor = Color.Crimson
        Label3.Location = New Point(12, 229)
        Label3.Name = "Label3"
        Label3.Size = New Size(449, 39)
        Label3.TabIndex = 2
        Label3.Text = "IMPORTANT: To remove the shell extensions, run this installer and click the Uninstall Everything button."
        ' 
        ' InstallerForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(485, 486)
        Controls.Add(Label3)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "InstallerForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Xmh Shell Extensions Installer"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents bInstall As Button
    Friend WithEvents bUninstall As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbInstallAll As RadioButton
    Friend WithEvents rbInstallThumbnails As RadioButton
    Friend WithEvents rbInstallContextMenu As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
