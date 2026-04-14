using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing.Text;

namespace CRUDMahasiswaADO
{
    public partial class Form1 : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=AYUNINGTYAS\\AYUNINGTYAS;Initial Catalog=DBAkademikADO;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void ConnectDatabase()
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                MessageBox.Show("Koneksi berhasil");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal: " + ex.Message);
            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectDatabase();
        }

        private void txtAlamat_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbJK_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (txtNIM.Text == "")
                {
                    MessageBox.Show("NIM harus diisi");
                    txtNIM.Focus();
                    return;
                }

                if (txtNama.Text == "")
                {
                    MessageBox.Show("Nama harus diisi");
                    txtNama.Focus();
                    return;
                }

                if (cmbJK.Text == "")
                {
                    MessageBox.Show("Jenis Kelamin harus dipilih");
                    txtKodeProdi.Focus();
                    return;
                }


                if (txtKodeProdi.Text == "")
                {
                    MessageBox.Show("Kode Prodi harus diisi");
                    txtKodeProdi.Focus();
                    return;
                }

                string query = @"INSERT INTO Mahasiswa (NIM, Nama, JenisKelamin, TanggalLahir, Alamat, KodeProdi, TanggalDaftar) 
                             VALUES (@NIM, @Nama, @JenisKelamin, @TanggalLahir, @Alamat, @KodeProdi, @TanggalDaftar)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@NIM", txtNIM.Text);
                cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@NamaKelamin", txtNama.Text);
                cmd.Parameters.AddWithValue("@JenisKelamin", cmbJK.Text);
                cmd.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value.Date);
                cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("@KodeProdi", txtKodeProdi.Text);
                cmd.Parameters.AddWithValue("@TanggalDaftar", DateTime.Now);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data mahasiswa berhasil ditambahkan");
                    ClearForm();
                    btnLoad.PerformClick();

                }
                else
                {
                    MessageBox.Show("Gagal menyimpan data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = @"UPDATE Mahasiswa SET Nama = @Nama, JenisKelamin = @JK, 
                                                      TanggalLahir = @TanggalLahir, 
                                                      Alamat = @Alamat,
                                                      KodeProdi = @KodeProdi 
                                WHERE NIM = @NIM";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@NIM", txtNIM.Text);
                cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@JK", cmbJK.Text);
                cmd.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value.Date);
                cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("@KodeProdi", txtKodeProdi.Text);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data berhasil diupdate");
                    ClearForm();
                    btnLoad.PerformClick();
                }
                else
                {
                    MessageBox.Show("Data tidak ditemukan");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                DialogResult resultConfirm = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultConfirm == DialogResult.Yes)
                {
                    string query = "DELETE FROM Mahasiswa WHERE NIM = @NIM";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NIM", txtNIM.Text);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Data berhasil dihapus");
                        btnLoad.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Data tidak ditemukan");
                        ClearForm();
                        btnLoad.PerformClick();
                    }
                }
                else
                {
                    MessageBox.Show("Data tidak ditemukan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        
