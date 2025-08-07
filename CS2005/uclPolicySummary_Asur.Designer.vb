<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uclPolicySummary_Asur
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ChangeKeyman_L = New ComCtl.ChangeKeyman()
        Me.ChangeKeyman_I = New ComCtl.ChangeKeyman()
        Me.Ctrl_VisitCS1 = New CRS_Ctrl.ctrl_VisitCS()
        Me.UclPolicyBillingInfo_Asur1 = New CS2005.uclPolicyBillingInfo_Asur()
        Me.UclPolicyMRP_Asur1 = New CS2005.uclPolicyMRP_Asur()
        Me.UclPolicyScreenHead_Asur1 = New CS2005.uclPolicyScreenHead_Asur()
        Me.UclPolicyClient_Asur1 = New CS2005.uclPolicyClient_Asur()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 112)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(815, 298)
        Me.TabControl1.TabIndex = 17
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnCancel)
        Me.TabPage1.Controls.Add(Me.btnUpdate)
        Me.TabPage1.Controls.Add(Me.btnAdd)
        Me.TabPage1.Controls.Add(Me.btnChange)
        Me.TabPage1.Controls.Add(Me.btnDelete)
        Me.TabPage1.Controls.Add(Me.UclPolicyClient_Asur1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(807, 272)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Policy Client Main"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Enabled = False
        Me.btnCancel.Location = New System.Drawing.Point(711, 223)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(53, 23)
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Enabled = False
        Me.btnUpdate.Location = New System.Drawing.Point(656, 223)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(53, 23)
        Me.btnUpdate.TabIndex = 14
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Enabled = False
        Me.btnAdd.Location = New System.Drawing.Point(491, 223)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(53, 23)
        Me.btnAdd.TabIndex = 6
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnChange
        '
        Me.btnChange.Enabled = False
        Me.btnChange.Location = New System.Drawing.Point(546, 223)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(53, 23)
        Me.btnChange.TabIndex = 7
        Me.btnChange.Text = "Change"
        Me.btnChange.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Enabled = False
        Me.btnDelete.Location = New System.Drawing.Point(601, 223)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(53, 23)
        Me.btnDelete.TabIndex = 8
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(807, 272)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Change Insured/Keyman"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ChangeKeyman_L, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ChangeKeyman_I, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(807, 272)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'ChangeKeyman_L
        '
        Me.ChangeKeyman_L.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChangeKeyman_L.Location = New System.Drawing.Point(403, 0)
        Me.ChangeKeyman_L.Margin = New System.Windows.Forms.Padding(0)
        Me.ChangeKeyman_L.Name = "ChangeKeyman_L"
        Me.ChangeKeyman_L.Size = New System.Drawing.Size(404, 272)
        Me.ChangeKeyman_L.TabIndex = 0
        '
        'ChangeKeyman_I
        '
        Me.ChangeKeyman_I.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChangeKeyman_I.Location = New System.Drawing.Point(0, 0)
        Me.ChangeKeyman_I.Margin = New System.Windows.Forms.Padding(0)
        Me.ChangeKeyman_I.Name = "ChangeKeyman_I"
        Me.ChangeKeyman_I.Size = New System.Drawing.Size(403, 272)
        Me.ChangeKeyman_I.TabIndex = 0
        '
        'Ctrl_VisitCS1
        '
        Me.Ctrl_VisitCS1.CSFlag = "N/A"
        Me.Ctrl_VisitCS1.CustomerID = ""
        Me.Ctrl_VisitCS1.Lang = "en"
        Me.Ctrl_VisitCS1.Location = New System.Drawing.Point(819, 253)
        Me.Ctrl_VisitCS1.Name = "Ctrl_VisitCS1"
        Me.Ctrl_VisitCS1.PolicyNo = ""
        Me.Ctrl_VisitCS1.PolicyNoInUse = ""
        Me.Ctrl_VisitCS1.Size = New System.Drawing.Size(224, 150)
        Me.Ctrl_VisitCS1.TabIndex = 20
        Me.Ctrl_VisitCS1.UpdateDate = ""
        Me.Ctrl_VisitCS1.UpdateUser = ""
        Me.Ctrl_VisitCS1.UpdateUser2 = ""
        Me.Ctrl_VisitCS1.Visible = False
        '
        'UclPolicyBillingInfo_Asur1
        '
        Me.UclPolicyBillingInfo_Asur1.Location = New System.Drawing.Point(0, 419)
        Me.UclPolicyBillingInfo_Asur1.Name = "UclPolicyBillingInfo_Asur1"
        Me.UclPolicyBillingInfo_Asur1.Size = New System.Drawing.Size(1365, 303)
        Me.UclPolicyBillingInfo_Asur1.TabIndex = 21
        '
        'UclPolicyMRP_Asur1
        '
        Me.UclPolicyMRP_Asur1.Location = New System.Drawing.Point(819, 122)
        Me.UclPolicyMRP_Asur1.Name = "UclPolicyMRP_Asur1"
        Me.UclPolicyMRP_Asur1.Size = New System.Drawing.Size(197, 124)
        Me.UclPolicyMRP_Asur1.TabIndex = 19
        Me.UclPolicyMRP_Asur1.Visible = False
        '
        'UclPolicyScreenHead_Asur1
        '
        Me.UclPolicyScreenHead_Asur1.Location = New System.Drawing.Point(0, -1)
        Me.UclPolicyScreenHead_Asur1.Name = "UclPolicyScreenHead_Asur1"
        Me.UclPolicyScreenHead_Asur1.Size = New System.Drawing.Size(1024, 110)
        Me.UclPolicyScreenHead_Asur1.TabIndex = 18
        '
        'UclPolicyClient_Asur1
        '
        Me.UclPolicyClient_Asur1.Location = New System.Drawing.Point(3, 1)
        Me.UclPolicyClient_Asur1.Name = "UclPolicyClient_Asur1"
        Me.UclPolicyClient_Asur1.Size = New System.Drawing.Size(787, 277)
        Me.UclPolicyClient_Asur1.TabIndex = 16
        '
        'uclPolicySummary_Asur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.UclPolicyBillingInfo_Asur1)
        Me.Controls.Add(Me.Ctrl_VisitCS1)
        Me.Controls.Add(Me.UclPolicyMRP_Asur1)
        Me.Controls.Add(Me.UclPolicyScreenHead_Asur1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "uclPolicySummary_Asur"
        Me.Size = New System.Drawing.Size(1089, 725)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnChange As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ChangeKeyman_L As ComCtl.ChangeKeyman
    Friend WithEvents ChangeKeyman_I As ComCtl.ChangeKeyman
    Friend WithEvents UclPolicyClient_Asur1 As CS2005.uclPolicyClient_Asur
    Friend WithEvents UclPolicyScreenHead_Asur1 As CS2005.uclPolicyScreenHead_Asur
    Friend WithEvents UclPolicyMRP_Asur1 As CS2005.uclPolicyMRP_Asur
    Friend WithEvents Ctrl_VisitCS1 As CRS_Ctrl.ctrl_VisitCS
    Friend WithEvents UclPolicyBillingInfo_Asur1 As CS2005.uclPolicyBillingInfo_Asur

End Class
