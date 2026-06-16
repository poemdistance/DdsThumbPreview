<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TexPatcher
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
        imfPatched = New ImageFileControl()
        imfPatch = New ImageFileControl()
        imfSource = New ImageFileControl()
        bApplyPatches = New Button()
        chkCompressDds = New CheckBox()
        cbPatches = New ComboBox()
        chkCloseWhenFinished = New CheckBox()
        MenuBar = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        ExitToolStripMenuItem = New ToolStripMenuItem()
        ToolsToolStripMenuItem = New ToolStripMenuItem()
        OpenCreatePatchToolToolStripMenuItem = New ToolStripMenuItem()
        HelpToolStripMenuItem = New ToolStripMenuItem()
        AboutImagePatcherToolStripMenuItem = New ToolStripMenuItem()
        ContributeToolStripMenuItem = New ToolStripMenuItem()
        PatreonToolStripMenuItem = New ToolStripMenuItem()
        PaypalToolStripMenuItem = New ToolStripMenuItem()
        Label1 = New Label()
        MenuBar.SuspendLayout()
        SuspendLayout()
        ' 
        ' imfPatched
        ' 
        imfPatched.FileFilter = "DDS and PNG Files|*.dds;*.png"
        imfPatched.ImageFile = "patched.png"
        imfPatched.ImageFileLabel = "Patched"
        imfPatched.IsOutput = True
        imfPatched.Location = New Point(349, 27)
        imfPatched.Margin = New Padding(4)
        imfPatched.Name = "imfPatched"
        imfPatched.Size = New Size(167, 150)
        imfPatched.TabIndex = 4
        ' 
        ' imfPatch
        ' 
        imfPatch.FileFilter = "PNG Files|*.png"
        imfPatch.ImageFile = ""
        imfPatch.ImageFileLabel = "Patch"
        imfPatch.IsOutput = False
        imfPatch.Location = New Point(177, 27)
        imfPatch.Margin = New Padding(4)
        imfPatch.Name = "imfPatch"
        imfPatch.Size = New Size(167, 125)
        imfPatch.TabIndex = 2
        ' 
        ' imfSource
        ' 
        imfSource.FileFilter = "DDS and PNG Files|*.dds;*.png"
        imfSource.ImageFile = ""
        imfSource.ImageFileLabel = "Source"
        imfSource.IsOutput = False
        imfSource.Location = New Point(5, 27)
        imfSource.Margin = New Padding(4)
        imfSource.Name = "imfSource"
        imfSource.Size = New Size(167, 150)
        imfSource.TabIndex = 1
        ' 
        ' bApplyPatches
        ' 
        bApplyPatches.Location = New Point(530, 119)
        bApplyPatches.Margin = New Padding(2)
        bApplyPatches.Name = "bApplyPatches"
        bApplyPatches.Size = New Size(197, 33)
        bApplyPatches.TabIndex = 7
        bApplyPatches.Text = "Apply Patch"
        bApplyPatches.UseVisualStyleBackColor = True
        ' 
        ' chkCompressDds
        ' 
        chkCompressDds.AutoSize = True
        chkCompressDds.Checked = True
        chkCompressDds.CheckState = CheckState.Checked
        chkCompressDds.Location = New Point(530, 63)
        chkCompressDds.Margin = New Padding(2)
        chkCompressDds.Name = "chkCompressDds"
        chkCompressDds.Size = New Size(266, 21)
        chkCompressDds.TabIndex = 5
        chkCompressDds.Text = "Compress as BC7 (Only for .dds outputs)"
        chkCompressDds.UseVisualStyleBackColor = True
        ' 
        ' cbPatches
        ' 
        cbPatches.DropDownStyle = ComboBoxStyle.DropDownList
        cbPatches.FormattingEnabled = True
        cbPatches.Location = New Point(177, 158)
        cbPatches.Margin = New Padding(2)
        cbPatches.Name = "cbPatches"
        cbPatches.Size = New Size(167, 25)
        cbPatches.TabIndex = 3
        ' 
        ' chkCloseWhenFinished
        ' 
        chkCloseWhenFinished.AutoSize = True
        chkCloseWhenFinished.Checked = True
        chkCloseWhenFinished.CheckState = CheckState.Checked
        chkCloseWhenFinished.Location = New Point(530, 84)
        chkCloseWhenFinished.Margin = New Padding(2)
        chkCloseWhenFinished.Name = "chkCloseWhenFinished"
        chkCloseWhenFinished.Size = New Size(147, 21)
        chkCloseWhenFinished.TabIndex = 6
        chkCloseWhenFinished.Text = "Close When Finished"
        chkCloseWhenFinished.UseVisualStyleBackColor = True
        ' 
        ' MenuBar
        ' 
        MenuBar.ImageScalingSize = New Size(24, 24)
        MenuBar.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem, ToolsToolStripMenuItem, HelpToolStripMenuItem})
        MenuBar.Location = New Point(0, 0)
        MenuBar.Name = "MenuBar"
        MenuBar.Padding = New Padding(5, 2, 0, 2)
        MenuBar.Size = New Size(770, 25)
        MenuBar.TabIndex = 15
        MenuBar.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ExitToolStripMenuItem})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(39, 21)
        FileToolStripMenuItem.Text = "File"
        ' 
        ' ExitToolStripMenuItem
        ' 
        ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        ExitToolStripMenuItem.Size = New Size(96, 22)
        ExitToolStripMenuItem.Text = "Exit"
        ' 
        ' ToolsToolStripMenuItem
        ' 
        ToolsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OpenCreatePatchToolToolStripMenuItem})
        ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        ToolsToolStripMenuItem.Size = New Size(52, 21)
        ToolsToolStripMenuItem.Text = "Tools"
        ' 
        ' OpenCreatePatchToolToolStripMenuItem
        ' 
        OpenCreatePatchToolToolStripMenuItem.Name = "OpenCreatePatchToolToolStripMenuItem"
        OpenCreatePatchToolToolStripMenuItem.Size = New Size(211, 22)
        OpenCreatePatchToolToolStripMenuItem.Text = "Open CreatePatch Tool"
        ' 
        ' HelpToolStripMenuItem
        ' 
        HelpToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {AboutImagePatcherToolStripMenuItem})
        HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        HelpToolStripMenuItem.Size = New Size(47, 21)
        HelpToolStripMenuItem.Text = "Help"
        ' 
        ' AboutImagePatcherToolStripMenuItem
        ' 
        AboutImagePatcherToolStripMenuItem.Name = "AboutImagePatcherToolStripMenuItem"
        AboutImagePatcherToolStripMenuItem.Size = New Size(195, 22)
        AboutImagePatcherToolStripMenuItem.Text = "About ImagePatcher"
        ' 
        ' ContributeToolStripMenuItem
        ' 
        ContributeToolStripMenuItem.Name = "ContributeToolStripMenuItem"
        ContributeToolStripMenuItem.Size = New Size(32, 19)
        ' 
        ' PatreonToolStripMenuItem
        ' 
        PatreonToolStripMenuItem.Name = "PatreonToolStripMenuItem"
        PatreonToolStripMenuItem.Size = New Size(32, 19)
        ' 
        ' PaypalToolStripMenuItem
        ' 
        PaypalToolStripMenuItem.Name = "PaypalToolStripMenuItem"
        PaypalToolStripMenuItem.Size = New Size(32, 19)
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(530, 27)
        Label1.Margin = New Padding(2, 0, 2, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(98, 17)
        Label1.TabIndex = 16
        Label1.Text = "Output Options"
        ' 
        ' TexPatcher
        ' 
        AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(770, 193)
        Controls.Add(Label1)
        Controls.Add(chkCloseWhenFinished)
        Controls.Add(cbPatches)
        Controls.Add(chkCompressDds)
        Controls.Add(imfPatched)
        Controls.Add(imfPatch)
        Controls.Add(imfSource)
        Controls.Add(bApplyPatches)
        Controls.Add(MenuBar)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MainMenuStrip = MenuBar
        Margin = New Padding(2)
        MaximizeBox = False
        Name = "TexPatcher"
        StartPosition = FormStartPosition.CenterScreen
        Text = "[XMH] TexPatcher"
        MenuBar.ResumeLayout(False)
        MenuBar.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents imfPatched As ImageFileControl
    Friend WithEvents imfPatch As ImageFileControl
    Friend WithEvents imfSource As ImageFileControl
    Friend WithEvents bApplyPatches As Button
    Friend WithEvents chkCompressDds As CheckBox
    Friend WithEvents cbPatches As ComboBox
    Friend WithEvents chkCloseWhenFinished As CheckBox
    Friend WithEvents MenuBar As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenCreatePatchToolToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutImagePatcherToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SupportUsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PatreonToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PaypalToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContributeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label1 As Label
End Class
