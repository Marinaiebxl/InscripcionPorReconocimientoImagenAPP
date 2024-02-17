using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
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

namespace InscripcionPorReconocimientoImagen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofdBuscaImagen.ShowDialog();
            pbImagen.ImageLocation = ofdBuscaImagen.FileName;
            string photo = ofdBuscaImagen.FileName;



            Amazon.Rekognition.Model.Image image = new Amazon.Rekognition.Model.Image();

            try
            {
                using (FileStream fs = new FileStream(photo, FileMode.Open))
                {
                    byte[] data = null;
                    data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    image.Bytes = new MemoryStream(data);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load file " + photo);
            }
            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient("AKIAIE5LZMZN4CR6IO5Q", "xUtzMH5IxZmuZYrc9KSN83JE+pgf5J60+FajM65J", RegionEndpoint.USEast1);

            RecognizeCelebritiesRequest request = new RecognizeCelebritiesRequest()
            {
                Image = image,
            };
           RecognizeCelebritiesResponse response = rekognitionClient.RecognizeCelebrities(request);

            foreach (var item in response.CelebrityFaces)
            {
                txtConfirmarCelebridad.Text += item.Name + ">" + item.MatchConfidence.ToString() + "\r\n";
            }
            

            /*DetectModerationLabelsRequest request = new DetectModerationLabelsRequest()
            {
                Image = image
            };
            DetectModerationLabelsResponse response = rekonigtionClient.DetectModerationLabels(request);

            DetectTextRequest response = rekonigtionClient.DetectText(request);

            foreach (var item in response.ModerationLabels)
            {
                txtSalidad.Text += item.Name + ">" + item.Confidence.ToString() + "\r\n";
            }*/


            DetectTextRequest request2 = new DetectTextRequest()
            {
                Image = image
            };
            DetectTextResponse response2 = rekognitionClient.DetectText(request2);

            foreach (var item in response2.TextDetections)
            {
                TxtInformacion.Text += item.DetectedText + ">" + item.Confidence.ToString() + "\r\n";
            }
        }

        private void tableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            string Monto;
            if (txtConfirmarCelebridad.Text != null)
            {
                Monto = "900";
                montoTextBox.Text = Monto;
            }
            else
            {
                Monto = "1000";
                montoTextBox.Text = Monto;
            }
            //rdlc

            this.Validate();
            this.tableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.inscripcionesDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'inscripcionesDataSet.Table' table. You can move, or remove it, as needed.
            this.tableTableAdapter.Fill(this.inscripcionesDataSet.Table);

        }

        private void montoTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void ofdBuscaImagen_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void pbImagen_Click(object sender, EventArgs e)
        {

        }

        private void tableDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
