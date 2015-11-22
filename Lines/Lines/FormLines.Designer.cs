namespace Lines
{
    partial class FormLines
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLines));
            this.panel = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripTextBoxName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBoxRealName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBoxScore = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBoxRealScore = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItemRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox10 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem1Help = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.White;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 27);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(532, 176);
            this.panel.TabIndex = 0;
            // 
            // timer
            // 
            this.timer.Interval = 200;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxName,
            this.toolStripTextBoxRealName,
            this.toolStripTextBoxScore,
            this.toolStripTextBoxRealScore,
            this.toolStripMenuItemRestart,
            this.toolStripMenuItemHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(532, 27);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripTextBoxName
            // 
            this.toolStripTextBoxName.AutoSize = false;
            this.toolStripTextBoxName.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStripTextBoxName.Name = "toolStripTextBoxName";
            this.toolStripTextBoxName.Size = new System.Drawing.Size(74, 23);
            this.toolStripTextBoxName.Text = "Имя игрока:";
            // 
            // toolStripTextBoxRealName
            // 
            this.toolStripTextBoxRealName.Name = "toolStripTextBoxRealName";
            this.toolStripTextBoxRealName.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBoxRealName.Text = "Player";
            // 
            // toolStripTextBoxScore
            // 
            this.toolStripTextBoxScore.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStripTextBoxScore.Name = "toolStripTextBoxScore";
            this.toolStripTextBoxScore.Size = new System.Drawing.Size(34, 23);
            this.toolStripTextBoxScore.Text = "Счёт:";
            // 
            // toolStripTextBoxRealScore
            // 
            this.toolStripTextBoxRealScore.Name = "toolStripTextBoxRealScore";
            this.toolStripTextBoxRealScore.ReadOnly = true;
            this.toolStripTextBoxRealScore.Size = new System.Drawing.Size(25, 23);
            this.toolStripTextBoxRealScore.Text = "0";
            // 
            // toolStripMenuItemRestart
            // 
            this.toolStripMenuItemRestart.Name = "toolStripMenuItemRestart";
            this.toolStripMenuItemRestart.Size = new System.Drawing.Size(99, 23);
            this.toolStripMenuItemRestart.Text = "Начать заново";
            this.toolStripMenuItemRestart.Click += new System.EventHandler(this.toolStripMenuItemRestart_Click);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox10,
            this.toolStripMenuItem1Help});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(63, 23);
            this.toolStripMenuItemHelp.Text = "Об игре";
            // 
            // toolStripComboBox10
            // 
            this.toolStripComboBox10.Name = "toolStripComboBox10";
            this.toolStripComboBox10.Size = new System.Drawing.Size(125, 23);
            this.toolStripComboBox10.Text = "Таблица рекордов";
            this.toolStripComboBox10.Click += new System.EventHandler(this.toolStripComboBox10_Click);
            // 
            // toolStripMenuItem1Help
            // 
            this.toolStripMenuItem1Help.Name = "toolStripMenuItem1Help";
            this.toolStripMenuItem1Help.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItem1Help.Text = "Правила";
            this.toolStripMenuItem1Help.Click += new System.EventHandler(this.toolStripMenuItem1Help_Click);
            // 
            // FormLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 203);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormLines";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lines";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxName;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxRealName;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxScore;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxRealScore;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRestart;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1Help;

    }
}

