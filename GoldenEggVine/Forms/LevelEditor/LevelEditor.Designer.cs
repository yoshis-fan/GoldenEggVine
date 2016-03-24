using GoldenEggVine.EditorRelated;
namespace GoldenEggVine.Forms
{
    partial class LevelEditor
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
            this.levelPanel = new GoldenEggVine.EditorRelated.NoFlickerPanel();
            this.SuspendLayout();
            // 
            // levelPanel
            // 
            this.levelPanel.AutoScroll = true;
            this.levelPanel.BackColor = System.Drawing.Color.Khaki;
            this.levelPanel.Location = new System.Drawing.Point(0, 0);
            this.levelPanel.Name = "levelPanel";
            this.levelPanel.Size = new System.Drawing.Size(200, 100);
            this.levelPanel.TabIndex = 0;
            this.levelPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.levelPanel_Paint);
            this.levelPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.levelPanel_MouseMove);
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(318, 233);
            this.Controls.Add(this.levelPanel);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "LevelEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "LevelEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LevelEditor_FormClosing);
            this.Enter += new System.EventHandler(this.LevelEditor_Enter);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LevelEditor_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

		private NoFlickerPanel levelPanel;


    }
}