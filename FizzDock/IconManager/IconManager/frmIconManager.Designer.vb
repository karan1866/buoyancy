<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIconManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIconManager))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel
        Me.lblAdd = New System.Windows.Forms.Label
        Me.lblDel = New System.Windows.Forms.Label
        Me.pcbButton = New System.Windows.Forms.PictureBox
        Me.File1 = New Microsoft.VisualBasic.Compatibility.VB6.FileListBox
        Me.FlowPanel = New System.Windows.Forms.FlowLayoutPanel
        Me.Panel1.SuspendLayout()
        Me.FlowLayoutPanel2.SuspendLayout()
        CType(Me.pcbButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.Controls.Add(Me.FlowLayoutPanel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 370)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(590, 33)
        Me.Panel1.TabIndex = 1
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent
        Me.FlowLayoutPanel2.Controls.Add(Me.lblAdd)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblDel)
        Me.FlowLayoutPanel2.Controls.Add(Me.pcbButton)
        Me.FlowLayoutPanel2.Controls.Add(Me.File1)
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(590, 33)
        Me.FlowLayoutPanel2.TabIndex = 1
        '
        'lblAdd
        '
        Me.lblAdd.BackColor = System.Drawing.Color.Transparent
        Me.lblAdd.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdd.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblAdd.Location = New System.Drawing.Point(3, 0)
        Me.lblAdd.Name = "lblAdd"
        Me.lblAdd.Size = New System.Drawing.Size(133, 33)
        Me.lblAdd.TabIndex = 1
        Me.lblAdd.Text = "ADD"
        Me.lblAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDel
        '
        Me.lblDel.BackColor = System.Drawing.Color.Transparent
        Me.lblDel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDel.ForeColor = System.Drawing.Color.White
        Me.lblDel.Location = New System.Drawing.Point(142, 0)
        Me.lblDel.Name = "lblDel"
        Me.lblDel.Size = New System.Drawing.Size(133, 33)
        Me.lblDel.TabIndex = 2
        Me.lblDel.Text = "Delete"
        Me.lblDel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pcbButton
        '
        Me.pcbButton.Image = CType(resources.GetObject("pcbButton.Image"), System.Drawing.Image)
        Me.pcbButton.Location = New System.Drawing.Point(281, 3)
        Me.pcbButton.Name = "pcbButton"
        Me.pcbButton.Size = New System.Drawing.Size(100, 27)
        Me.pcbButton.TabIndex = 7
        Me.pcbButton.TabStop = False
        Me.pcbButton.Visible = False
        '
        'File1
        '
        Me.File1.FormattingEnabled = True
        Me.File1.Location = New System.Drawing.Point(387, 3)
        Me.File1.Name = "File1"
        Me.File1.Pattern = "*.png"
        Me.File1.Size = New System.Drawing.Size(135, 17)
        Me.File1.TabIndex = 8
        Me.File1.Visible = False
        '
        'FlowPanel
        '
        Me.FlowPanel.AutoScroll = True
        Me.FlowPanel.BackgroundImage = CType(resources.GetObject("FlowPanel.BackgroundImage"), System.Drawing.Image)
        Me.FlowPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.FlowPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowPanel.Location = New System.Drawing.Point(0, 0)
        Me.FlowPanel.Name = "FlowPanel"
        Me.FlowPanel.Padding = New System.Windows.Forms.Padding(5)
        Me.FlowPanel.Size = New System.Drawing.Size(590, 370)
        Me.FlowPanel.TabIndex = 0
        '
        'frmIconManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 403)
        Me.Controls.Add(Me.FlowPanel)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmIconManager"
        Me.ShowInTaskbar = False
        Me.Text = "IconManager"
        Me.Panel1.ResumeLayout(False)
        Me.FlowLayoutPanel2.ResumeLayout(False)
        CType(Me.pcbButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblAdd As System.Windows.Forms.Label
    Friend WithEvents lblDel As System.Windows.Forms.Label
    Friend WithEvents FlowPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pcbButton As System.Windows.Forms.PictureBox
    Friend WithEvents File1 As Microsoft.VisualBasic.Compatibility.VB6.FileListBox

End Class
