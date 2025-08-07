<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPostSalesQuestionnaireMasterMcu
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
        Me.dgvQuestions = New System.Windows.Forms.DataGridView
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkVC = New System.Windows.Forms.CheckBox
        Me.chkSM = New System.Windows.Forms.CheckBox
        Me.txtQuestion = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtQuestionNo = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdNew = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdEdit = New System.Windows.Forms.Button
        Me.cmdSelect = New System.Windows.Forms.Button
        Me.bsQuestions = New System.Windows.Forms.BindingSource(Me.components)
        Me.chkNoFNA = New System.Windows.Forms.CheckBox
        Me.cboQuestionnaire = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.colQuestionnaire = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.colQuestionNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colQuestion = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvQuestions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.bsQuestions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvQuestions
        '
        Me.dgvQuestions.AllowUserToAddRows = False
        Me.dgvQuestions.AllowUserToDeleteRows = False
        Me.dgvQuestions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvQuestions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colQuestionnaire, Me.colQuestionNo, Me.colQuestion})
        Me.dgvQuestions.Location = New System.Drawing.Point(24, 36)
        Me.dgvQuestions.Name = "dgvQuestions"
        Me.dgvQuestions.ReadOnly = True
        Me.dgvQuestions.Size = New System.Drawing.Size(766, 181)
        Me.dgvQuestions.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Questions"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cboQuestionnaire)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.txtQuestion)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtQuestionNo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(24, 239)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(766, 165)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Details"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkNoFNA)
        Me.GroupBox2.Controls.Add(Me.chkVC)
        Me.GroupBox2.Controls.Add(Me.chkSM)
        Me.GroupBox2.Location = New System.Drawing.Point(522, 25)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 48)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Call Type"
        '
        'chkVC
        '
        Me.chkVC.AutoSize = True
        Me.chkVC.Location = New System.Drawing.Point(17, 19)
        Me.chkVC.Name = "chkVC"
        Me.chkVC.Size = New System.Drawing.Size(40, 17)
        Me.chkVC.TabIndex = 4
        Me.chkVC.Text = "VC"
        Me.chkVC.UseVisualStyleBackColor = True
        '
        'chkSM
        '
        Me.chkSM.AutoSize = True
        Me.chkSM.Location = New System.Drawing.Point(66, 19)
        Me.chkSM.Name = "chkSM"
        Me.chkSM.Size = New System.Drawing.Size(42, 17)
        Me.chkSM.TabIndex = 5
        Me.chkSM.Text = "SM"
        Me.chkSM.UseVisualStyleBackColor = True
        '
        'txtQuestion
        '
        Me.txtQuestion.Location = New System.Drawing.Point(102, 79)
        Me.txtQuestion.Multiline = True
        Me.txtQuestion.Name = "txtQuestion"
        Me.txtQuestion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtQuestion.Size = New System.Drawing.Size(615, 64)
        Me.txtQuestion.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Question"
        '
        'txtQuestionNo
        '
        Me.txtQuestionNo.Location = New System.Drawing.Point(340, 42)
        Me.txtQuestionNo.MaxLength = 10
        Me.txtQuestionNo.Name = "txtQuestionNo"
        Me.txtQuestionNo.Size = New System.Drawing.Size(100, 20)
        Me.txtQuestionNo.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(265, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Question No."
        '
        'cmdNew
        '
        Me.cmdNew.Location = New System.Drawing.Point(211, 424)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(75, 23)
        Me.cmdNew.TabIndex = 3
        Me.cmdNew.Text = "New"
        Me.cmdNew.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(373, 424)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 4
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(715, 424)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(75, 23)
        Me.cmdExit.TabIndex = 5
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdDelete
        '
        Me.cmdDelete.Location = New System.Drawing.Point(535, 424)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(75, 23)
        Me.cmdDelete.TabIndex = 6
        Me.cmdDelete.Text = "Delete"
        Me.cmdDelete.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(454, 424)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 7
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.Location = New System.Drawing.Point(292, 424)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(75, 23)
        Me.cmdEdit.TabIndex = 8
        Me.cmdEdit.Text = "Edit"
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'cmdSelect
        '
        Me.cmdSelect.Location = New System.Drawing.Point(616, 424)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(75, 23)
        Me.cmdSelect.TabIndex = 9
        Me.cmdSelect.Text = "Select"
        Me.cmdSelect.UseVisualStyleBackColor = True
        '
        'chkNoFNA
        '
        Me.chkNoFNA.AutoSize = True
        Me.chkNoFNA.Location = New System.Drawing.Point(114, 18)
        Me.chkNoFNA.Name = "chkNoFNA"
        Me.chkNoFNA.Size = New System.Drawing.Size(64, 17)
        Me.chkNoFNA.TabIndex = 6
        Me.chkNoFNA.Text = "No FNA"
        Me.chkNoFNA.UseVisualStyleBackColor = True
        '
        'cboQuestionnaire
        '
        Me.cboQuestionnaire.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboQuestionnaire.FormattingEnabled = True
        Me.cboQuestionnaire.Location = New System.Drawing.Point(102, 42)
        Me.cboQuestionnaire.Name = "cboQuestionnaire"
        Me.cboQuestionnaire.Size = New System.Drawing.Size(121, 21)
        Me.cboQuestionnaire.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Questionnaire"
        '
        'colQuestionnaire
        '
        Me.colQuestionnaire.DataPropertyName = "cswpsq_questionnaire_code"
        Me.colQuestionnaire.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.colQuestionnaire.HeaderText = "Questionnaire"
        Me.colQuestionnaire.Name = "colQuestionnaire"
        Me.colQuestionnaire.ReadOnly = True
        '
        'colQuestionNo
        '
        Me.colQuestionNo.DataPropertyName = "cswpsq_question_no"
        Me.colQuestionNo.HeaderText = "No."
        Me.colQuestionNo.Name = "colQuestionNo"
        Me.colQuestionNo.ReadOnly = True
        '
        'colQuestion
        '
        Me.colQuestion.DataPropertyName = "cswpsq_description"
        Me.colQuestion.HeaderText = "Question"
        Me.colQuestion.Name = "colQuestion"
        Me.colQuestion.ReadOnly = True
        Me.colQuestion.Width = 600
        '
        'frmPostSalesQuestionnaireMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(814, 470)
        Me.Controls.Add(Me.cmdSelect)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdNew)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvQuestions)
        Me.Name = "frmPostSalesQuestionnaireMaster"
        Me.Text = "Post Sales Call Question Maintenance"
        CType(Me.dgvQuestions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.bsQuestions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvQuestions As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtQuestion As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtQuestionNo As System.Windows.Forms.TextBox
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents cmdSelect As System.Windows.Forms.Button
    Friend WithEvents bsQuestions As System.Windows.Forms.BindingSource
    Friend WithEvents chkSM As System.Windows.Forms.CheckBox
    Friend WithEvents chkVC As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkNoFNA As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboQuestionnaire As System.Windows.Forms.ComboBox
    Friend WithEvents colQuestionnaire As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents colQuestionNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colQuestion As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
