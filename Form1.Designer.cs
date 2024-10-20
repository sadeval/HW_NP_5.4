namespace GutenbergBookViewer
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listBoxBooks;
        private System.Windows.Forms.TextBox textBoxBookText;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.listBoxBooks = new System.Windows.Forms.ListBox();
            this.textBoxBookText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            // 
            // listBoxBooks
            // 
            this.listBoxBooks.FormattingEnabled = true;
            this.listBoxBooks.ItemHeight = 16;
            this.listBoxBooks.Location = new System.Drawing.Point(12, 12);
            this.listBoxBooks.Name = "listBoxBooks";
            this.listBoxBooks.Size = new System.Drawing.Size(250, 420);
            this.listBoxBooks.TabIndex = 0;
            this.listBoxBooks.SelectedIndexChanged += new System.EventHandler(this.listBoxBooks_SelectedIndexChanged);

            // 
            // textBoxBookText
            // 
            this.textBoxBookText.Location = new System.Drawing.Point(268, 12);
            this.textBoxBookText.Multiline = true;
            this.textBoxBookText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBookText.Size = new System.Drawing.Size(500, 420);
            this.textBoxBookText.TabIndex = 1;

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxBookText);
            this.Controls.Add(this.listBoxBooks);
            this.Name = "Form1";
            this.Text = "Gutenberg Book Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
