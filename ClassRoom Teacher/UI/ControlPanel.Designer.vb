<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ControlPanel
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ControlPanel))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PCOverviewPanel = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UserNameLabel = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.UserProfilePictureBox = New System.Windows.Forms.PictureBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.MainPanel = New System.Windows.Forms.Panel()
        Me.MenuPanel = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.WiFiDisplayPanel = New System.Windows.Forms.Panel()
        Me.WiFiPasswordTextBox = New System.Windows.Forms.TextBox()
        Me.SSIDTextBox = New System.Windows.Forms.TextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.IPAddressTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.UserProfilePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.MainPanel.SuspendLayout()
        Me.MenuPanel.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.WiFiDisplayPanel.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.Panel1.Controls.Add(Me.PCOverviewPanel)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(200, 401)
        Me.Panel1.TabIndex = 2
        '
        'PCOverviewPanel
        '
        Me.PCOverviewPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PCOverviewPanel.Location = New System.Drawing.Point(0, 35)
        Me.PCOverviewPanel.Name = "PCOverviewPanel"
        Me.PCOverviewPanel.Size = New System.Drawing.Size(200, 366)
        Me.PCOverviewPanel.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(200, 35)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Computer"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.Panel2.Controls.Add(Me.UserNameLabel)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(800, 49)
        Me.Panel2.TabIndex = 3
        '
        'UserNameLabel
        '
        Me.UserNameLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserNameLabel.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.UserNameLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(199, Byte), Integer))
        Me.UserNameLabel.Location = New System.Drawing.Point(403, 0)
        Me.UserNameLabel.Name = "UserNameLabel"
        Me.UserNameLabel.Size = New System.Drawing.Size(343, 49)
        Me.UserNameLabel.TabIndex = 3
        Me.UserNameLabel.Text = "Label2"
        Me.UserNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(200, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(203, 49)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "ClassRoom Teacher"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(200, 49)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.UserProfilePictureBox)
        Me.Panel4.Controls.Add(Me.Panel8)
        Me.Panel4.Controls.Add(Me.Panel7)
        Me.Panel4.Controls.Add(Me.Panel6)
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel4.Location = New System.Drawing.Point(746, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(54, 49)
        Me.Panel4.TabIndex = 5
        '
        'UserProfilePictureBox
        '
        Me.UserProfilePictureBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserProfilePictureBox.Location = New System.Drawing.Point(3, 3)
        Me.UserProfilePictureBox.Name = "UserProfilePictureBox"
        Me.UserProfilePictureBox.Size = New System.Drawing.Size(48, 43)
        Me.UserProfilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.UserProfilePictureBox.TabIndex = 4
        Me.UserProfilePictureBox.TabStop = False
        '
        'Panel8
        '
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel8.Location = New System.Drawing.Point(0, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(3, 43)
        Me.Panel8.TabIndex = 8
        '
        'Panel7
        '
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel7.Location = New System.Drawing.Point(51, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(3, 43)
        Me.Panel7.TabIndex = 7
        '
        'Panel6
        '
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, 46)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(54, 3)
        Me.Panel6.TabIndex = 6
        '
        'Panel5
        '
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(54, 3)
        Me.Panel5.TabIndex = 5
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.MainPanel)
        Me.Panel3.Controls.Add(Me.Panel1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 49)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(800, 401)
        Me.Panel3.TabIndex = 4
        '
        'MainPanel
        '
        Me.MainPanel.Controls.Add(Me.MenuPanel)
        Me.MainPanel.Controls.Add(Me.Panel9)
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(200, 0)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Size = New System.Drawing.Size(600, 401)
        Me.MainPanel.TabIndex = 3
        '
        'MenuPanel
        '
        Me.MenuPanel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.MenuPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MenuPanel.Controls.Add(Me.Button4)
        Me.MenuPanel.Controls.Add(Me.Button3)
        Me.MenuPanel.Controls.Add(Me.Button2)
        Me.MenuPanel.Location = New System.Drawing.Point(400, 1)
        Me.MenuPanel.Name = "MenuPanel"
        Me.MenuPanel.Size = New System.Drawing.Size(200, 100)
        Me.MenuPanel.TabIndex = 1
        Me.MenuPanel.Visible = False
        '
        'Button3
        '
        Me.Button3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Location = New System.Drawing.Point(0, 30)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(198, 30)
        Me.Button3.TabIndex = 1
        Me.Button3.Text = "HotSpot Einstellungen"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(0, 0)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(198, 30)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "Uploads Ordner"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.WiFiDisplayPanel)
        Me.Panel9.Controls.Add(Me.IPAddressTextBox)
        Me.Panel9.Controls.Add(Me.Label2)
        Me.Panel9.Location = New System.Drawing.Point(50, 50)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(499, 271)
        Me.Panel9.TabIndex = 0
        '
        'WiFiDisplayPanel
        '
        Me.WiFiDisplayPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.WiFiDisplayPanel.Controls.Add(Me.WiFiPasswordTextBox)
        Me.WiFiDisplayPanel.Controls.Add(Me.SSIDTextBox)
        Me.WiFiDisplayPanel.Controls.Add(Me.PictureBox2)
        Me.WiFiDisplayPanel.Controls.Add(Me.Button1)
        Me.WiFiDisplayPanel.Location = New System.Drawing.Point(19, 64)
        Me.WiFiDisplayPanel.Name = "WiFiDisplayPanel"
        Me.WiFiDisplayPanel.Size = New System.Drawing.Size(461, 124)
        Me.WiFiDisplayPanel.TabIndex = 2
        '
        'WiFiPasswordTextBox
        '
        Me.WiFiPasswordTextBox.BackColor = System.Drawing.SystemColors.Control
        Me.WiFiPasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.WiFiPasswordTextBox.Font = New System.Drawing.Font("Segoe UI", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WiFiPasswordTextBox.Location = New System.Drawing.Point(64, 66)
        Me.WiFiPasswordTextBox.Name = "WiFiPasswordTextBox"
        Me.WiFiPasswordTextBox.ReadOnly = True
        Me.WiFiPasswordTextBox.Size = New System.Drawing.Size(359, 50)
        Me.WiFiPasswordTextBox.TabIndex = 3
        '
        'SSIDTextBox
        '
        Me.SSIDTextBox.BackColor = System.Drawing.SystemColors.Control
        Me.SSIDTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SSIDTextBox.Font = New System.Drawing.Font("Segoe UI", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSIDTextBox.Location = New System.Drawing.Point(64, 7)
        Me.SSIDTextBox.Name = "SSIDTextBox"
        Me.SSIDTextBox.ReadOnly = True
        Me.SSIDTextBox.Size = New System.Drawing.Size(359, 50)
        Me.SSIDTextBox.TabIndex = 2
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(41, 116)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(425, 96)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(31, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'IPAddressTextBox
        '
        Me.IPAddressTextBox.BackColor = System.Drawing.SystemColors.Control
        Me.IPAddressTextBox.Font = New System.Drawing.Font("Segoe UI", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IPAddressTextBox.Location = New System.Drawing.Point(19, 194)
        Me.IPAddressTextBox.Name = "IPAddressTextBox"
        Me.IPAddressTextBox.ReadOnly = True
        Me.IPAddressTextBox.Size = New System.Drawing.Size(461, 57)
        Me.IPAddressTextBox.TabIndex = 1
        Me.IPAddressTextBox.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(155, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(188, 40)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Server läuft!"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'Button4
        '
        Me.Button4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Location = New System.Drawing.Point(0, 60)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(198, 30)
        Me.Button4.TabIndex = 2
        Me.Button4.Text = "Einstellungen"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'ControlPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MinimumSize = New System.Drawing.Size(816, 489)
        Me.Name = "ControlPanel"
        Me.Text = "ClassRoom ControlPanel"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        CType(Me.UserProfilePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.MainPanel.ResumeLayout(False)
        Me.MenuPanel.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.WiFiDisplayPanel.ResumeLayout(False)
        Me.WiFiDisplayPanel.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents MainPanel As Panel
    Friend WithEvents PCOverviewPanel As Panel
    Friend WithEvents UserNameLabel As Label
    Friend WithEvents UserProfilePictureBox As PictureBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents IPAddressTextBox As TextBox
    Friend WithEvents WiFiDisplayPanel As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents WiFiPasswordTextBox As TextBox
    Friend WithEvents SSIDTextBox As TextBox
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents MenuPanel As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
End Class
