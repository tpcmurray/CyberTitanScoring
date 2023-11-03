namespace WindowsScoreKeeper
{
    partial class CyberTitan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CyberTitan));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lRunningTime = new System.Windows.Forms.Label();
            this.lPoints = new System.Windows.Forms.Label();
            this.doStateCheck = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 19.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(131, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(509, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "CyberTitan GFW Practice Windows 10 Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(204, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(258, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Approximage Image Running Time:";
            // 
            // lRunningTime
            // 
            this.lRunningTime.AutoSize = true;
            this.lRunningTime.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold);
            this.lRunningTime.Location = new System.Drawing.Point(468, 70);
            this.lRunningTime.Name = "lRunningTime";
            this.lRunningTime.Size = new System.Drawing.Size(65, 21);
            this.lRunningTime.TabIndex = 2;
            this.lRunningTime.Text = "0:00:00";
            // 
            // lPoints
            // 
            this.lPoints.AutoSize = true;
            this.lPoints.Font = new System.Drawing.Font("Calibri", 19.75F, System.Drawing.FontStyle.Bold);
            this.lPoints.Location = new System.Drawing.Point(203, 102);
            this.lPoints.Name = "lPoints";
            this.lPoints.Size = new System.Drawing.Size(330, 33);
            this.lPoints.TabIndex = 3;
            this.lPoints.Text = "0 out of 100 points received";
            // 
            // doStateCheck
            // 
            this.doStateCheck.Tick += new System.EventHandler(this.doStateCheck_Tick);
            // 
            // CyberTitan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 494);
            this.Controls.Add(this.lPoints);
            this.Controls.Add(this.lRunningTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CyberTitan";
            this.Text = "CyberTitan Score Card";
            this.Load += new System.EventHandler(this.CyberTitan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lRunningTime;
        private System.Windows.Forms.Label lPoints;
        private System.Windows.Forms.Timer doStateCheck;
    }
}

