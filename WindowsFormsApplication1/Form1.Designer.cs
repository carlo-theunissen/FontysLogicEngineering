namespace WindowsFormsApplication1
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.kickoff = new System.Windows.Forms.Button();
			this.fullTable = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.fullTable.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Input";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(22, 25);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(234, 66);
			this.textBox1.TabIndex = 1;
			// 
			// kickoff
			// 
			this.kickoff.Location = new System.Drawing.Point(274, 25);
			this.kickoff.Name = "kickoff";
			this.kickoff.Size = new System.Drawing.Size(75, 23);
			this.kickoff.TabIndex = 2;
			this.kickoff.Text = "Process";
			this.kickoff.UseVisualStyleBackColor = true;
			this.kickoff.Click += new System.EventHandler(this.kickoff_Click);
			// 
			// fullTable
			// 
			this.fullTable.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.fullTable.ColumnCount = 1;
			this.fullTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.fullTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.fullTable.Controls.Add(this.label2,0,0);
			this.fullTable.Location = new System.Drawing.Point(492, 25);
			this.fullTable.Name = "fullTable";
			this.fullTable.RowCount = 2;
			this.fullTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.fullTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.fullTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.fullTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.fullTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.fullTable.Size = new System.Drawing.Size(200, 213);
			this.fullTable.TabIndex = 3;
			this.fullTable.TabStop = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "label2";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(849, 394);
			this.Controls.Add(this.fullTable);
			this.Controls.Add(this.kickoff);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Properties";
			this.fullTable.ResumeLayout(false);
			this.fullTable.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button kickoff;
		private System.Windows.Forms.TableLayoutPanel fullTable;
		private System.Windows.Forms.Label label2;
	}
}

