namespace GoldenEggVine.Forms
{
    partial class LevelSelector
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
            this.LevelChoiceTB = new System.Windows.Forms.TextBox();
            this.LevelsListLV = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.DescriptionFilterTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.LevelChoiceTB.Location = new System.Drawing.Point(244, 12);
            this.LevelChoiceTB.MaxLength = 2;
            this.LevelChoiceTB.Name = "textBox1";
            this.LevelChoiceTB.Size = new System.Drawing.Size(29, 20);
            this.LevelChoiceTB.TabIndex = 0;
            this.LevelChoiceTB.TextChanged += new System.EventHandler(this.LevelChoiceTB_TextChanged);
            // 
            // listView1
            // 
            this.LevelsListLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.LevelsListLV.FullRowSelect = true;
            this.LevelsListLV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LevelsListLV.Location = new System.Drawing.Point(12, 63);
            this.LevelsListLV.MultiSelect = false;
            this.LevelsListLV.Name = "listView1";
            this.LevelsListLV.Size = new System.Drawing.Size(261, 276);
            this.LevelsListLV.TabIndex = 5;
            this.LevelsListLV.UseCompatibleStateImageBehavior = false;
            this.LevelsListLV.View = System.Windows.Forms.View.Details;
            this.LevelsListLV.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LevelsListLV_MouseClick);
            this.LevelsListLV.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LevelsListLV_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Index";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 192;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select Level from List or type Level Index here";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(-1, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1, 1);
            this.button1.TabIndex = 8;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SubmitB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Filter description";
            // 
            // textBox2
            // 
            this.DescriptionFilterTB.Location = new System.Drawing.Point(102, 37);
            this.DescriptionFilterTB.MaxLength = 32678;
            this.DescriptionFilterTB.Name = "textBox2";
            this.DescriptionFilterTB.Size = new System.Drawing.Size(171, 20);
            this.DescriptionFilterTB.TabIndex = 1;
            this.DescriptionFilterTB.TextChanged += new System.EventHandler(this.DescriptionFilterTB_TextChanged);
            // 
            // LevelSelector
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 351);
            this.Controls.Add(this.DescriptionFilterTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LevelsListLV);
            this.Controls.Add(this.LevelChoiceTB);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(630, 900);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(301, 389);
            this.Name = "LevelSelector";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Select a Level";
            this.SizeChanged += new System.EventHandler(this.LevelSelector_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LevelChoiceTB;
        private System.Windows.Forms.ListView LevelsListLV;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DescriptionFilterTB;

    }
}