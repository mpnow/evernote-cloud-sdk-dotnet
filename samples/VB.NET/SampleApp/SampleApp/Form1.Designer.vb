<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.PictureBoxThumbnail = New System.Windows.Forms.PictureBox()
        Me.label1 = New System.Windows.Forms.Label()
        CType(Me.PictureBoxThumbnail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxThumbnail
        '
        Me.PictureBoxThumbnail.Location = New System.Drawing.Point(41, 23)
        Me.PictureBoxThumbnail.Name = "PictureBoxThumbnail"
        Me.PictureBoxThumbnail.Size = New System.Drawing.Size(120, 120)
        Me.PictureBoxThumbnail.TabIndex = 0
        Me.PictureBoxThumbnail.TabStop = False
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(44, 156)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(115, 13)
        Me.label1.TabIndex = 2
        Me.label1.Text = "Downloaded thumbnail"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(198, 253)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.PictureBoxThumbnail)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.PictureBoxThumbnail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxThumbnail As System.Windows.Forms.PictureBox
    Private WithEvents label1 As System.Windows.Forms.Label
End Class
