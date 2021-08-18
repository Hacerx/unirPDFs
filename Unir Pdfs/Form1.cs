using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unir_Pdfs
{
    public partial class Form1 : Form
    {

        string[] pdfsIniciales = new string[0];

        public Form1()
        {
            InitializeComponent();
        }

        public void CreateMergedPDF()
        {
            SaveFileDialog rutaFinal = new SaveFileDialog();
            rutaFinal.Filter = "PDF (*.pdf)|*.pdf";
            if (rutaFinal.ShowDialog() == DialogResult.OK) {
                String targetPDF = rutaFinal.FileName;
                using (FileStream stream = new FileStream(targetPDF, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4);
                    PdfCopy pdf = new PdfCopy(pdfDoc, stream);
                    pdfDoc.Open();
                    int division = 100 / (pdfsIniciales.Length);
                    //var files = Directory.GetFiles(sourceDir);
                    foreach (string file in pdfsIniciales)
                    {
                        if (file.Split('.').Last().ToUpper() == "PDF")
                        {
                            pdf.AddDocument(new PdfReader(file));
                            progressBar1.Value += division;
                        }
                    }
                    progressBar1.Value = 100;
                   
                    if (pdfDoc != null)
                        pdfDoc.Close();
                    richTextBox1.Text = "";
                    Array.Resize(ref pdfsIniciales, 0);
                    richTextBox1.Text = "¡Archivo generado correctamente!";

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100) {
                progressBar1.Value = 0;
                richTextBox1.Text = "";
            }
            OpenFileDialog archivos = new OpenFileDialog();
            archivos.Filter = "PDF (*.pdf)|*.pdf";
            archivos.Multiselect = true;
            if (archivos.ShowDialog() == DialogResult.OK) {
                foreach (String archivo in archivos.FileNames) {
                    Array.Resize(ref pdfsIniciales, pdfsIniciales.Length + 1);
                    pdfsIniciales[pdfsIniciales.Length - 1] = archivo;
                    richTextBox1.Text += archivo + "\n";
                }
                //textBox1.Text = archivos.FileName;

            }
            /*foreach (String aux in pdfsIniciales) {
                textBox1.Text += aux;
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateMergedPDF();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
