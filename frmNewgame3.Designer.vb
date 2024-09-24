<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewgame3
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
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbAllies1 = New System.Windows.Forms.CheckBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.cbAllies2 = New System.Windows.Forms.CheckBox
        Me.cbAllies3 = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(86, 290)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(178, 40)
        Me.Button3.TabIndex = 142
        Me.Button3.Text = "Hard"
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(86, 227)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(178, 40)
        Me.Button4.TabIndex = 141
        Me.Button4.Text = "Average"
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Location = New System.Drawing.Point(86, 165)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(178, 40)
        Me.Button5.TabIndex = 140
        Me.Button5.Text = "Easy"
        Me.Button5.UseVisualStyleBackColor = True
        Me.Button5.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(248, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(291, 24)
        Me.Label1.TabIndex = 143
        Me.Label1.Text = "Choose the Difficulty of the Game:"
        Me.Label1.Visible = False
        '
        'cbAllies1
        '
        Me.cbAllies1.AutoSize = True
        Me.cbAllies1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAllies1.Location = New System.Drawing.Point(349, 165)
        Me.cbAllies1.Name = "cbAllies1"
        Me.cbAllies1.Size = New System.Drawing.Size(80, 30)
        Me.cbAllies1.TabIndex = 144
        Me.cbAllies1.Text = "Easy"
        Me.cbAllies1.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(603, 401)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(125, 40)
        Me.Button1.TabIndex = 145
        Me.Button1.Text = "Start"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cbAllies2
        '
        Me.cbAllies2.AutoSize = True
        Me.cbAllies2.Checked = True
        Me.cbAllies2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAllies2.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAllies2.Location = New System.Drawing.Point(349, 230)
        Me.cbAllies2.Name = "cbAllies2"
        Me.cbAllies2.Size = New System.Drawing.Size(112, 30)
        Me.cbAllies2.TabIndex = 146
        Me.cbAllies2.Text = "Average"
        Me.cbAllies2.UseVisualStyleBackColor = True
        '
        'cbAllies3
        '
        Me.cbAllies3.AutoSize = True
        Me.cbAllies3.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAllies3.Location = New System.Drawing.Point(349, 295)
        Me.cbAllies3.Name = "cbAllies3"
        Me.cbAllies3.Size = New System.Drawing.Size(78, 30)
        Me.cbAllies3.TabIndex = 147
        Me.cbAllies3.Text = "Hard"
        Me.cbAllies3.UseVisualStyleBackColor = True
        '
        'frmNewgame3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ClientSize = New System.Drawing.Size(767, 453)
        Me.Controls.Add(Me.cbAllies3)
        Me.Controls.Add(Me.cbAllies2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cbAllies1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button5)
        Me.Name = "frmNewgame3"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "_______________________________________  WWIIBattles Campaign 1 - The Polish Inva" & _
            "sion   ______________________________________"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbAllies1 As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cbAllies2 As System.Windows.Forms.CheckBox
    Friend WithEvents cbAllies3 As System.Windows.Forms.CheckBox
End Class
