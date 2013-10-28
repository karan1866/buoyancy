<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCapture
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCapture))
        Me.pcbMain = New System.Windows.Forms.PictureBox
        Me.cmdSave = New System.Windows.Forms.Button
        CType(Me.pcbMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pcbMain
        '
        Me.pcbMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.pcbMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pcbMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pcbMain.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pcbMain.Location = New System.Drawing.Point(12, 12)
        Me.pcbMain.Name = "pcbMain"
        Me.pcbMain.Size = New System.Drawing.Size(256, 192)
        Me.pcbMain.TabIndex = 0
        Me.pcbMain.TabStop = False
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(193, 215)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 1
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'frmCapture
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(280, 250)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.pcbMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCapture"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "iCapture"
        CType(Me.pcbMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pcbMain As System.Windows.Forms.PictureBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button

End Class
