namespace PlayGround
{
    partial class MainForm
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
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.BTNEntities = new System.Windows.Forms.Button();
            this.BTNTags = new System.Windows.Forms.Button();
            this.BTNQuery = new System.Windows.Forms.Button();
            this.BTNWords = new System.Windows.Forms.Button();
            this.BTNSentences = new System.Windows.Forms.Button();
            this.APISourceLabel = new System.Windows.Forms.Label();
            this.APISourceCombo = new System.Windows.Forms.ComboBox();
            this.SB = new System.Windows.Forms.StatusStrip();
            this.SP = new System.Windows.Forms.SplitContainer();
            this.InputText = new System.Windows.Forms.TextBox();
            this.InputLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RTBox = new System.Windows.Forms.RichTextBox();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.MenuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SP)).BeginInit();
            this.SP.Panel1.SuspendLayout();
            this.SP.Panel2.SuspendLayout();
            this.SP.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuPanel
            // 
            this.MenuPanel.AccessibleDescription = "Menu controls";
            this.MenuPanel.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.MenuPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MenuPanel.Controls.Add(this.BTNEntities);
            this.MenuPanel.Controls.Add(this.BTNTags);
            this.MenuPanel.Controls.Add(this.BTNQuery);
            this.MenuPanel.Controls.Add(this.BTNWords);
            this.MenuPanel.Controls.Add(this.BTNSentences);
            this.MenuPanel.Controls.Add(this.APISourceLabel);
            this.MenuPanel.Controls.Add(this.APISourceCombo);
            this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MenuPanel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuPanel.Location = new System.Drawing.Point(0, 0);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Size = new System.Drawing.Size(747, 36);
            this.MenuPanel.TabIndex = 0;
            // 
            // BTNEntities
            // 
            this.BTNEntities.Enabled = false;
            this.BTNEntities.Location = new System.Drawing.Point(520, 10);
            this.BTNEntities.Name = "BTNEntities";
            this.BTNEntities.Size = new System.Drawing.Size(94, 22);
            this.BTNEntities.TabIndex = 6;
            this.BTNEntities.Text = "Entities";
            this.BTNEntities.UseVisualStyleBackColor = true;
            this.BTNEntities.Click += new System.EventHandler(this.BTNEntities_Click);
            // 
            // BTNTags
            // 
            this.BTNTags.Enabled = false;
            this.BTNTags.Location = new System.Drawing.Point(420, 9);
            this.BTNTags.Name = "BTNTags";
            this.BTNTags.Size = new System.Drawing.Size(94, 22);
            this.BTNTags.TabIndex = 5;
            this.BTNTags.Text = "Tags";
            this.BTNTags.UseVisualStyleBackColor = true;
            this.BTNTags.Click += new System.EventHandler(this.BTNTags_Click);
            // 
            // BTNQuery
            // 
            this.BTNQuery.Enabled = false;
            this.BTNQuery.Location = new System.Drawing.Point(620, 10);
            this.BTNQuery.Name = "BTNQuery";
            this.BTNQuery.Size = new System.Drawing.Size(94, 22);
            this.BTNQuery.TabIndex = 4;
            this.BTNQuery.Text = "Ask";
            this.BTNQuery.UseVisualStyleBackColor = true;
            this.BTNQuery.Click += new System.EventHandler(this.BTNQuery_Click);
            // 
            // BTNWords
            // 
            this.BTNWords.Enabled = false;
            this.BTNWords.Location = new System.Drawing.Point(320, 9);
            this.BTNWords.Name = "BTNWords";
            this.BTNWords.Size = new System.Drawing.Size(94, 22);
            this.BTNWords.TabIndex = 3;
            this.BTNWords.Text = "Words";
            this.BTNWords.UseVisualStyleBackColor = true;
            this.BTNWords.Click += new System.EventHandler(this.BTNWords_Click);
            // 
            // BTNSentences
            // 
            this.BTNSentences.Enabled = false;
            this.BTNSentences.Location = new System.Drawing.Point(220, 9);
            this.BTNSentences.Name = "BTNSentences";
            this.BTNSentences.Size = new System.Drawing.Size(94, 22);
            this.BTNSentences.TabIndex = 2;
            this.BTNSentences.Text = "Sentences";
            this.BTNSentences.UseVisualStyleBackColor = true;
            this.BTNSentences.Click += new System.EventHandler(this.BTNSentences_Click);
            // 
            // APISourceLabel
            // 
            this.APISourceLabel.AutoSize = true;
            this.APISourceLabel.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.APISourceLabel.Location = new System.Drawing.Point(12, 9);
            this.APISourceLabel.Name = "APISourceLabel";
            this.APISourceLabel.Size = new System.Drawing.Size(41, 17);
            this.APISourceLabel.TabIndex = 1;
            this.APISourceLabel.Text = "API\'s";
            // 
            // APISourceCombo
            // 
            this.APISourceCombo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.APISourceCombo.FormattingEnabled = true;
            this.APISourceCombo.Items.AddRange(new object[] {
            "NLP Succinctly",
            "Google",
            "Cloudmersive",
            "Microsoft "});
            this.APISourceCombo.Location = new System.Drawing.Point(69, 8);
            this.APISourceCombo.Name = "APISourceCombo";
            this.APISourceCombo.Size = new System.Drawing.Size(145, 23);
            this.APISourceCombo.TabIndex = 0;
            // 
            // SB
            // 
            this.SB.Location = new System.Drawing.Point(0, 514);
            this.SB.Name = "SB";
            this.SB.Size = new System.Drawing.Size(747, 22);
            this.SB.TabIndex = 1;
            this.SB.Text = "statusStrip1";
            // 
            // SP
            // 
            this.SP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SP.Location = new System.Drawing.Point(0, 36);
            this.SP.Name = "SP";
            this.SP.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SP.Panel1
            // 
            this.SP.Panel1.Controls.Add(this.InputText);
            this.SP.Panel1.Controls.Add(this.InputLabel);
            // 
            // SP.Panel2
            // 
            this.SP.Panel2.Controls.Add(this.panel1);
            this.SP.Panel2.Controls.Add(this.ResultLabel);
            this.SP.Size = new System.Drawing.Size(747, 478);
            this.SP.SplitterDistance = 156;
            this.SP.TabIndex = 2;
            // 
            // InputText
            // 
            this.InputText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputText.Location = new System.Drawing.Point(71, 15);
            this.InputText.Multiline = true;
            this.InputText.Name = "InputText";
            this.InputText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InputText.Size = new System.Drawing.Size(664, 124);
            this.InputText.TabIndex = 1;
            this.InputText.TextChanged += new System.EventHandler(this.InputText_TextChanged);
            // 
            // InputLabel
            // 
            this.InputLabel.AutoSize = true;
            this.InputLabel.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputLabel.Location = new System.Drawing.Point(14, 15);
            this.InputLabel.Name = "InputLabel";
            this.InputLabel.Size = new System.Drawing.Size(51, 19);
            this.InputLabel.TabIndex = 0;
            this.InputLabel.Text = "Input";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RTBox);
            this.panel1.Location = new System.Drawing.Point(75, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 277);
            this.panel1.TabIndex = 2;
            // 
            // RTBox
            // 
            this.RTBox.BackColor = System.Drawing.SystemColors.Info;
            this.RTBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTBox.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RTBox.Location = new System.Drawing.Point(0, 0);
            this.RTBox.Name = "RTBox";
            this.RTBox.ReadOnly = true;
            this.RTBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.RTBox.Size = new System.Drawing.Size(658, 277);
            this.RTBox.TabIndex = 4;
            this.RTBox.Text = "";
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultLabel.Location = new System.Drawing.Point(13, 16);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(56, 19);
            this.ResultLabel.TabIndex = 1;
            this.ResultLabel.Text = "Result";
            // 
            // MainForm
            // 
            this.AccessibleDescription = "NLP Test application";
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Application;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 536);
            this.Controls.Add(this.SP);
            this.Controls.Add(this.SB);
            this.Controls.Add(this.MenuPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MainForm";
            this.Text = "NLP Sandbox";
            this.MenuPanel.ResumeLayout(false);
            this.MenuPanel.PerformLayout();
            this.SP.Panel1.ResumeLayout(false);
            this.SP.Panel1.PerformLayout();
            this.SP.Panel2.ResumeLayout(false);
            this.SP.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SP)).EndInit();
            this.SP.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.ComboBox APISourceCombo;
        private System.Windows.Forms.Label APISourceLabel;
        private System.Windows.Forms.StatusStrip SB;
        private System.Windows.Forms.SplitContainer SP;
        private System.Windows.Forms.TextBox InputText;
        private System.Windows.Forms.Label InputLabel;
        private System.Windows.Forms.Button BTNQuery;
        private System.Windows.Forms.Button BTNWords;
        private System.Windows.Forms.Button BTNSentences;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BTNEntities;
        private System.Windows.Forms.Button BTNTags;
        private System.Windows.Forms.RichTextBox RTBox;
    }
}

