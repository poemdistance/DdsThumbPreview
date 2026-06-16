<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutXMadHack
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        Label2 = New Label()
        Button1 = New Button()
        llPatreon = New LinkLabel()
        llGithub = New LinkLabel()
        llPaypal = New LinkLabel()
        Label3 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 10)
        Label1.Name = "Label1"
        Label1.Size = New Size(278, 17)
        Label1.TabIndex = 0
        Label1.Text = "xMadHack is creating CG and Modding Tools."
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 46)
        Label2.Name = "Label2"
        Label2.Size = New Size(259, 17)
        Label2.TabIndex = 1
        Label2.Text = "Find the latest news and support the effort:"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(266, 169)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 26)
        Button1.TabIndex = 2
        Button1.Text = "OK"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' llPatreon
        ' 
        llPatreon.AutoSize = True
        llPatreon.Location = New Point(12, 103)
        llPatreon.Name = "llPatreon"
        llPatreon.Size = New Size(221, 17)
        llPatreon.TabIndex = 3
        llPatreon.TabStop = True
        llPatreon.Text = "https://www.patreon.com/xMadHack"
        ' 
        ' llGithub
        ' 
        llGithub.AutoSize = True
        llGithub.Location = New Point(12, 82)
        llGithub.Name = "llGithub"
        llGithub.Size = New Size(319, 17)
        llGithub.TabIndex = 4
        llGithub.TabStop = True
        llGithub.Text = "https://github.com/poemdistance/DdsThumbPreview/"
        ' 
        ' llPaypal
        ' 
        llPaypal.AutoSize = True
        llPaypal.Location = New Point(12, 125)
        llPaypal.Name = "llPaypal"
        llPaypal.Size = New Size(177, 17)
        llPaypal.TabIndex = 5
        llPaypal.TabStop = True
        llPaypal.Text = "https://paypal.me/xMadHack"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(12, 173)
        Label3.Name = "Label3"
        Label3.Size = New Size(193, 17)
        Label3.TabIndex = 6
        Label3.Text = "Contact: xMadHack@gmail.com"
        ' 
        ' AboutXMadHack
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(355, 209)
        Controls.Add(Label3)
        Controls.Add(llPaypal)
        Controls.Add(llGithub)
        Controls.Add(llPatreon)
        Controls.Add(Button1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Name = "AboutXMadHack"
        StartPosition = FormStartPosition.CenterParent
        Text = "About xMadHack"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents llPatreon As LinkLabel
    Friend WithEvents llGithub As LinkLabel
    Friend WithEvents llPaypal As LinkLabel
    Friend WithEvents Label3 As Label
End Class
