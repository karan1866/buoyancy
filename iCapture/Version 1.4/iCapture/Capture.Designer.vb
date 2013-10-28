<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Capture
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Capture))
        Me.tmr1 = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.InfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.InfoSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.CropSnapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CaptureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.FlashToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoSaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoStartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ChangePathToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tmr1
        '
        Me.tmr1.Interval = 1
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Text = "iCapture"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InfoToolStripMenuItem, Me.InfoSeparator, Me.CropSnapToolStripMenuItem, Me.CaptureToolStripMenuItem, Me.ToolStripSeparator2, Me.FlashToolStripMenuItem, Me.AutoSaveToolStripMenuItem, Me.AutoStartToolStripMenuItem, Me.ToolStripSeparator1, Me.ChangePathToolStripMenuItem, Me.SettingsToolStripMenuItem, Me.ToolStripSeparator4, Me.AboutToolStripMenuItem, Me.ToolStripSeparator3, Me.ExitToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 276)
        '
        'InfoToolStripMenuItem
        '
        Me.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem"
        Me.InfoToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.InfoToolStripMenuItem.Text = "Stop"
        Me.InfoToolStripMenuItem.Visible = False
        '
        'InfoSeparator
        '
        Me.InfoSeparator.Name = "InfoSeparator"
        Me.InfoSeparator.Size = New System.Drawing.Size(161, 6)
        Me.InfoSeparator.Visible = False
        '
        'CropSnapToolStripMenuItem
        '
        Me.CropSnapToolStripMenuItem.Name = "CropSnapToolStripMenuItem"
        Me.CropSnapToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.CropSnapToolStripMenuItem.Text = "Cropped Snap"
        '
        'CaptureToolStripMenuItem
        '
        Me.CaptureToolStripMenuItem.Name = "CaptureToolStripMenuItem"
        Me.CaptureToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.CaptureToolStripMenuItem.Text = "Timed Snaps"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(161, 6)
        '
        'FlashToolStripMenuItem
        '
        Me.FlashToolStripMenuItem.Checked = True
        Me.FlashToolStripMenuItem.CheckOnClick = True
        Me.FlashToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.FlashToolStripMenuItem.Name = "FlashToolStripMenuItem"
        Me.FlashToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.FlashToolStripMenuItem.Text = "Flash"
        '
        'AutoSaveToolStripMenuItem
        '
        Me.AutoSaveToolStripMenuItem.Checked = True
        Me.AutoSaveToolStripMenuItem.CheckOnClick = True
        Me.AutoSaveToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AutoSaveToolStripMenuItem.Name = "AutoSaveToolStripMenuItem"
        Me.AutoSaveToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.AutoSaveToolStripMenuItem.Text = "Auto Save"
        '
        'AutoStartToolStripMenuItem
        '
        Me.AutoStartToolStripMenuItem.Checked = True
        Me.AutoStartToolStripMenuItem.CheckOnClick = True
        Me.AutoStartToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AutoStartToolStripMenuItem.Name = "AutoStartToolStripMenuItem"
        Me.AutoStartToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.AutoStartToolStripMenuItem.Text = "Auto Start"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(161, 6)
        '
        'ChangePathToolStripMenuItem
        '
        Me.ChangePathToolStripMenuItem.Name = "ChangePathToolStripMenuItem"
        Me.ChangePathToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.ChangePathToolStripMenuItem.Text = "Change Path"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(161, 6)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(161, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'Capture
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(500000, 500000)
        Me.Name = "Capture"
        Me.Opacity = 0
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Capture"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tmr1 As System.Windows.Forms.Timer
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AutoSaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FlashToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CaptureToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents AutoStartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CropSnapToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangePathToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InfoSeparator As System.Windows.Forms.ToolStripSeparator
End Class
