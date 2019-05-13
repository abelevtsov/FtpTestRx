namespace FtpTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnGetFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTimeLeft = new System.Windows.Forms.Label();
            this.txtURI = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.lblBytesRead = new System.Windows.Forms.Label();
            this.lblDownloadComplete = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGetFile
            // 
            this.btnGetFile.Location = new System.Drawing.Point(28, 27);
            this.btnGetFile.Name = "btnGetFile";
            this.btnGetFile.Size = new System.Drawing.Size(75, 23);
            this.btnGetFile.TabIndex = 0;
            this.btnGetFile.Text = "Get File";
            this.btnGetFile.UseVisualStyleBackColor = true;
            this.btnGetFile.Click += new System.EventHandler(this.btnGetFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Estimated time:";
            // 
            // lblTimeLeft
            // 
            this.lblTimeLeft.Location = new System.Drawing.Point(125, 110);
            this.lblTimeLeft.Name = "lblTimeLeft";
            this.lblTimeLeft.Size = new System.Drawing.Size(90, 14);
            this.lblTimeLeft.TabIndex = 2;
            this.lblTimeLeft.Text = "[lblTimeLeft]";
            // 
            // txtURI
            // 
            this.txtURI.Location = new System.Drawing.Point(120, 27);
            this.txtURI.Name = "txtURI";
            this.txtURI.Size = new System.Drawing.Size(335, 20);
            this.txtURI.TabIndex = 5;
            this.txtURI.Text = "ftp://speedtest.tele2.net/10GB.zip";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(28, 67);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(427, 23);
            this.progressBar.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(272, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Downloaded bytes:";
            // 
            // lblBytesRead
            // 
            this.lblBytesRead.AutoSize = true;
            this.lblBytesRead.Location = new System.Drawing.Point(380, 110);
            this.lblBytesRead.Name = "lblBytesRead";
            this.lblBytesRead.Size = new System.Drawing.Size(75, 13);
            this.lblBytesRead.TabIndex = 10;
            this.lblBytesRead.Text = "[lblBytesRead]";
            // 
            // lblDownloadComplete
            // 
            this.lblDownloadComplete.AutoSize = true;
            this.lblDownloadComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDownloadComplete.ForeColor = System.Drawing.Color.Green;
            this.lblDownloadComplete.Location = new System.Drawing.Point(174, 188);
            this.lblDownloadComplete.Name = "lblDownloadComplete";
            this.lblDownloadComplete.Size = new System.Drawing.Size(152, 20);
            this.lblDownloadComplete.TabIndex = 11;
            this.lblDownloadComplete.Text = "Download Complete";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(118, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Transfer rate (kbps):";
            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.Location = new System.Drawing.Point(244, 141);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(46, 13);
            this.lblRate.TabIndex = 13;
            this.lblRate.Text = "[lblRate]";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 220);
            this.Controls.Add(this.lblRate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDownloadComplete);
            this.Controls.Add(this.lblBytesRead);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.txtURI);
            this.Controls.Add(this.lblTimeLeft);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Ftp Test";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button btnGetFile;
	private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblTimeLeft;
	private System.Windows.Forms.TextBox txtURI;
	private System.Windows.Forms.ProgressBar progressBar;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.Label lblBytesRead;
	private System.Windows.Forms.Label lblDownloadComplete;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label lblRate;
    }
}

