namespace InterfataUtilizator_WindowsForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
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
            this.btn_client = new System.Windows.Forms.Button();
            this.btn_manager = new System.Windows.Forms.Button();
            this.lblOptiune = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_client
            // 
            this.btn_client.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_client.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_client.Font = new System.Drawing.Font("Papyrus", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_client.Location = new System.Drawing.Point(357, 236);
            this.btn_client.Name = "btn_client";
            this.btn_client.Size = new System.Drawing.Size(446, 101);
            this.btn_client.TabIndex = 0;
            this.btn_client.Text = "Client";
            this.btn_client.UseVisualStyleBackColor = false;
            this.btn_client.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // btn_manager
            // 
            this.btn_manager.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_manager.Font = new System.Drawing.Font("Papyrus", 12F, System.Drawing.FontStyle.Bold);
            this.btn_manager.Location = new System.Drawing.Point(357, 377);
            this.btn_manager.Name = "btn_manager";
            this.btn_manager.Size = new System.Drawing.Size(446, 101);
            this.btn_manager.TabIndex = 1;
            this.btn_manager.Text = "Manager";
            this.btn_manager.UseVisualStyleBackColor = false;
            this.btn_manager.Click += new System.EventHandler(this.btnManager_Click);
            // 
            // lblOptiune
            // 
            this.lblOptiune.AutoSize = true;
            this.lblOptiune.Font = new System.Drawing.Font("Papyrus", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptiune.Location = new System.Drawing.Point(364, 139);
            this.lblOptiune.Name = "lblOptiune";
            this.lblOptiune.Size = new System.Drawing.Size(418, 76);
            this.lblOptiune.TabIndex = 2;
            this.lblOptiune.Text = "Alegeti o optiune:";
            this.lblOptiune.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1174, 1129);
            this.Controls.Add(this.lblOptiune);
            this.Controls.Add(this.btn_manager);
            this.Controls.Add(this.btn_client);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Meniu Principal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_client;
        private System.Windows.Forms.Button btn_manager;
        private System.Windows.Forms.Label lblOptiune;
    }
}

