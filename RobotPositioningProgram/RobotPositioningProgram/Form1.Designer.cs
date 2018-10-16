namespace RobotPositioningProgram
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.simulatorControlBox = new System.Windows.Forms.GroupBox();
            this.positioningControlBox = new System.Windows.Forms.GroupBox();
            this.arduinoReadoutControlBox = new System.Windows.Forms.GroupBox();
            this.fileEditorControlBox = new System.Windows.Forms.GroupBox();
            this.moveButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rawAngleModeRadio = new System.Windows.Forms.RadioButton();
            this.poseModeRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.xPositionTextBox = new System.Windows.Forms.TextBox();
            this.yPositionTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.zPositionTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.iOrientationTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.jOrientationTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.kOrientationTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.r0ComboBox = new System.Windows.Forms.ComboBox();
            this.r1ComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.r2ComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.r5ComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.r4ComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.r3ComboBox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.resetButton2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.slowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialReadoutTextBox = new System.Windows.Forms.TextBox();
            this.fileEditorTextBox = new System.Windows.Forms.TextBox();
            this.positioningControlBox.SuspendLayout();
            this.arduinoReadoutControlBox.SuspendLayout();
            this.fileEditorControlBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // simulatorControlBox
            // 
            this.simulatorControlBox.Location = new System.Drawing.Point(13, 13);
            this.simulatorControlBox.Name = "simulatorControlBox";
            this.simulatorControlBox.Size = new System.Drawing.Size(460, 364);
            this.simulatorControlBox.TabIndex = 0;
            this.simulatorControlBox.TabStop = false;
            this.simulatorControlBox.Text = "Simulator";
            // 
            // positioningControlBox
            // 
            this.positioningControlBox.Controls.Add(this.panel2);
            this.positioningControlBox.Controls.Add(this.panel1);
            this.positioningControlBox.Controls.Add(this.saveButton);
            this.positioningControlBox.Controls.Add(this.moveButton);
            this.positioningControlBox.Location = new System.Drawing.Point(480, 13);
            this.positioningControlBox.Name = "positioningControlBox";
            this.positioningControlBox.Size = new System.Drawing.Size(527, 263);
            this.positioningControlBox.TabIndex = 1;
            this.positioningControlBox.TabStop = false;
            this.positioningControlBox.Text = "Positioning";
            // 
            // arduinoReadoutControlBox
            // 
            this.arduinoReadoutControlBox.Controls.Add(this.serialReadoutTextBox);
            this.arduinoReadoutControlBox.Location = new System.Drawing.Point(13, 384);
            this.arduinoReadoutControlBox.Name = "arduinoReadoutControlBox";
            this.arduinoReadoutControlBox.Size = new System.Drawing.Size(460, 122);
            this.arduinoReadoutControlBox.TabIndex = 2;
            this.arduinoReadoutControlBox.TabStop = false;
            this.arduinoReadoutControlBox.Text = "Serial Readout";
            // 
            // fileEditorControlBox
            // 
            this.fileEditorControlBox.Controls.Add(this.fileEditorTextBox);
            this.fileEditorControlBox.Controls.Add(this.menuStrip1);
            this.fileEditorControlBox.Location = new System.Drawing.Point(480, 283);
            this.fileEditorControlBox.Name = "fileEditorControlBox";
            this.fileEditorControlBox.Size = new System.Drawing.Size(527, 223);
            this.fileEditorControlBox.TabIndex = 3;
            this.fileEditorControlBox.TabStop = false;
            this.fileEditorControlBox.Text = "File Editor";
            // 
            // moveButton
            // 
            this.moveButton.Location = new System.Drawing.Point(7, 234);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(253, 23);
            this.moveButton.TabIndex = 0;
            this.moveButton.Text = "Move to pose";
            this.moveButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(266, 234);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(255, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save pose";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.resetButton);
            this.panel1.Controls.Add(this.kOrientationTextBox);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.jOrientationTextBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.iOrientationTextBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.zPositionTextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.yPositionTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.xPositionTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.poseModeRadioButton);
            this.panel1.Location = new System.Drawing.Point(7, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(514, 79);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.resetButton2);
            this.panel2.Controls.Add(this.r5ComboBox);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.r4ComboBox);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.r3ComboBox);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.r2ComboBox);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.r1ComboBox);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.r0ComboBox);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.rawAngleModeRadio);
            this.panel2.Location = new System.Drawing.Point(7, 105);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(514, 79);
            this.panel2.TabIndex = 3;
            // 
            // rawAngleModeRadio
            // 
            this.rawAngleModeRadio.AutoSize = true;
            this.rawAngleModeRadio.Location = new System.Drawing.Point(4, 4);
            this.rawAngleModeRadio.Name = "rawAngleModeRadio";
            this.rawAngleModeRadio.Size = new System.Drawing.Size(107, 17);
            this.rawAngleModeRadio.TabIndex = 0;
            this.rawAngleModeRadio.TabStop = true;
            this.rawAngleModeRadio.Text = "Raw Angle Mode";
            this.rawAngleModeRadio.UseVisualStyleBackColor = true;
            // 
            // poseModeRadioButton
            // 
            this.poseModeRadioButton.AutoSize = true;
            this.poseModeRadioButton.Location = new System.Drawing.Point(4, 4);
            this.poseModeRadioButton.Name = "poseModeRadioButton";
            this.poseModeRadioButton.Size = new System.Drawing.Size(79, 17);
            this.poseModeRadioButton.TabIndex = 0;
            this.poseModeRadioButton.TabStop = true;
            this.poseModeRadioButton.Text = "Pose Mode";
            this.poseModeRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "X:";
            // 
            // xPositionTextBox
            // 
            this.xPositionTextBox.Location = new System.Drawing.Point(27, 25);
            this.xPositionTextBox.Name = "xPositionTextBox";
            this.xPositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.xPositionTextBox.TabIndex = 2;
            // 
            // yPositionTextBox
            // 
            this.yPositionTextBox.Location = new System.Drawing.Point(153, 25);
            this.yPositionTextBox.Name = "yPositionTextBox";
            this.yPositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.yPositionTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y:";
            // 
            // zPositionTextBox
            // 
            this.zPositionTextBox.Location = new System.Drawing.Point(284, 25);
            this.zPositionTextBox.Name = "zPositionTextBox";
            this.zPositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.zPositionTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Z:";
            // 
            // iOrientationTextBox
            // 
            this.iOrientationTextBox.Location = new System.Drawing.Point(27, 51);
            this.iOrientationTextBox.Name = "iOrientationTextBox";
            this.iOrientationTextBox.Size = new System.Drawing.Size(100, 20);
            this.iOrientationTextBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "i:";
            // 
            // jOrientationTextBox
            // 
            this.jOrientationTextBox.Location = new System.Drawing.Point(153, 51);
            this.jOrientationTextBox.Name = "jOrientationTextBox";
            this.jOrientationTextBox.Size = new System.Drawing.Size(100, 20);
            this.jOrientationTextBox.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "j:";
            // 
            // kOrientationTextBox
            // 
            this.kOrientationTextBox.Location = new System.Drawing.Point(284, 51);
            this.kOrientationTextBox.Name = "kOrientationTextBox";
            this.kOrientationTextBox.Size = new System.Drawing.Size(100, 20);
            this.kOrientationTextBox.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(262, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "k:";
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(390, 23);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(121, 48);
            this.resetButton.TabIndex = 13;
            this.resetButton.Text = "Reset To Home";
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "R0:";
            // 
            // r0ComboBox
            // 
            this.r0ComboBox.FormattingEnabled = true;
            this.r0ComboBox.Location = new System.Drawing.Point(27, 21);
            this.r0ComboBox.Name = "r0ComboBox";
            this.r0ComboBox.Size = new System.Drawing.Size(100, 21);
            this.r0ComboBox.TabIndex = 2;
            // 
            // r1ComboBox
            // 
            this.r1ComboBox.FormattingEnabled = true;
            this.r1ComboBox.Location = new System.Drawing.Point(153, 21);
            this.r1ComboBox.Name = "r1ComboBox";
            this.r1ComboBox.Size = new System.Drawing.Size(100, 21);
            this.r1ComboBox.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(130, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "R1:";
            // 
            // r2ComboBox
            // 
            this.r2ComboBox.FormattingEnabled = true;
            this.r2ComboBox.Location = new System.Drawing.Point(284, 21);
            this.r2ComboBox.Name = "r2ComboBox";
            this.r2ComboBox.Size = new System.Drawing.Size(100, 21);
            this.r2ComboBox.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(261, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "R2:";
            // 
            // r5ComboBox
            // 
            this.r5ComboBox.FormattingEnabled = true;
            this.r5ComboBox.Location = new System.Drawing.Point(284, 48);
            this.r5ComboBox.Name = "r5ComboBox";
            this.r5ComboBox.Size = new System.Drawing.Size(100, 21);
            this.r5ComboBox.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(261, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "R5:";
            // 
            // r4ComboBox
            // 
            this.r4ComboBox.FormattingEnabled = true;
            this.r4ComboBox.Location = new System.Drawing.Point(153, 48);
            this.r4ComboBox.Name = "r4ComboBox";
            this.r4ComboBox.Size = new System.Drawing.Size(100, 21);
            this.r4ComboBox.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(130, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "R4:";
            // 
            // r3ComboBox
            // 
            this.r3ComboBox.FormattingEnabled = true;
            this.r3ComboBox.Location = new System.Drawing.Point(27, 48);
            this.r3ComboBox.Name = "r3ComboBox";
            this.r3ComboBox.Size = new System.Drawing.Size(100, 21);
            this.r3ComboBox.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "R3:";
            // 
            // resetButton2
            // 
            this.resetButton2.Location = new System.Drawing.Point(390, 21);
            this.resetButton2.Name = "resetButton2";
            this.resetButton2.Size = new System.Drawing.Size(121, 48);
            this.resetButton2.TabIndex = 14;
            this.resetButton2.Text = "Reset To Home";
            this.resetButton2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.runToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 16);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(521, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProgramToolStripMenuItem,
            this.openProgramToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveProgramToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem1,
            this.slowToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.runToolStripMenuItem.Text = "Run";
            // 
            // runToolStripMenuItem1
            // 
            this.runToolStripMenuItem1.Name = "runToolStripMenuItem1";
            this.runToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.runToolStripMenuItem1.Text = "Intended Speed";
            // 
            // slowToolStripMenuItem
            // 
            this.slowToolStripMenuItem.Name = "slowToolStripMenuItem";
            this.slowToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.slowToolStripMenuItem.Text = "Slow";
            // 
            // newProgramToolStripMenuItem
            // 
            this.newProgramToolStripMenuItem.Name = "newProgramToolStripMenuItem";
            this.newProgramToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.newProgramToolStripMenuItem.Text = "New Program";
            // 
            // saveProgramToolStripMenuItem
            // 
            this.saveProgramToolStripMenuItem.Name = "saveProgramToolStripMenuItem";
            this.saveProgramToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.saveProgramToolStripMenuItem.Text = "Save Program";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(158, 6);
            // 
            // openProgramToolStripMenuItem
            // 
            this.openProgramToolStripMenuItem.Name = "openProgramToolStripMenuItem";
            this.openProgramToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.openProgramToolStripMenuItem.Text = "Open Program...";
            // 
            // serialReadoutTextBox
            // 
            this.serialReadoutTextBox.Location = new System.Drawing.Point(7, 20);
            this.serialReadoutTextBox.Multiline = true;
            this.serialReadoutTextBox.Name = "serialReadoutTextBox";
            this.serialReadoutTextBox.Size = new System.Drawing.Size(447, 96);
            this.serialReadoutTextBox.TabIndex = 0;
            // 
            // fileEditorTextBox
            // 
            this.fileEditorTextBox.Location = new System.Drawing.Point(7, 44);
            this.fileEditorTextBox.Multiline = true;
            this.fileEditorTextBox.Name = "fileEditorTextBox";
            this.fileEditorTextBox.Size = new System.Drawing.Size(514, 173);
            this.fileEditorTextBox.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 518);
            this.Controls.Add(this.fileEditorControlBox);
            this.Controls.Add(this.arduinoReadoutControlBox);
            this.Controls.Add(this.positioningControlBox);
            this.Controls.Add(this.simulatorControlBox);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.positioningControlBox.ResumeLayout(false);
            this.arduinoReadoutControlBox.ResumeLayout(false);
            this.arduinoReadoutControlBox.PerformLayout();
            this.fileEditorControlBox.ResumeLayout(false);
            this.fileEditorControlBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox simulatorControlBox;
        private System.Windows.Forms.GroupBox positioningControlBox;
        private System.Windows.Forms.GroupBox arduinoReadoutControlBox;
        private System.Windows.Forms.GroupBox fileEditorControlBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rawAngleModeRadio;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox kOrientationTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox jOrientationTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox iOrientationTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox zPositionTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox yPositionTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox xPositionTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton poseModeRadioButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button resetButton2;
        private System.Windows.Forms.ComboBox r5ComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox r4ComboBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox r3ComboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox r2ComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox r1ComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox r0ComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox serialReadoutTextBox;
        private System.Windows.Forms.TextBox fileEditorTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem slowToolStripMenuItem;
    }
}

