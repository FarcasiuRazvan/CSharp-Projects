namespace Internship
{
    partial class MenuForm
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
            this.btnConnectPoints = new System.Windows.Forms.Button();
            this.btnTestRoutes = new System.Windows.Forms.Button();
            this.btnCustomGrid = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnectPoints
            // 
            this.btnConnectPoints.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnectPoints.Location = new System.Drawing.Point(307, 55);
            this.btnConnectPoints.Name = "btnConnectPoints";
            this.btnConnectPoints.Size = new System.Drawing.Size(176, 76);
            this.btnConnectPoints.TabIndex = 1;
            this.btnConnectPoints.Text = "Connect Points";
            this.btnConnectPoints.UseVisualStyleBackColor = true;
            this.btnConnectPoints.Click += new System.EventHandler(this.btnConnectPoints_Click);
            // 
            // btnTestRoutes
            // 
            this.btnTestRoutes.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestRoutes.Location = new System.Drawing.Point(307, 137);
            this.btnTestRoutes.Name = "btnTestRoutes";
            this.btnTestRoutes.Size = new System.Drawing.Size(176, 76);
            this.btnTestRoutes.TabIndex = 2;
            this.btnTestRoutes.Text = "Test Routes";
            this.btnTestRoutes.UseVisualStyleBackColor = true;
            this.btnTestRoutes.Click += new System.EventHandler(this.btnTestRoutes_Click);
            // 
            // btnCustomGrid
            // 
            this.btnCustomGrid.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomGrid.Location = new System.Drawing.Point(307, 219);
            this.btnCustomGrid.Name = "btnCustomGrid";
            this.btnCustomGrid.Size = new System.Drawing.Size(176, 76);
            this.btnCustomGrid.TabIndex = 3;
            this.btnCustomGrid.Text = "Custom Grid";
            this.btnCustomGrid.UseVisualStyleBackColor = true;
            this.btnCustomGrid.Click += new System.EventHandler(this.btnCustomGrid_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(307, 301);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(176, 76);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCustomGrid);
            this.Controls.Add(this.btnTestRoutes);
            this.Controls.Add(this.btnConnectPoints);
            this.Name = "MenuForm";
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnConnectPoints;
        private System.Windows.Forms.Button btnTestRoutes;
        private System.Windows.Forms.Button btnCustomGrid;
        private System.Windows.Forms.Button btnExit;
    }
}

