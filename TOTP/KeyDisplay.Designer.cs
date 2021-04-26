
namespace TOTP
{
    partial class KeyDisplay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.lblIssuer = new System.Windows.Forms.Label();
            this.lblAcct = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 6;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpMain.Controls.Add(this.pbIcon, 1, 1);
            this.tlpMain.Controls.Add(this.lblIssuer, 2, 1);
            this.tlpMain.Controls.Add(this.lblAcct, 2, 2);
            this.tlpMain.Controls.Add(this.lblCode, 4, 1);
            this.tlpMain.Controls.Add(this.lblTime, 4, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpMain.Size = new System.Drawing.Size(456, 62);
            this.tlpMain.TabIndex = 0;
            this.tlpMain.Click += new System.EventHandler(this.Control_Click);
            // 
            // pbIcon
            // 
            this.pbIcon.Location = new System.Drawing.Point(11, 11);
            this.pbIcon.Name = "pbIcon";
            this.tlpMain.SetRowSpan(this.pbIcon, 2);
            this.pbIcon.Size = new System.Drawing.Size(40, 40);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIcon.TabIndex = 1;
            this.pbIcon.TabStop = false;
            this.pbIcon.Click += new System.EventHandler(this.Control_Click);
            // 
            // lblIssuer
            // 
            this.lblIssuer.AutoEllipsis = true;
            this.lblIssuer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIssuer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIssuer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIssuer.Location = new System.Drawing.Point(57, 11);
            this.lblIssuer.Margin = new System.Windows.Forms.Padding(3);
            this.lblIssuer.Name = "lblIssuer";
            this.lblIssuer.Size = new System.Drawing.Size(362, 20);
            this.lblIssuer.TabIndex = 2;
            this.lblIssuer.Click += new System.EventHandler(this.Control_Click);
            // 
            // lblAcct
            // 
            this.lblAcct.AutoEllipsis = true;
            this.lblAcct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAcct.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAcct.Location = new System.Drawing.Point(57, 37);
            this.lblAcct.Margin = new System.Windows.Forms.Padding(3);
            this.lblAcct.Name = "lblAcct";
            this.lblAcct.Size = new System.Drawing.Size(362, 14);
            this.lblAcct.TabIndex = 3;
            this.lblAcct.Click += new System.EventHandler(this.Control_Click);
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCode.Location = new System.Drawing.Point(445, 11);
            this.lblCode.Margin = new System.Windows.Forms.Padding(3);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(0, 20);
            this.lblCode.TabIndex = 4;
            this.lblCode.Click += new System.EventHandler(this.Control_Click);
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.AutoSize = true;
            this.lblTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTime.Location = new System.Drawing.Point(446, 38);
            this.lblTime.Margin = new System.Windows.Forms.Padding(3);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(0, 13);
            this.lblTime.TabIndex = 5;
            this.lblTime.Click += new System.EventHandler(this.Control_Click);
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // KeyDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tlpMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "KeyDisplay";
            this.Size = new System.Drawing.Size(456, 62);
            this.Click += new System.EventHandler(this.Control_Click);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.Label lblIssuer;
        private System.Windows.Forms.Label lblAcct;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer tmrUpdate;
    }
}
