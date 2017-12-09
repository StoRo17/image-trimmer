namespace ImageTrimmer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileButton = new System.Windows.Forms.Button();
            this.doneLabel = new System.Windows.Forms.Label();
            this.openDirectoryButton = new System.Windows.Forms.Button();
            this.retrieveFromDbButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(12, 44);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(128, 23);
            this.openFileButton.TabIndex = 0;
            this.openFileButton.Text = "Открыть файл";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // doneLabel
            // 
            this.doneLabel.AutoSize = true;
            this.doneLabel.Location = new System.Drawing.Point(12, 9);
            this.doneLabel.Name = "doneLabel";
            this.doneLabel.Size = new System.Drawing.Size(0, 13);
            this.doneLabel.TabIndex = 1;
            // 
            // openDirectoryButton
            // 
            this.openDirectoryButton.Location = new System.Drawing.Point(146, 44);
            this.openDirectoryButton.Name = "openDirectoryButton";
            this.openDirectoryButton.Size = new System.Drawing.Size(128, 23);
            this.openDirectoryButton.TabIndex = 2;
            this.openDirectoryButton.Text = "Открыть папку";
            this.openDirectoryButton.UseVisualStyleBackColor = true;
            this.openDirectoryButton.Click += new System.EventHandler(this.openDirectoryButton_Click);
            // 
            // retrieveFromDbButton
            // 
            this.retrieveFromDbButton.Location = new System.Drawing.Point(282, 44);
            this.retrieveFromDbButton.Name = "retrieveFromDbButton";
            this.retrieveFromDbButton.Size = new System.Drawing.Size(128, 23);
            this.retrieveFromDbButton.TabIndex = 3;
            this.retrieveFromDbButton.Text = "Достать из БД";
            this.retrieveFromDbButton.UseVisualStyleBackColor = true;
            this.retrieveFromDbButton.Click += new System.EventHandler(this.retrieveFromDbButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 9);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(398, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 4;
            this.progressBar.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 79);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.retrieveFromDbButton);
            this.Controls.Add(this.openDirectoryButton);
            this.Controls.Add(this.doneLabel);
            this.Controls.Add(this.openFileButton);
            this.Name = "Form1";
            this.Text = "Триммирование изображения/ий";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Label doneLabel;
        private System.Windows.Forms.Button openDirectoryButton;
        private System.Windows.Forms.Button retrieveFromDbButton;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

